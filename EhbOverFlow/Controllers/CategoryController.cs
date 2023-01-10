using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.FileManager;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using EhbOverFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EhbOverFlow.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;
        private ICategoryRepository _ehbOverFlowCategory;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileManager _fileManager;

        public CategoryController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IFileManager fileManager, ICategoryRepository ehbOverFlowCategory)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;
            _context = context;
            _userManager = userManager;
            _fileManager = fileManager;
            _ehbOverFlowCategory=ehbOverFlowCategory;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var isAdmin = await _userManager.IsInRoleAsync(user, "UserAdministrator");

            ViewData["IsAdmin"] = isAdmin;


            var categories = _ehbOverFlowCategory.GetAllCategories();
            return View(await _context.Categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = _ehbOverFlowCategory.GetCategory(id);
            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = "UserAdministrator")]
        public IActionResult Create(int? id)
        {


            if (id == null)
            {
                return View(new Category());
            }
            else
            {

                var category = _ehbOverFlowCategory.GetCategory((int)id);

                return View(category);
            }

        }
        [HttpPost]
        [Authorize(Roles = "UserAdministrator")]
        public async Task<IActionResult> Create([Bind("Id,SubjectName")] Category category)
        {

            if(category.Id > 0)
            {
                _ehbOverFlowCategory.UpdateCategory(category);
            }
            else
            {
                _ehbOverFlowCategory.AddCategory(category);
            }

            
            if(await _ehbOverFlowCategory.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {

            _ehbOverFlowCategory.RemoveCategory(id);
            await _ehbOverFlowCategory.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
