using EhbOverFlow.APIModel;
using EhbOverFlow.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EhbOverFlow.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController :ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context=context;
            _signInManager=signInManager;
        }

       
        [Route("Login")]
        public async Task<ActionResult<Boolean>> Login([FromBody]LoginModel @login)
        {
            var result = await _signInManager.PasswordSignInAsync(@login.UserName, @login.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }
    }

}
