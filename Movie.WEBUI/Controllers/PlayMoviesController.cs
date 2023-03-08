using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.Controllers
{
	public class PlayMoviesController : Controller
	{
        private readonly AppDbContext _context;

        public PlayMoviesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            return View(movie);
        }
    }
}
