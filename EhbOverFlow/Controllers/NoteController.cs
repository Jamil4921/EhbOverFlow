using EhbOverFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace EhbOverFlow.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Note());
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            return RedirectToAction("Index");
        }
    }
}
