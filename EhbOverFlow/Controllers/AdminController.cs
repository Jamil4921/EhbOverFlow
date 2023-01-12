using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.FileManager;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EhbOverFlow.Controllers
{
    [Authorize(Roles = "UserAdministrator")]

    public class AdminController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;
        private ICategoryRepository _ehbOverFlowCategory;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFileManager _fileManager;
        private readonly IUserRepository _userRepository;

        public AdminController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IFileManager fileManager, ICategoryRepository ehbOverFlowCategory, IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;
            _context = context;
            _userManager = userManager;
            _fileManager = fileManager;
            _ehbOverFlowCategory=ehbOverFlowCategory;
            _userRepository=userRepository;
            _roleManager=roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            var allUsers = from u in _context.Users
                           join ur in _context.UserRoles on u.Id equals ur.UserId
                           select new UserViewModel { User = u, Role = ur.RoleId, IsAdmin = ur.RoleId == "UserAdministrator" };

            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData["UserId"] = user.Id;


            return View(await allUsers.ToListAsync());
		}

        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            _userRepository.RemoveUser(id);
            await _userRepository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> AdminRole(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            var adminRole = await _roleManager.FindByNameAsync("UserAdministrator");
            var previousRole = await _userManager.GetRolesAsync(user);
            if (previousRole.Count > 0)
            {
                await _userManager.RemoveFromRoleAsync(user, previousRole.First());
            }
            var result = await _userManager.AddToRoleAsync(user, adminRole.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error", result.Errors);
            }
        }
    }
}
