using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Forenzika.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Forenzika.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IConfiguration config;

        public AccountController(ILogger<AccountController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context, IConfiguration configuration)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
            this.config = configuration;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> test()
        {
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;            
            IdentityUser identityUser = await userManager.FindByIdAsync(userId);


            return Ok($"hello {identityUser.UserName}");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterForm registerForm)
        {             
            IdentityUser identityUser = await userManager.FindByEmailAsync(registerForm.Email);
            if (identityUser != null)
            {
                return BadRequest("Useri me kete email egziston");
            }
            if(!registerForm.Password.Equals(registerForm.PasswordConfirmation))
            {
                return BadRequest("Paswordi i konfirmuar eshte gabim.");
            }
            IdentityUser user = new IdentityUser
            {
                UserName = registerForm.Username,
                Email = registerForm.Email,
                EmailConfirmed = true
            };
            IdentityResult identityResult = await userManager.CreateAsync(user, registerForm.Password);
            if (identityResult.Succeeded)
            {
                return Ok(new { message = "Useri u krijua me sukses."});
            }
            else
            {
                return BadRequest(identityResult.Errors);
            }
            return BadRequest("Error creating user");
        }


        public async Task<IActionResult> GetToken([FromBody]Login login)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByEmailAsync(login.Email);
                SignInResult result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
                
                if (result.Succeeded)
                {
                    Claim[] claims = new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Sid, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        
                    };

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                    SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                        config["Tokens:Issuer"],
                        config["Tokens:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(60),
                        signingCredentials: credentials
                        );

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };
                    return Created("", results);
                }
            }

            return BadRequest();
        }

        public class Login
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class RegisterForm
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
        }
    }
}