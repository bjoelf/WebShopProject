using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


using WebShopProjectApp.Users;


namespace WebShopProjectApp.Controllers
{
    [EnableCors("ReactPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class JwtController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        public JwtController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(JwtLogin jwtLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(jwtLogin.UserName, jwtLogin.Password, false, true);

                if (!result.Succeeded)
                {
                    return Unauthorized();
                }
                User user = await _userManager.FindByNameAsync(jwtLogin.UserName);
                IList<string> userRoles = await _userManager.GetRolesAsync(user);
                //ToDo token gen
                JwtSecurityToken jwtTokenData = GenerateJwtToken(user, userRoles);

                JwtToken jwtToken = new JwtToken() { Token = new JwtSecurityTokenHandler().WriteToken(jwtTokenData) };

                return Ok(jwtToken);
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Test()
        {
            return Ok("It works");
        }

        private JwtSecurityToken GenerateJwtToken(User user, IList<string> userRoles)
        {
            List<Claim> claims = User.Claims.ToList();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            int expiraionDays = _configuration.GetValue<int>("JWTConfiguration:TokenExpirationDays");
            byte[] signingKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey"));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
                audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expiraionDays),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256)
                );

            return jwtSecurityToken;
        }

    }
}
