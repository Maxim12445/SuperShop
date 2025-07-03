using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using SuperShopApi.Services;
using SuperShopApi.ViewModels;

namespace SuperShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authenticate;

        public AccountController(IConfiguration configuration, IAuthenticate authenticate)
        {
            _configuration = configuration ?? 
                throw new ArgumentNullException(nameof(configuration));
            _authenticate = authenticate ?? 
                throw new ArgumentNullException(nameof(authenticate));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword",
                    "A senha e a confirmação de senha não coincidem.");
                return BadRequest(ModelState);
            }

            var result = await _authenticate.RegisterUser(model.Email, model.Password);

            if (result)
            {
                return Ok($"Usuário {model.Email} criado com sucesso.");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro inválido.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                return GenerateToken(userInfo);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Usuário ou senha inválidos.");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel userInfo)
        {
            var claims = new[]
            {
                new Claim("email", userInfo.Email),
                new Claim("meuToken", "token do Max"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
