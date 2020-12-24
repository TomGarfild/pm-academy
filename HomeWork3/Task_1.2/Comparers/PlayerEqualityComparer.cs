using System;
using System.Collections.Generic;

namespace Task_1._2.Comparers
{
    public class PlayerEqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            return x.Age == y.Age && x.FirstName == y.FirstName && x.LastName == y.LastName && x.Rank == y.Rank;
        }

        public int GetHashCode(Player obj)
        {
            return HashCode.Combine(obj.Age, obj.FirstName, obj.LastName, (int)obj.Rank);
        }
    }
}