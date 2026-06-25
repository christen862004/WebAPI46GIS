using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI46GIS.DTO;
using WebAPI46GIS.Models;

namespace WebAPI46GIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Regsiter(RegisterUserDTO userFromReq)
        {
            if (ModelState.IsValid)
            {
                //create 
                ApplicationUser appUser = new ApplicationUser()
                {
                    UserName = userFromReq.UserName,
                    PasswordHash = userFromReq.Password,
                    Email = userFromReq.Email
                };
                IdentityResult result=await userManager.CreateAsync(appUser, userFromReq.Password);
                if(result.Succeeded)
                {
                    return Ok("User add Success");
                }
                foreach (var errorItem in result.Errors)
                {
                    ModelState.AddModelError("", errorItem.Description);
                }
            }
            //bad request
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDTO userFromreq)
        {
            if (ModelState.IsValid)
            {
                //check valiad
                ApplicationUser appUser=await  userManager.FindByNameAsync(userFromreq.UserName);
                if (appUser != null) {
                    bool found=await userManager.CheckPasswordAsync(appUser, userFromreq.Password);
                    if (found)
                    {
                        #region Generate Token
                        //1- define Claims to set in jwt payload
                        List<Claim> extraClaim=new List<Claim>();

                        extraClaim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        extraClaim.Add(new Claim(ClaimTypes.Name, appUser.UserName));
                        extraClaim.Add(new Claim(ClaimTypes.NameIdentifier, appUser.Id));

                        var roles =await userManager.GetRolesAsync(appUser);
                        foreach (var role in roles)
                        {
                            extraClaim.Add(new Claim(ClaimTypes.Role, role));

                        }
                        //2- define SigningCredentials
                        string key = configuration["JWT:Key"];
                        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));

                        SigningCredentials mySigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        //design token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: configuration["JWT:Iss"],
                            audience: configuration["JWT:Aud"],
                            expires: DateTime.Now.AddHours(1),
                            claims: extraClaim,
                            signingCredentials: mySigningCredentials

                            );
                        //generate compact token
                        return Ok(new
                        {
                            expired = DateTime.Now.AddHours(1),
                            token=new JwtSecurityTokenHandler().WriteToken(mytoken)
                        });
                        #endregion
                    }
                }
                ModelState.AddModelError("", "Invalid Account");
            }
            return BadRequest(ModelState);
        }
    }
}
