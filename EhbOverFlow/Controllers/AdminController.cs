using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EhbOverFlow.Controllers
{
    [Authorize(Roles = "UserAdministrator")]

    public class AdminController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private INoteRepository _ehbOverFlowNote;



        public AdminController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote)
        {
            _logger = logger;
            _ehbOverFlowNote = ehbOverFlowNote;

        }
        public IActionResult Index()
		{
			return View();
		}
	}
}
