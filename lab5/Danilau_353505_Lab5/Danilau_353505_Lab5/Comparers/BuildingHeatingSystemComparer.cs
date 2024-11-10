using Danilau_353505_Lab5.Domain;

namespace Danilau_353505_Lab5.Comparers
{
    public class BuildingHeatingSystemComparer : IEqualityComparer<BuildingHeatingSystem>
    {
        public bool Equals(BuildingHeatingSystem? x, BuildingHeatingSystem? y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.BuildingName == y.BuildingName &&
                   x.HeatingUnits.SequenceEqual(y.HeatingUnits, new HeatingUnitComparer());
        }

        public int GetHashCode(BuildingHeatingSystem obj)
        {
            return HashCode.Combine(obj.Id, obj.BuildingName);
        }
    }
}
