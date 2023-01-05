using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Data.Repository;
using EhbOverFlow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EhbOverFlow.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private INoteRepository _ehbOverFlowNote;

		

		public HomeController(ILogger<HomeController> logger, INoteRepository ehbOverFlowNote)
		{
			_logger = logger;
			_ehbOverFlowNote = ehbOverFlowNote;

		}

		public IActionResult Index()
		{
			return View();
		}

		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}