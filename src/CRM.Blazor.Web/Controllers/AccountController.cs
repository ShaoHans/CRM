using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace CRM.Blazor.Web.Controllers;

public class AccountController : AbpControllerBase
{
    private readonly SignInManager<Volo.Abp.Identity.IdentityUser> _signInManager;

    public AccountController(SignInManager<Volo.Abp.Identity.IdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [HttpPost("/account/login")]
    public async Task<IActionResult> LoginAsync(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, true, true);
        if (result.Succeeded)
        {
            return Redirect("~/");
        }
        
        return Redirect($"~/Login?error=Invalid user or password:{result.GetResultAsString()}");
    }

    [HttpPost("/account/logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
