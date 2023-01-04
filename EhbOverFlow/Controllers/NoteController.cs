using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EhbOverFlow.Controllers
{
    public class NoteController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;


        public NoteController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;
        }
        public IActionResult Index()
        {
            var notes = _ehbOverFlowNote.GetAllNotes();
            return View(notes);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var note = _ehbOverFlowNote.GetNote(id);
            return View(note);
        }

        [HttpGet]
        public IActionResult Create(int? id)
        {
            if(id == null)
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
        public async Task<IActionResult> Create(Note note)
        {
            if(note.Id > 0)
            {
                _ehbOverFlowNote.UpdateNote(note);
            }
            else
            {
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
    }
}
