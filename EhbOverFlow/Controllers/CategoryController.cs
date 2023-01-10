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
    [Authorize(Roles = "UserAdministrator")]
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

        public async Task<IActionResult> Index()
        {
            var categories = _ehbOverFlowCategory.GetAllCategories();
            return View(await _context.Categories.ToListAsync());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = _ehbOverFlowCategory.GetCategory(id);
            return View(category);
        }

        [HttpPost]
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
    }
}
