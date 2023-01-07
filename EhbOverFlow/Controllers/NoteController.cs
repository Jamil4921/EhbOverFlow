using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.FileManager;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using EhbOverFlow.ViewModels;
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
        private readonly IFileManager _fileManager;

        public NoteController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IFileManager fileManager)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;
            _context = context;
            _userManager = userManager;
            _fileManager = fileManager;

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
                return View(new NoteViewModel());
            }
            else
            {
   
                var note = _ehbOverFlowNote.GetNote((int)id);
               
                return View(new NoteViewModel{

                    Id = note.Id,
                    Title = note.Title,
                    Body = note.Body,
                    Solved = note.Solved,
                    UserId = note.UserId,
                    User = note.User
                });
            }

            

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate,Image")] NoteViewModel nvm)
        {

            var note = new Note
            {
                Id = nvm.Id,
                Title = nvm.Title,
                Body = nvm.Body,
                Solved = nvm.Solved,
                Image = await _fileManager.SaveImage(nvm.Image)
            };

            
            var user = await _userManager.GetUserAsync(HttpContext.User);

           
            note.User = user;

            if (note.Id > 0)
            {
                _ehbOverFlowNote.UpdateNote(note);
            }
            else
            {
                if (user.Notes == null)
                {
                    user.Notes = new List<Note>();
                }
                user.Notes.Add(note);
                _ehbOverFlowNote.AddNote(note);
            }

            if (await _ehbOverFlowNote.SaveChangesAsync())
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Solved(int id)
        {
            var note = _ehbOverFlowNote.GetNote(id);
            note.Solved = true;
            await _ehbOverFlowNote.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(".") + 1);
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

    }
}
