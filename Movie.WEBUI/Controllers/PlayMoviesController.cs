using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.ViewModels;
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
            var vm = new HomeViewModel();
            vm.MovieC = _context.Movies.FirstOrDefault(x => x.Id == id);
            return View(vm);
        }
    }
}
