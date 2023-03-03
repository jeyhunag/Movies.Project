﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movie.WEBUI.Controllers
{
	public class AboutController : Controller
	{
        private readonly IGenericService<AboutDto, About> _service;

        public AboutController(IGenericService<AboutDto, About> service)
        {
            _service = service;
        }
        public async Task<IActionResult>  Index()
		{
            var data = await _service.GetListAsync();
            return View(data);
        }

	}
}
