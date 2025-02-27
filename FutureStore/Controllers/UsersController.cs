using FutureStore.Authentication.BearerAuthentication.Jwt;
using FutureStore.Data;
using FutureStore.Helpers;
using FutureStore.Interfaces;
using FutureStore.Models.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FutureStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(JwtOptions jwtOptions,IGenericRepository<User> usersRepo) : ControllerBase
    {
        [HttpPost]
        [Route("auth")]
        public ActionResult<string> Authenticate(AuthenticationRequest request)
        {
            var user = usersRepo.FindOne(x => x.Username == request.Username && x.Password == request.Password);
            if (user == null)
            {
                return Unauthorized();
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                    
                }),
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return Ok(accessToken);
        }
    }
}
