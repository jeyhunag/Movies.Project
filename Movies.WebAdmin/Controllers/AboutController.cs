using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movies.WebAdmin.Controllers
{
    public class AboutController : Controller
    {
        private readonly IGenericService<AboutDto, About> _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imgPath = @"img/";

        public AboutController(IGenericService<AboutDto, About> service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var about = await _service.GetListAsync();
            return View(about);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(AboutDto aboutDto, IFormFile imageFile)
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
                    aboutDto.Img = imagePath;
                }
            }

            var about = await _service.AddAsync(aboutDto);
            if (about != null)
            {
                TempData["success"] = "About added successfully. ";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
            //}

            return View(aboutDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var about = await _service.GetByIdAsync(id);
            return View(about);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, AboutDto about, IFormFile imageFile)
        {
            if (id != about.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var imagePath = _imgPath + imageFile.FileName;
                        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                            about.Img = imagePath;
                        }
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutExists(about.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "About have been successfully changed.";
                _service.Update(about);
                return RedirectToAction(nameof(Index));
            }

            return View(about);
        }



        public async Task<IActionResult> Delete(int id)
        {
            var about = await _service.GetByIdAsync(id);

            return View(about);

        }
        [HttpPost]
        public IActionResult Delete(AboutDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "About have been successfully deleted.";
            return RedirectToAction("Index");
        }

        private bool AboutExists(int id)
        {
            throw new NotImplementedException();
        }

    }
}
