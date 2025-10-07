﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")] //verilmezse sayfa bulunamıyor hatası alınır
	public class UsersController : Controller
	{
		private readonly HttpClient _httpClient; //dependences injection ile yapıldı (inversion için)
		static string _apiAdres = "http://localhost:5063/Api/Users";

		public UsersController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		// GET: UsersController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			return View(model);
		}

		// GET: UsersController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Slider>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: UsersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(User collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(Index));
					}
					ModelState.AddModelError("", "Kayıt Yapılamadı!");
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: UsersController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: UsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, User collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, collection);
					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction(nameof(Index));
					}
					ModelState.AddModelError("", "Kayıt Yapılamadı!");
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: UsersController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: UsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, User collection)
		{
			try
			{
				var response = await _httpClient.DeleteAsync(_apiAdres);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				}
				ModelState.AddModelError("", "İşlem Başarısız!");
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}
	}
}
