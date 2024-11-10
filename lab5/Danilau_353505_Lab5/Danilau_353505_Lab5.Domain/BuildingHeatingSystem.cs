namespace Danilau_353505_Lab5.Domain
{
    public class BuildingHeatingSystem
    {
        public int Id { get; set; }
        public string BuildingName { get; set; } = string.Empty;
        public List<HeatingUnit> HeatingUnits { get; set; } = [];

        public BuildingHeatingSystem() { }
    }
}
