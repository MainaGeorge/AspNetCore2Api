using System;
using System.Threading.Tasks;
using AspNetCoreApi.WebApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationModel registrationModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState.Values);

                var user = new IdentityUser
                {
                    UserName = registrationModel.Name,
                    Email = registrationModel.Email
                };

                var result = await _userManager.CreateAsync(user, registrationModel.Password);

                if (result.Succeeded)
                {
                    return Ok(user);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return BadRequest(ModelState.Values);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState.Values);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var signInResult = await _signInManager.PasswordSignInAsync(userName: signInModel.Username,
                password: signInModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
            {
                return Ok();
            }

            return BadRequest(ModelState);
        }
    }
}