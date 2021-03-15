using System.Threading.Tasks;

namespace DepsWebApp.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<int?> RegisterAsync(string login, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<int?> FindAsync(string login, string password);
    }
}