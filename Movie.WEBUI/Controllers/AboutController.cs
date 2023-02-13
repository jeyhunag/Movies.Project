using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
