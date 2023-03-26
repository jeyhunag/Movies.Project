using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.ViewModels;
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


        public IActionResult Index(int Id,int PageIndex=1, int pagSize=12)
        {
            var vm = new HomeViewModel();
            var movies =  _context.Movies.Include(p => p.GenresCategory).Include(p => p.CountryCategory)
                .Include(p => p.Trend).Include(p => p.LanguageCategory);
            vm.PagedViewModel = new PagedViewModel<MovieC>(movies, PageIndex, pagSize);

            vm.GenresCategories = _context.GenresCategories.ToList();
            vm.CountryCategories = _context.CountryCategories.ToList();
            vm.LanguageCategories = _context.LanguageCategories.ToList();

            return View(vm);

        }


    }
}
