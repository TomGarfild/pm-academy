using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Authentication.Models;
using DepsWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly DataContext _dbContext;

        /// <summary>
        /// Authentication service constructor.
        /// </summary>
        /// <param name="dbContext">Data context.</param>
        public AuthService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Account id.</returns>
        public async Task<bool> RegisterAsync(string login, string password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));

            if (await _dbContext.Users.AnyAsync(u => u.Login == login)) return false;

            await _dbContext.Users.AddAsync(new Account(login, GetHash(password)));
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Checks if can login.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Accounts id if can.</returns>
        public async Task<int?> FindAsync(string login, string password)
        {
            if (login == null || password == null) return null;

            var account = await _dbContext.Users.FirstOrDefaultAsync(acc => acc.Login == login);

            if (account == null ||  account.PasswordHash != GetHash(password)) return null;

            return account.Id;
        }

        private static int GetHash(string str)
        {
            return int.MinValue + str.Select((t, i) => (i + 1) * (t - 'a' + 1)).Sum();
        }
    }
}