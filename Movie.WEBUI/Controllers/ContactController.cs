using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
