using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using DepsWebApp.Authentication.Models;

namespace DepsWebApp.Services
{
    /// <summary>
    /// Authentication service.
    /// </summary>
    public class AuthServiceInMemory : IAuthService
    {
        private readonly ConcurrentDictionary<string, Account> _accounts = new ConcurrentDictionary<string, Account>();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        /// <summary>
        /// Register user.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Account id.</returns>
        public async Task<int?> RegisterAsync(string login, string password)
        {
            if (login == null) throw new ArgumentNullException(nameof(login));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var release = await _semaphore.WaitAsync(1000);
            try
            {
                var id = _accounts.Count + 1;
                if (!_accounts.TryAdd(login, new Account(id, password.GetHashCode()))) return null;
                return id;
            }
            finally
            {
                if (release) _semaphore.Release();
            }
        }

        /// <summary>
        /// Checks if can login.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="password">Password.</param>
        /// <returns>Accounts id if can.</returns>
        public Task<int?> FindAsync(string login, string password)
        {
            if (login == null || password == null) return Task.FromResult<int?>(default);

            if (!_accounts.TryGetValue(login, out var account) || account.PasswordHash != password.GetHashCode())
            {
                return Task.FromResult<int?>(default);
            }

            return Task.FromResult<int?>(account.Id);
        }
    }
}