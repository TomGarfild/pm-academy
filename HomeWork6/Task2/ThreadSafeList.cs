using System.Collections.Generic;
using System.Linq;

namespace Task2
{
    public class ThreadSafeHashSet
    {
        private static HashSet<int> _storage = new HashSet<int>();
        private static readonly object Marker = new object();

        public void SyncAdd(int value)
        {
            lock (Marker)
            {
                _storage.Add(value);
            }
        }

        public void Sort()
        {
            lock (Marker)
            {
                _storage = _storage.OrderBy(s => s).ToHashSet();
            }
        }
        public int[] ToArray()
        {
            lock (Marker)
            {
                return _storage.ToArray();
            }
        }
    }
}