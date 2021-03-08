using System;
using DepsWebApp.Filters;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [TypeFilter(typeof(ExceptionFilter))]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Register user with login and password.
        /// </summary>
        /// <param name="account">Account model</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] Account account)
        {
            throw new NotImplementedException();
        }
    }
}