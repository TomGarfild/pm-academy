using System;

namespace DepsWebApp.Options
{
    /// <summary>
    /// Options for NBU client
    /// </summary>
    public class NbuClientOptions
    {
        /// <summary>
        /// Base address for NBU client.
        /// </summary>
        public string BaseAddress { get; set; }
        
        /// <summary>
        /// Is true when <see cref="BaseAddress"/> is valid. 
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
}
