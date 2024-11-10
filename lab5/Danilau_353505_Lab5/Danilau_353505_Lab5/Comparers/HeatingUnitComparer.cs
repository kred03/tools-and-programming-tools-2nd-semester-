using Danilau_353505_Lab5.Domain;

namespace Danilau_353505_Lab5.Comparers
{
    public class HeatingUnitComparer : IEqualityComparer<HeatingUnit>
    {
        public bool Equals(HeatingUnit? x, HeatingUnit? y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.UnitType == y.UnitType &&
                   x.Power == y.Power &&
                   x.Location == y.Location;
        }

        public int GetHashCode(HeatingUnit obj)
        {
            return HashCode.Combine(obj.Id, obj.UnitType, obj.Power, obj.Location);
        }
    }
}
