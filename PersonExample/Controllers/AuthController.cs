using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonExample.Auth;
using PersonExample.DTOs;
using PersonExample.Entities;

namespace PersonExample.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService) : ControllerBase
{
    [HttpGet("roles")]
    public ActionResult<IEnumerable<string>> GetRoles()
    {
        return Ok(new[] { Roles.Admin, Roles.Guest });
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto request)
    {
        if (request.Role != Roles.Admin && request.Role != Roles.Guest)
            return BadRequest($"Invalid role. Use '{Roles.Admin}' or '{Roles.Guest}'.");

        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await userManager.AddToRoleAsync(user, request.Role);

        var token = tokenService.CreateToken(user, request.Role);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized("Invalid credentials.");

        var roles = await userManager.GetRolesAsync(user);
        var token = tokenService.CreateToken(user, roles.First());
        return Ok(new { token });
    }
}
