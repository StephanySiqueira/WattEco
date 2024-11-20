using Microsoft.ML.Data;

namespace WattEco.Models
{
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; } // Resultado da previsão (true para positivo, false para negativo)

        public float Probability { get; set; } // Probabilidade da previsão
    }
}
