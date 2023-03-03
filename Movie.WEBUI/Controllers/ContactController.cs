using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movie.WEBUI.Controllers
{
	public class ContactController : Controller
	{
        private readonly IGenericService<ContactDto, Contact> _service;
        public ContactController(IGenericService<ContactDto, Contact> service)
        {
            _service = service;
        }
        public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Create(ContactDto itemDto)
        {

            if (ModelState.IsValid)
            {
                var category = await _service.AddAsync(itemDto);

                if (category != null)
                {
                    return RedirectToAction("Create");
                }
            }
            return View(itemDto);
        }
    }
}
