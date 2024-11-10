using Danilau_353505_Lab5.Domain;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.Json;
using System.Globalization;

namespace SerializerLib;

public class Serializer : ISerializer
{
    private static readonly JsonSerializerOptions options = new() { WriteIndented = true };

    public IEnumerable<BuildingHeatingSystem> DeSerializeByLINQ(string fileName)
    {
        var doc = XDocument.Load(fileName);
        var root = doc.Element("HeatingSystems");
        foreach (var element in root!.Elements("HeatingSystem"))
        {
            var id = int.Parse(element.Element("Id")!.Value);
            var buildingName = element.Element("BuildingName")!.Value;
            var heatingUnitsElement = element.Element("HeatingUnits");

            var heatingUnits = heatingUnitsElement?.Elements("HeatingUnit").Select(h => new HeatingUnit
            {
                Id = int.Parse(h.Element("Id")!.Value),
                UnitType = h.Element("UnitType")!.Value,
                Power = double.Parse(h.Element("Power")!.Value, CultureInfo.InvariantCulture), // Используем InvariantCulture
                Location = h.Element("Location")!.Value
            }).ToList() ?? new List<HeatingUnit>();

            yield return new BuildingHeatingSystem
            {
                Id = id,
                BuildingName = buildingName,
                HeatingUnits = heatingUnits
            };
        }
    }

    public IEnumerable<BuildingHeatingSystem> DeSerializeXML(string fileName)
    {
        var serializer = new XmlSerializer(typeof(List<BuildingHeatingSystem>));
        using var reader = new StreamReader(fileName);
        return (List<BuildingHeatingSystem>?)serializer.Deserialize(reader) ?? [];
    }

    public IEnumerable<BuildingHeatingSystem> DeSerializeJSON(string fileName)
    {
        var json = File.ReadAllText(fileName);
        return JsonSerializer.Deserialize<List<BuildingHeatingSystem>>(json) ?? [];
    }

    public void SerializeByLINQ(IEnumerable<BuildingHeatingSystem> systems, string fileName)
    {
        var xml = new XElement("HeatingSystems",
            systems.Select(static s => new XElement("HeatingSystem",
                new XElement("Id", s.Id),
                new XElement("BuildingName", s.BuildingName),
                new XElement("HeatingUnits",
                    s.HeatingUnits.Select(static h => new XElement("HeatingUnit",
                        new XElement("Id", h.Id),
                        new XElement("UnitType", h.UnitType),
                        new XElement("Power", h.Power),
                        new XElement("Location", h.Location)
                    ))
                )
            ))
        );
        xml.Save(fileName);
    }

    public void SerializeXML(IEnumerable<BuildingHeatingSystem> systems, string fileName)
    {
        var serializer = new XmlSerializer(typeof(List<BuildingHeatingSystem>));
        using var writer = new StreamWriter(fileName);
        serializer.Serialize(writer, systems.ToList());
    }

    public void SerializeJSON(IEnumerable<BuildingHeatingSystem> systems, string fileName)
    {
        var json = JsonSerializer.Serialize(systems, options);
        File.WriteAllText(fileName, json);
    }
}
