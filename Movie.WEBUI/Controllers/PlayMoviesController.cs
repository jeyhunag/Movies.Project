using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class PlayMoviesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
