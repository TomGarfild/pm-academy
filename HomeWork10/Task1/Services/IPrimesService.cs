using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task1.Services
{
    public interface IPrimesService
    {
        public Task<bool> IsPrimeAsync(int number);
        public Task<IList<int>> GetPrimesAsync(int from, int to);
    }
}