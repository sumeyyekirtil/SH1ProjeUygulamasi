using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.WebAPIUsing.Tools;
using static System.Net.Mime.MediaTypeNames;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")] //verilmezse sayfa bulunamıyor hatası alınır
	public class SlidersController : Controller
	{
		private readonly HttpClient _httpClient; //dependences injection ile yapıldı (inversion için)

		public SlidersController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		static string _apiAdres = "http://localhost:5063/Api/Sliders";

		// GET: SlidersController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			return View(model);
		}

		// GET: SlidersController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Slider>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: SlidersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SlidersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Slider collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
					{
						collection.Image = FileHelper.FileLoader(Image);
						var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
						if (response.IsSuccessStatusCode)
						{
							return RedirectToAction(nameof(Index));
						}
						ModelState.AddModelError("", "Kayıt Yapılamadı!");
					}
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: SlidersController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: SlidersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, Slider collection, IFormFile? Image)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (Image is not null)
					{
						collection.Image = FileHelper.FileLoader(Image);
						var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, collection);
						if (response.IsSuccessStatusCode)
						{
							return RedirectToAction(nameof(Index));
						}
						ModelState.AddModelError("", "Kayıt Yapılamadı!");
					}
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: SlidersController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: SlidersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Slider collection)
		{
			try
			{
				if (!string.IsNullOrEmpty(collection.Image)) //tekli işlemde gerek yok
					FileHelper.FileRemover(collection.Image);
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
