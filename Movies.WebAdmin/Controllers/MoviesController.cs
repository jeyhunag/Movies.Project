using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _movieService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";
        private readonly string _videoPath = @"Video/";
        private readonly string _trailerPath = @"Video/";

        public MoviesController(IMoviesService movieService, IWebHostEnvironment webHostEnvironment)
        {
            _movieService = movieService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var movie = await _movieService.GetListAsync();
            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            MovieCDto model = new()
            {
                CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync(),
                GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync(),
                LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync(),
                TrandCategoryDtos = await _movieService.GeTTrandsCategoriesAsync()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(MovieCDto movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {
            ModelState.Remove("Img");
            ModelState.Remove("MovieVideo");
            ModelState.Remove("Trailer");
            if (ModelState.IsValid)
            {

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = _imgPath + imageFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                        movie.Img = imagePath;
                    }
                }


                if (videoFile != null && videoFile.Length > 0)
                {
                    var videoPath = _videoPath + videoFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, videoPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                        movie.MovieVideo = videoPath;
                    }
                }


                if (trailerFile != null && trailerFile.Length > 0)
                {
                    var trailerPath = _trailerPath + trailerFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, trailerPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await trailerFile.CopyToAsync(stream);
                        movie.Trailer = trailerPath;
                    }
                }


                await _movieService.AddAsync(movie);


                return RedirectToAction("Index");
            }

            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            movie.CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync();
            movie.GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync();
            movie.LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync();
            movie.TrandCategoryDtos = await _movieService.GeTTrandsCategoriesAsync();



            return View(movie);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MovieCDto movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {

            if (id != movie.Id)
            {
                return NotFound();
            }
            //ModelState.Remove("Img");
            //ModelState.Remove("MovieVideo");
            //ModelState.Remove("Trailer");

            //if (ModelState.IsValid)
            //{

                if (imageFile != null && imageFile.Length > 0)
                {
                    var imagePath = _imgPath + imageFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                        movie.Img = imagePath;
                    }
                }

                if (videoFile != null && videoFile.Length > 0)
                {
                    var videoPath = _videoPath + videoFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, videoPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await videoFile.CopyToAsync(stream);
                        movie.MovieVideo = videoPath;
                    }
                }

                if (trailerFile != null && trailerFile.Length > 0)
                {
                    var trailerPath = _trailerPath + trailerFile.FileName;
                    var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, trailerPath);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await trailerFile.CopyToAsync(stream);
                        movie.Trailer = trailerPath;
                    }
                }

                _movieService.UpdateMovie(movie);
                TempData["success"] = "Movie have been successfully changed.";
                return RedirectToAction("Index");
            //}


            return View(movie);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetDetailByIdAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            movie.CountryCategoryDtos = await _movieService.GetCountryCategoriesAsync();
            movie.GenresCategoryDtos = await _movieService.GetGenresCategoriesAsync();
            movie.LanguageCategoryDtos = await _movieService.GetLangaugeCategoriesAsync();

            return View(movie);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            _movieService.Delete(id);
            return RedirectToAction("Index");
        }

    }

}