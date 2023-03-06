using Microsoft.AspNetCore.Mvc;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.Controllers
{
	public class MoviesController : Controller
	{
		public IActionResult Index(string? genres, GenresCategory genresCategory)
		{
			//var movies = db.movies.tolist();
			//if(genresCategory != null)
			//{
			//	movies = movies.where(p= prop.genresId == genresCategory.Id).tolist
			//}
			return View();
		}
        public IActionResult NewMovie(string? genres)
        {
            return View();
        }

    }
}
