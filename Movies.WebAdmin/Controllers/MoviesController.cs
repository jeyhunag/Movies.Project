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

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        //private readonly ILogger<MoviesController> _logger;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MoviesController(AppDbContext context, IWebHostEnvironment webHostEnvironment/*, ILogger<MoviesController> logger*/)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            //_logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Movies.ToListAsync());
        }

        public IActionResult Create()
        {
      
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(10_000_000_000)]
        public async Task<IActionResult> Create([FromForm] MovieC movieC, List<IFormFile> files)
        {

            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(movieC.file.FileName);
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", uniqueFileName);

                // Check if the uploaded file is an image or video
                if (movieC.file.ContentType.StartsWith("image/"))
                {
                    // Save the image file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await movieC.file.CopyToAsync(stream);
                    }

                    movieC.Img = "/uploads/" + uniqueFileName; // Set the image file path
                }
                else if (movieC.file.ContentType == "video/mp4")
                {
                    // Save the video file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await movieC.file.CopyToAsync(stream);
                    }

                    movieC.MovieVideo = "16:9"; // Set the video proportions
                }
                else if (movieC.file.ContentType == "video/mp4")
                {
                    // Save the video file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await movieC.file.CopyToAsync(stream);
                    }

                    movieC.Trailer = "16:9"; // Set the video proportions
                }
                else
                {
                    // Invalid file type
                    ModelState.AddModelError("File", "The file must be an image or MP4 video.");
                    return View(movieC);
                }

                // Add the model to the database
                _context.Add(movieC);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(movieC);
        }

    }
}
