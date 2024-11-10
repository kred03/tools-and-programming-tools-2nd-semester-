namespace Danilau_353505_Lab5.Domain
{
    public interface ISerializer
    {
        IEnumerable<BuildingHeatingSystem> DeSerializeByLINQ(string fileName);
        IEnumerable<BuildingHeatingSystem> DeSerializeXML(string fileName);
        IEnumerable<BuildingHeatingSystem> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<BuildingHeatingSystem> xxx, string fileName);
        void SerializeXML(IEnumerable<BuildingHeatingSystem> xxx, string fileName);
        void SerializeJSON(IEnumerable<BuildingHeatingSystem> xxx, string fileName);
    }
}
