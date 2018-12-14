using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using SecurityDemo.Authentication.JWT.AuthHelper;
using SecurityDemo.Authentication.JWT.Models;

namespace SecurityDemo.Authentication.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IUserService userService, IMemoryCache cache)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
            _cache = cache;
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

            _cache.Set(refreshToken, user.UserName, TimeSpan.FromMinutes(11));

            var token = await _jwtFactory.GenerateEncodedToken(user.UserName, refreshToken, claimsIdentity);
            return new OkObjectResult(token);
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken([FromBody]RefreshTokenRequest request)
        {
            string userName;
            if (!_cache.TryGetValue(request.RefreshToken, out userName))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid refreshtoken.");
                return BadRequest(ModelState);
            }
            if (!request.UserName.Equals(userName))
            {
                ModelState.AddModelError("refreshtoken_failure", "Invalid userName.");
                return BadRequest(ModelState);
            }

            var user = _userService.GetUserByName(request.UserName);
            string newRefreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);

            _cache.Remove(request.RefreshToken);
            _cache.Set(newRefreshToken, user.UserName, TimeSpan.FromMinutes(11));

            var token = await _jwtFactory.GenerateEncodedToken(user.UserName, newRefreshToken, claimsIdentity);
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
            return Ok(claimsIdentity.Claims.ToList().Select(r=> new { r.Type, r.Value}));
        }
    }
}