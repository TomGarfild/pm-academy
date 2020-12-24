using System;
using System.Collections.Generic;

namespace Task_1._2.Comparers
{
    public class NameComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            var firstNameComparison = string.Compare(x.FirstName, y.FirstName, StringComparison.Ordinal);
            if (firstNameComparison != 0) return firstNameComparison;
            return string.Compare(x.LastName, y.LastName, StringComparison.Ordinal);
        }
    }
}