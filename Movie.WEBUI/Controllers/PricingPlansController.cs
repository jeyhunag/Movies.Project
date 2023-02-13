using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class PricingPlansController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
