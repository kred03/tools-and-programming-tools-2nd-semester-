namespace Danilau_353505_Lab5.Domain
{
    public class HeatingUnit
    {
        public int Id { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public double Power { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
