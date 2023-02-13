using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class MoviesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
