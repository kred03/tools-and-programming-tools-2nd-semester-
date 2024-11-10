using Danilau_353505_Lab5.Domain;
using Microsoft.Extensions.Configuration;
using SerializerLib;
using Danilau_353505_Lab5.Comparers;


namespace Danilau_353505_Lab5;


public class Program
{
    static void Main(string[] args)
    {
        // Load configuration
        var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("H:\\test\\Danilau_353505_Lab5\\Danilau_353505_Lab5\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string xmlFileName = configuration["FileName"] + ".xml";
        string linqXmlFileName = configuration["FileName"] + "_linq.xml";
        string jsonFileName = configuration["FileName"] + ".json";

        // Create collection of BuildingHeatingSystem
        var buildingSystems = new List<BuildingHeatingSystem>
        {
            new BuildingHeatingSystem { Id = 1, BuildingName = "Building A", HeatingUnits =
            [
                new HeatingUnit { Id = 1, UnitType = "Boiler", Power = 150.0, Location = "Basement" },
                new HeatingUnit { Id = 2, UnitType = "Radiator", Power = 10.5, Location = "First Floor" }
            ]},
            new BuildingHeatingSystem { Id = 2, BuildingName = "Building B", HeatingUnits =
            [
                new HeatingUnit { Id = 3, UnitType = "Heat Pump", Power = 120.0, Location = "Outside" },
                new HeatingUnit { Id = 4, UnitType = "Radiator", Power = 15.0, Location = "Second Floor" }
            ]},
            new BuildingHeatingSystem { Id = 3, BuildingName = "Building C", HeatingUnits =
            [
                new HeatingUnit { Id = 5, UnitType = "Furnace", Power = 200.0, Location = "Garage" }
            ]}
        };

        // Serialize collection to XML, LINQ-XML, and JSON
        var serializer = new Serializer();
        serializer.SerializeXML(buildingSystems, xmlFileName);
        serializer.SerializeByLINQ(buildingSystems, linqXmlFileName);
        serializer.SerializeJSON(buildingSystems, jsonFileName);

        // Display serialized content
        Console.WriteLine("Serialized XML (XmlSerializer):\n" + File.ReadAllText(xmlFileName));
        Console.WriteLine("\nSerialized XML (LINQ-to-XML):\n" + File.ReadAllText(linqXmlFileName));
        Console.WriteLine("\nSerialized JSON:\n" + File.ReadAllText(jsonFileName));

        // Deserialize and compare with original collection
        var deserializedXml = serializer.DeSerializeXML(xmlFileName);
        var deserializedLinqXml = serializer.DeSerializeByLINQ(linqXmlFileName);
        var deserializedJson = serializer.DeSerializeJSON(jsonFileName);

        Console.WriteLine("\nDeserialization results match original collection: ");
        Console.WriteLine("XmlSerializer: " + buildingSystems.SequenceEqual(deserializedXml, new BuildingHeatingSystemComparer()));
        Console.WriteLine("LINQ-to-XML: " + buildingSystems.SequenceEqual(deserializedLinqXml, new BuildingHeatingSystemComparer()));
        Console.WriteLine("JSON: " + buildingSystems.SequenceEqual(deserializedJson, new BuildingHeatingSystemComparer()));
    }
}