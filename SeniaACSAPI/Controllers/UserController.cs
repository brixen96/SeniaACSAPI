using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeniaACSAPI.Data;
using SeniaACSAPI.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeniaACSAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;

        public UserController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDTOCreate userDto)
        {

            var user = new IdentityUser
            {
                Email = userDto.Email,
                UserName = userDto.Email
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if(result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                return Ok();
            }
            
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDTOLogin request)
        {

            try
            {
                var user = userManager.FindByEmailAsync(request.Email);


                if (user == null)
                {
                    return BadRequest("Invalid login");
                }

                string key = configuration.GetValue<string>("Jwt:Key");
                var issuer = configuration.GetValue<string>("Jwt:Issuer");
                var audience = configuration.GetValue<string>("Jwt:Audience");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim("Email", request.Email.ToString()));


                var token = new JwtSecurityToken(issuer,
                                audience,
                                claims,
                                expires: DateTime.Now.AddDays(31),
                                signingCredentials: credentials);
                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(jwt_token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

            return BadRequest();
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(context.Users.ToList());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await userManager.FindByIdAsync(id));
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                return Ok(await userManager.DeleteAsync(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            
        }

        // EDIT
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromBody] UserDTOCreate userDto)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                await userManager.SetPhoneNumberAsync(user, userDto.PhoneNumber);
                await userManager.SetEmailAsync(user, userDto.Email);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
