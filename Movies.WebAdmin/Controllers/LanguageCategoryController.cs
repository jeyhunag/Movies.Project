using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movies.WebAdmin.Controllers
{
    public class LanguageCategoryController : Controller
    {
        private readonly IGenericService<LanguageCategoryDto, LanguageCategory> _service;
        public LanguageCategoryController(IGenericService<LanguageCategoryDto, LanguageCategory> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var languageCategories = await _service.GetListAsync();
            return View(languageCategories);
        }
        public async Task<IActionResult> Update(int id)
        {
            var languageCategories = await _service.GetByIdAsync(id);
            return View(languageCategories);

        }

        [HttpPost]
        public IActionResult Update(LanguageCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var languageCategories = _service.Update(itemDto);
                if (languageCategories != null)
                {
                    TempData["success"] = "Language have been successfully changed.";
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
        public async Task<IActionResult> Create(LanguageCategoryDto itemDto)
        {
            if (ModelState.IsValid)
            {
                var languageCategories = await _service.AddAsync(itemDto);
                if (languageCategories != null)
                {
                    TempData["success"] = "Language added successfully. ";
                    return RedirectToAction("Index");
                }
            }
        
            return View(itemDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var languageCategories = await _service.GetByIdAsync(id);

            return View(languageCategories);

        }
        [HttpPost]
        public IActionResult Delete(LanguageCategoryDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Language have been successfully deleted.";
            return RedirectToAction("Index");
        }
    }
}
