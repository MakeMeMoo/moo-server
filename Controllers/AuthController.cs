using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;
using moo_server.Core.BL.Interfaces;
using moo_server.Core.Entities;
using moo_server.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using moo_server.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace moo_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersBL _usersBL;
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(
            ILogger<AccountController> logger,
            IMapper mapper,
            IUsersBL usersBL,
            IConfiguration configuration,
            IJwtAuthManager jwtAuthManager)
        {
            _usersBL = usersBL;
            _jwtAuthManager = jwtAuthManager;
        }

        [SwaggerOperation(Summary = "Аутентификация по Логину/Паролю")]
        [AllowAnonymous]
        [HttpPost("authByLogin")]
        public IActionResult AuthByLogin(LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest();

            //TODO: get user from DB by loginModel
            if (loginModel.UserName != "User1" && loginModel.UserName != "User2") return Unauthorized();

            var user = new UserModel { TgUsername = "User1", MooCount = 15 };

            var claims = new []
            {
                new Claim(ClaimTypes.Name, user.TgUsername),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(loginModel.UserName, claims, DateTime.Now);

            return Ok(new ResponseModel(new { 
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString,
                UserName = loginModel.UserName
            }));
        }

        [SwaggerOperation(Summary = "Аутентификация через Telegram")]
        [AllowAnonymous]
        [HttpPost("authByTelegram")]
        public IActionResult AuthByTelegram(TelegramLoginModel loginModel)
        {
            if (!ModelState.IsValid || !loginModel.CheckAuth()) return BadRequest();

            //TODO: get user from DB by loginModel
            var user = _usersBL.GetUserByTgUsername(loginModel.Username);
            if (user == null) return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.TgUsername),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtResult = _jwtAuthManager.GenerateTokens(loginModel.Username, claims, DateTime.Now);

            return Ok(new ResponseModel(new
            {
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString,
                UserName = loginModel.Username
            }));
        }

        [SwaggerOperation(Summary = "Информация о текущем пользователе")]
        [HttpGet("user")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            return Ok(new ResponseModel(new { UserName = User.Identity?.Name }));
        }

        [SwaggerOperation(Summary = "Выход из приложения")]
        [HttpPost("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            var userName = User.Identity?.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            return Ok(new ResponseModel($"User [{userName}] logged out the system."));
        }

        [SwaggerOperation(Summary = "Обновляет AccessToken")]
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenModel request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.RefreshToken)) return Unauthorized();

                var userName = User.Identity?.Name;
                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);

                return Ok(new ResponseModel(new {
                    UserName = userName,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                }));
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }
    }
}
