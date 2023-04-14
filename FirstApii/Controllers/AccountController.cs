using FirstApii.Dtos.UserDtos;
using FirstApii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            result = await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            result = await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
             return StatusCode(201);
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.UserName);
            if (user! == null) return StatusCode(409);
            user = new AppUser()
            {
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
            };
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);
            result = await _userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded) return BadRequest(result.Errors);


            return StatusCode(201); 
        }
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {

            return Ok(new {token=""});
        }
    }


}

