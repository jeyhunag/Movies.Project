using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using Serilog;
using static System.Formats.Asn1.AsnWriter;

namespace Movies.WebAdmin.Controllers
{
    public class GenresCategoryController : Controller
    {
        private readonly IGenericService<GenresCategoryDto, GenresCategory> _service;
        public GenresCategoryController(IGenericService<GenresCategoryDto, GenresCategory> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {

            var genresCategories = await _service.GetListAsync();
            return View(genresCategories);
        }
        public async Task<IActionResult> Update(int id)
        {
            var genresCategories = await _service.GetByIdAsync(id);
            return View(genresCategories);

        }

        [HttpPost]
        public async Task<IActionResult> Update(GenresCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var genresCategories = _service.Update(itemDto);

                if (genresCategories != null)
                {
                    TempData["success"] = "Kateqoriya uğurla dəyişdirildi.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);

        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenresCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var genresCategories = await _service.AddAsync(itemDto);
                if (genresCategories != null)
                {
                    TempData["success"] = "Kateqoriya uğurla əlavə edildi.";
                    return RedirectToAction("Index");
                }
            }
          
            return View(itemDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var genresCategories = await _service.GetByIdAsync(id);

            return View(genresCategories);

        }
        [HttpPost]
        public IActionResult Delete(GenresCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Kateqoriya uğurla silindi.";
            return RedirectToAction("Index");
        }

    }
}
