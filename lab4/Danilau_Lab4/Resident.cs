using Danilau_Lab4.Interfaces;

namespace Danilau_Lab4
{
    public class Resident : ISerializable
    {
        public string Name { get; set; }
        public int ApartmentNumber { get; set; }

        public Resident()
        {
            Name = string.Empty;
        }

        public Resident(string name, int apartmentNumber)
        {
            Name = name;
            ApartmentNumber = apartmentNumber;
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(ApartmentNumber);
        }

        public void Deserialize(BinaryReader reader)
        {
            Name = reader.ReadString();
            ApartmentNumber = reader.ReadInt32();
        }

        public override string ToString()
        {
            return $"{Name} (Квартира №{ApartmentNumber})";
        }
    }

}
