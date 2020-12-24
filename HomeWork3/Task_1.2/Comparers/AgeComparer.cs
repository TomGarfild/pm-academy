using System.Collections.Generic;

namespace Task_1._2.Comparers
{
    public class AgeComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            return x.Age.CompareTo(y.Age);
        }
    }
}