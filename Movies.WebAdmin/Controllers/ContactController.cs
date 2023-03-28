using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System.Net.Mail;
using System.Net;

namespace Movies.WebAdmin.Controllers
{
    public class ContactController : Controller
    {
        private readonly IGenericService<ContactDto, Contact> _service;
        public ContactController(IGenericService<ContactDto, Contact> service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {

            var contact = await _service.GetListAsync();
            return View(contact);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _service.GetByIdAsync(id);

            return View(category);

        }
        [HttpPost]
        public IActionResult Delete(ContactDto itemDto)
        {
            _service.Delete(itemDto.Id);
            TempData["success"] = "Kateqoriya uğurla silindi.";
            return RedirectToAction("Index");
        }
    }
}
