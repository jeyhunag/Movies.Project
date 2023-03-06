using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movies.WebAdmin.Controllers
{
    public class TrandsController : Controller
    {
        private readonly IGenericService<TrandCategoryDto, Trends> _service;
        public TrandsController(IGenericService<TrandCategoryDto, Trends> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var trendsCategories = await _service.GetListAsync();
            return View(trendsCategories);
        }
        public async Task<IActionResult> Update(int id)
        {
            var trendsCategories = await _service.GetByIdAsync(id);
            return View(trendsCategories);

        }

        [HttpPost]
        public IActionResult Update(TrandCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var trendsCategories = _service.Update(itemDto);
                if (trendsCategories != null)
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
        public async Task<IActionResult> Create(TrandCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var trendsCategories = await _service.AddAsync(itemDto);
                if (trendsCategories != null)
                {
                    TempData["success"] = "Kateqoriya uğurla əlavə edildi.";
                    return RedirectToAction("Index");
                }
            }

            return View(itemDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var trendsCategories = await _service.GetByIdAsync(id);

            return View(trendsCategories);

        }
        [HttpPost]
        public IActionResult Delete(TrandCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Kateqoriya uğurla silindi.";
            return RedirectToAction("Index");
        }
    }
}
