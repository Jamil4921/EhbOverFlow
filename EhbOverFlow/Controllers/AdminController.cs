using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EhbOverFlow.Controllers
{
    [Authorize(Roles = "UserAdministrator")]
    public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
