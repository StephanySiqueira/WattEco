using WattEco.Models;
using Microsoft.ML;
using System.IO;

namespace WattEco.Training
{
    public class SentimentAnalysisTrainer  
    {
        private readonly MLContext _mlContext;

        public SentimentAnalysisTrainer()
        {
            _mlContext = new MLContext();
        }

        public void TrainAndSaveModel(string modelPath)
        {
            // Chama o método estático de EnergyConsumptionDataGenerator
            var energyData = EnergyConsumptionDataGenerator.GetEnergyConsumptionData();

            // Carregar os dados no formato adequado
            var data = _mlContext.Data.LoadFromEnumerable(energyData);

            // Pipeline de transformação para dados de energia
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(EnergyConsumptionData.Text))
               .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
                   labelColumnName: nameof(EnergyConsumptionData.ConsumptionLevel), maximumNumberOfIterations: 100));

            // Treinamento do modelo
            var model = pipeline.Fit(data);

            // Verifica e cria o diretório caso não exista
            var directory = Path.GetDirectoryName(modelPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Salvar o modelo treinado
            _mlContext.Model.Save(model, data.Schema, modelPath);
        }
    }
}



