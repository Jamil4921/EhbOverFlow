using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace EhbOverFlow.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NoteController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;
            _context = context;
            _userManager = userManager;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["UserId"] = user.Id;

            return View(await _context.notes.Include(n => n.User).ToListAsync());
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var note = _ehbOverFlowNote.GetNote(id);
            return View(note);
        }

        [HttpGet]
        [Authorize]

        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return View(new Note());
            }
            else
            {
   
                var note = _ehbOverFlowNote.GetNote((int)id);
               
               
                return View(note);
            }

            

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate")] Note note)
        {
           

            var user = await _userManager.GetUserAsync(HttpContext.User);
            
            note.User = user;

            if (note.Id > 0)
            {
                _ehbOverFlowNote.UpdateNote(note);
            }
            else
            {
                if(user.Notes == null)
                {
                    user.Notes = new List<Note>();
                }
                user.Notes.Add(note);
                _ehbOverFlowNote.AddNote(note);

            }

            if(await _ehbOverFlowNote.SaveChangesAsync())
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(note);
            }
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            _ehbOverFlowNote.RemoveNote(id);
            await _ehbOverFlowNote.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
