using System.ComponentModel.DataAnnotations;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Account model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Account's login.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Login { get; set; }

        /// <summary>
        /// Account's password.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [MinLength(6)]
        public string Password { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}