using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MyPortfolio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;

            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDefaultUser()
        {
            IdentityUser defaultUser = new IdentityUser()
            {
                UserName = "franco"
            };
            var result = await _userManager.CreateAsync(defaultUser, "Tmac90!");
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return Problem(detail: result.Errors.First().Description.ToString(),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials.");
            }
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid login attempt");
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.Id),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.UserName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyPortfolioSecretKey123456Borty!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "MyPortfolio",
                audience: "MyPortfolio",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
