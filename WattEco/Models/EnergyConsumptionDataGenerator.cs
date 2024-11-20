namespace WattEco.Models
{
    public class EnergyConsumptionDataGenerator
    {
        public static IEnumerable<EnergyConsumptionData> GetEnergyConsumptionData()
        {
            return new List<EnergyConsumptionData>
            {
                new EnergyConsumptionData { Text = "Seu consumo de energia está alto. Tente aplicar práticas para reduzir o uso e economizar energia.", ConsumptionLevel = true },
                new EnergyConsumptionData { Text = "Seu consumo de energia está ótimo! Continue mantendo essas boas práticas.", ConsumptionLevel = false }, 
            };
        }
    }
}