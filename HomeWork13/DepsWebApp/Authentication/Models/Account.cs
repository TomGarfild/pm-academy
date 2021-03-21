using System.ComponentModel.DataAnnotations.Schema;

namespace DepsWebApp.Authentication.Models
{
    /// <summary>
    /// Account model for authentication.
    /// </summary>
    [Table("Users")]
    public class Account
    {
        /// <summary>
        /// Auto generated account id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// PasswordHash.
        /// </summary>
        public int PasswordHash { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="login">Login.</param>
        /// <param name="passwordHash">Password hash.</param>
        public Account(string login, int passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }
    }
}