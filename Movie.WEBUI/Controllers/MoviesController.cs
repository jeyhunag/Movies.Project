using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.DAL.DbModel;


namespace Movie.WEBUI.Controllers
{
	public class MoviesController : Controller
	{
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int Id)
        {

            return View(await _context.Movies.Include(p => p.GenresCategory).Include(p => p.CountryCategory)
                .Include(p => p.Trend).Include(p => p.LanguageCategory).ToListAsync());

        }


    }
}
