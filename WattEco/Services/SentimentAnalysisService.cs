using WattEco.Models;
using Microsoft.ML;
using System.IO;

namespace WattEco.Services
{
    public class SentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private ITransformer _model;

        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();
            // Carregar o modelo treinado
            _model = LoadModel(@"C:\Users\tesiq\OneDrive\Documentos\FIAP PROJETOS\2TDSPK\WattEco\WattEco\TrainedModels\energyConsumptionModel.zip");
        }

        public SentimentPrediction Predict(string text)
        {
            var input = new EnergyConsumptionData { Text = text };
            var inputData = _mlContext.Data.LoadFromEnumerable(new[] { input });
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<EnergyConsumptionData, SentimentPrediction>(_model);
            var result = predictionEngine.Predict(input);
            return result;
        }

        private ITransformer LoadModel(string modelPath)
        {
            return _mlContext.Model.Load(modelPath, out var _);
        }
    }
}
