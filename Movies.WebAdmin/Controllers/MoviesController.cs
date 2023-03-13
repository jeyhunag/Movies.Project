using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"Img/";
        private readonly string _videoPath = @"Video/";
        private readonly string _trailerPath = @"Video/";

        public MoviesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CountryCategoryId"] = new SelectList(_context.CountryCategories, "Id", "Name");
            ViewData["GenresCategoryId"] = new SelectList(_context.GenresCategories, "Id", "Name");
            ViewData["LanguageCategoryId"] = new SelectList(_context.LanguageCategories, "Id", "Name");
            ViewData["TrendId"] = new SelectList(_context.Trends, "Id", "Name");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(MovieC movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {

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


            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            TempData["success"] = "Movie added successfully. ";
            return RedirectToAction("Index");
            //}
            ViewData["CountryCategoryId"] = new SelectList(_context.CountryCategories, "Id", "Name", movie.CountryCategoryId);
            ViewData["GenresCategoryId"] = new SelectList(_context.GenresCategories, "Id", "Name", movie.GenresCategoryId);
            ViewData["LanguageCategoryId"] = new SelectList(_context.LanguageCategories, "Id", "Name", movie.LanguageCategoryId);
            ViewData["TrendId"] = new SelectList(_context.Trends, "Id", "Name", movie.TrendId);
            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["CountryCategoryId"] = new SelectList(_context.CountryCategories, "Id", "Name");
            ViewData["GenresCategoryId"] = new SelectList(_context.GenresCategories, "Id", "Name");
            ViewData["LanguageCategoryId"] = new SelectList(_context.LanguageCategories, "Id", "Name");
            ViewData["TrendId"] = new SelectList(_context.Trends, "Id", "Name");
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieC movie, IFormFile imageFile, IFormFile videoFile, IFormFile trailerFile)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            try
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
                TempData["success"] = "Movie have been successfully changed.";
                _context.Update(movie);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}

            ViewData["CountryCategoryId"] = new SelectList(_context.CountryCategories, "Id", "Name", movie.CountryCategoryId);
            ViewData["GenresCategoryId"] = new SelectList(_context.GenresCategories, "Id", "Name", movie.GenresCategoryId);
            ViewData["LanguageCategoryId"] = new SelectList(_context.LanguageCategories, "Id", "Name", movie.LanguageCategoryId);
            ViewData["TrendId"] = new SelectList(_context.Trends, "Id", "Name", movie.TrendId);
            return View(movie);
        }



        public IActionResult Delete(int? id)
        {
            MovieC movie = _context.Movies.Where(p => p.Id == id).FirstOrDefault();
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            TempData["success"] = "Movie have been successfully deleted.";
            return RedirectToAction("Index");
        }

        private bool MovieExists(int id)
        {
            throw new NotImplementedException("Yalnis melumat daxil etdiniz.");
        }
    }

}