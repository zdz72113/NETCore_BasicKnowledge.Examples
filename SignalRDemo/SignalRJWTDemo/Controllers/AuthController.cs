using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SignalRJWTDemo.Authentication;
using SignalRJWTDemo.Models;
using SignalRJWTDemo.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignalRJWTDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService _userService;

        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IUserService userService)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var user = _userService.GetUserByName(request.UserName);
            if (user == null)
            {
                ModelState.AddModelError("login_failure", "Invalid username.");
                return BadRequest(ModelState);
            }
            if (!request.Password.Equals(user.Password))
            {
                ModelState.AddModelError("login_failure", "Invalid password.");
                return BadRequest(ModelState);
            }

            string refreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);

            var token = await _jwtFactory.GenerateEncodedToken(user.UserName, refreshToken, claimsIdentity);
            return new OkObjectResult(token);
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return Ok(claimsIdentity.Claims.ToList().Select(r => new { r.Type, r.Value }));
        }
    }
}
