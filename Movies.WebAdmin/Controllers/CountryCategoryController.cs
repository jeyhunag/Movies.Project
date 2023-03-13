using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movies.WebAdmin.Controllers
{
    public class CountryCategoryController : Controller
    {
        private readonly IGenericService<CountryCategoryDto, CountryCategory> _service;
        public CountryCategoryController(IGenericService<CountryCategoryDto, CountryCategory> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var countryCategories = await _service.GetListAsync();
            return View(countryCategories);
        }
        public async Task<IActionResult> Update(int id)
        {
            var countryCategories = await _service.GetByIdAsync(id);
            return View(countryCategories);

        }

        [HttpPost]
        public IActionResult Update(CountryCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var countryCategories = _service.Update(itemDto);
                if (countryCategories != null)
                {
                    TempData["success"] = "Country have been successfully changed.";
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
        public async Task<IActionResult> Create(CountryCategoryDto itemDto)
        {

            if (ModelState.IsValid)
            {
                var category = await _service.AddAsync(itemDto);
                if (category != null)
                {
                    TempData["success"] = "Country added successfully. ";
                    return RedirectToAction("Index");
                }
            }
            return View(itemDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);

            return View(category);

        }
        [HttpPost]
        public IActionResult Delete(CountryCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Country have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}
