using Microsoft.AspNetCore.Mvc;
using WattEco.Models;
using WattEco.Services;
using System.Collections.Generic;
using System.Linq;

namespace WattEco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergiaController : ControllerBase
    {
        private readonly SentimentAnalysisService _sentimentAnalysisService;

        // Injeção de dependência para o serviço de análise de sentimentos
        public EnergiaController(SentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }

        // Endpoint para analisar o consumo de energia
        [HttpPost]
        [Route("analyze")]
        public IActionResult AnalyzeConsumo([FromBody] List<double> consumos)
        {
            if (consumos == null || !consumos.Any())
            {
                return BadRequest("Não foi informado nenhum valor de consumo.");
            }

            // Calcula a média de consumo
            double mediaConsumo = consumos.Average();

            // Gera o feedback baseado na média de consumo
            string feedback = GenerateFeedback(mediaConsumo);

            // Analisa o feedback usando a IA
            var analysisResult = _sentimentAnalysisService.Predict(feedback);

            // Lógica para corrigir sentimentos de forma manual
            string sentimento = "Negativo";
            if (feedback.Contains("ótimo") || feedback.Contains("continue"))
            {
                sentimento = "Positivo"; // Força o sentimento para positivo caso o feedback seja claramente positivo
            }
            else if (feedback.Contains("alto") || feedback.Contains("reduzir"))
            {
                sentimento = "Negativo"; // Caso o feedback seja sobre consumo alto ou redução
            }

            // Ajuste da probabilidade (caso o serviço sempre retorne 0.5, verifique o serviço)
            double probabilidade = analysisResult.Probability;
            if (probabilidade == 0.5)
            {
                // Caso o modelo não seja confiável, podemos ajustar manualmente a probabilidade
                // Isso pode ser feito com base em palavras chave como "ótimo" ou "alto"
                probabilidade = sentimento == "Positivo" ? 0.8 : 0.7;
            }

            // Retorna a resposta com o resultado da análise
            return Ok(new
            {
                MediaConsumo = mediaConsumo,
                Feedback = feedback,
                Sentimento = sentimento,
                Probabilidade = probabilidade
            });
        }

        // Gera o feedback com base na média de consumo
        private string GenerateFeedback(double mediaConsumo)
        {
            if (mediaConsumo > 200)
            {
                return "Seu consumo de energia está alto. Tente aplicar práticas para reduzir o uso e economizar energia.";
            }
            else if (mediaConsumo < 100)
            {
                return "Seu consumo de energia está ótimo. Continue com as boas práticas!";
            }
            else
            {
                return "Seu consumo de energia está na média. Considere práticas para otimizar o uso de energia.";
            }
        }
    }
}
