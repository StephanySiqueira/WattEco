namespace WattEco.Models
{
    public class EnergyConsumptionData
    {
        public string Text { get; set; }  // Descrição do consumo de energia
        public bool ConsumptionLevel { get; set; }  // Nível de consumo (true para alto, false para baixo)
    }
}
