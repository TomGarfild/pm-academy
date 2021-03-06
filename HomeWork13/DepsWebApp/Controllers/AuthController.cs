﻿using System;
using System.Net;
using System.Threading.Tasks;
using DepsWebApp.Models;
using DepsWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly DataContext _dbContext;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="authService">Authentication service.</param>
        /// <param name="dbContext"></param>
        public AuthController(IAuthService authService, DataContext dbContext)
        {
            _authService = authService;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Register action.
        /// </summary>
        /// <param name="user">User model.</param>
        /// <returns>Returns Action result</returns>
        [HttpPost]
        [Route("register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (!ModelState.IsValid) return BadRequest();

            var canRegster = await _authService.RegisterAsync(user.Login, user.Password);
            if (canRegster) return Ok();
            return BadRequest();
        }
    }
}