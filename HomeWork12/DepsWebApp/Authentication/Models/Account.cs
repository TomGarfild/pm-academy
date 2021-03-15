namespace DepsWebApp.Authentication.Models
{
    /// <summary>
    /// Account model for authentication.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// PasswordHash.
        /// </summary>
        public int PasswordHash { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <param name="passwordHash">Password hash.</param>
        public Account(int id, int passwordHash)
        {
            Id = id;
            PasswordHash = passwordHash;
        }
    }
}