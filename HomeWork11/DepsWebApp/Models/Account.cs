using System.Text.Json.Serialization;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Account model.
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account's login.
        /// </summary>
        [JsonPropertyName("login")]
        public string Login { get; set; }
        
        /// <summary>
        /// Account's password.
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}