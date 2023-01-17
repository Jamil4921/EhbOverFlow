using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.FileManager;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using EhbOverFlow.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace EhbOverFlow.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;
        private ICategoryRepository _ehbOverFlowCategory;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileManager _fileManager;

        public NoteController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote, ApplicationDbContext context, UserManager<ApplicationUser> userManager, IFileManager fileManager, ICategoryRepository ehbOverFlowCategory)
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
        public async Task<IActionResult> Index(string sort, string searchField)
        {
            
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewData["UserId"] = user.Id;

            var isAdmin = await _userManager.IsInRoleAsync(user, "UserAdministrator");

            ViewData["IsAdmin"] = isAdmin;

            ViewData["GetTitleNote"] = searchField;
            var noteQuery = from n in _context.notes select n;

            if (!String.IsNullOrEmpty(searchField))
            {
                noteQuery = noteQuery.Where(n => n.Title.Contains(searchField) || n.Body.Contains(searchField)).Include(n => n.User).Include(c => c.Category);
                return View(await noteQuery.AsNoTracking().ToListAsync());
            }

            IQueryable<Note> notes;
            if (sort == "solved")
            {
                notes = _context.notes.Where(n => n.Solved == true).Include(n => n.User).Include(c => c.Category);
                return View(await notes.ToListAsync());
            }

            if (sort == "unsolved")
            {
                notes = _context.notes.Where(n => n.Solved == false).Include(n => n.User).Include(c => c.Category);
                return View(await notes.ToListAsync());
            }

            if (sort == "allnotes")
            {
                
                notes = _context.notes.Include(n => n.User).Include(c => c.Category);
                return View(await notes.ToListAsync());
            }

            if (sort == "recent")
            {
                notes = _context.notes.Include(n => n.User).OrderByDescending(n => n.CreatedDate).Include(c => c.Category);
                return View(await notes.ToListAsync());
            }

            else
            {
                notes = _context.notes;
            }
            return View(await _context.notes.Include(n => n.User).Include(c =>c.Category).ToListAsync());

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

            

            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "SubjectName");
            var selectedCategory = _context.Categories.Find(id);


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
                    CurrentImage = note.Image,
                    UserId = note.UserId,
                    User = note.User,
                    CategoryId = selectedCategory?.Id
                });
            }
           

        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,CreatedDate,Image,CategoryId")] NoteViewModel nvm)
        {

            
            ViewData["CategoryId"] = new SelectList(_context.Set<Category>(), "Id", "SubjectName");

            var note = new Note
            {
                Id = nvm.Id,
                Title = nvm.Title,
                Body = nvm.Body,
                Solved = nvm.Solved,
                UserName = nvm.UserName,
                CategoryId = nvm.CategoryId


            };

            
       
            if(nvm.Image == null)
            {
                note.Image = nvm.CurrentImage;
            }
            else
            {
                note.Image = await _fileManager.SaveImage(nvm.Image);
            }

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
        [HttpPost]
        public async Task<IActionResult> Comment([Bind("NoteId,MainCommentId,Created,Message")] CommentViewModel cvm)
        {
            

            var note = _ehbOverFlowNote.GetNote(cvm.NoteId);
            
            if (cvm.MainCommentId == 0)
            {
                note.MainComments = note.MainComments ?? new List<MainComment>();
                
                var mainComment = new MainComment
                {
                    Message = cvm.Message,
                    Created = DateTime.Now,
                 
                };

                if (User.Identity.IsAuthenticated)
                {
                    mainComment.UserName = User.Identity.Name;
                }
                note.MainComments.Add(mainComment);
                _ehbOverFlowNote.UpdateNote(note);
            }
            else
            {
                var comment = new SubComment
                {
                    MainCommentId = cvm.MainCommentId,
                    Message = cvm.Message,
                    Created = DateTime.Now,

                };

                if (User.Identity.IsAuthenticated)
                {
                    comment.UserName = User.Identity.Name;
                }

                _ehbOverFlowNote.AddSubComment(comment);
            }
            await _ehbOverFlowNote.SaveChangesAsync();

            return RedirectToAction("Details", new { id = cvm.NoteId });
        }
        [HttpGet]
        public async Task<IActionResult> Like(int id)
        {
           
            var comment = await _context.mainComments.FindAsync(id);
            comment.Like += 1;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

            //if (comment == null)
            //{
            //    return NotFound();
            //}

            //comment.Like += 1;

            //_context.Update(comment);
            //await _context.SaveChangesAsync();

            //return RedirectToAction("Index");

            //var note = _ehbOverFlowNote.GetNote(id);
            //note.Solved = true;
            //await _ehbOverFlowNote.SaveChangesAsync();
            //return RedirectToAction("Index");

        }
    }
}
