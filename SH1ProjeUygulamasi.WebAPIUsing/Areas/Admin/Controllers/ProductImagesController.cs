using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")] //verilmezse sayfa bulunamıyor hatası alınır
	public class ProductImagesController : Controller
	{
		private readonly HttpClient _httpClient; //dependences injection ile yapıldı (inversion için)

		static string _apiAdres = "http://localhost:5063/Api/ProductImages";
		static string _apiAdres2 = "http://localhost:5063/Api/Products";

		public ProductImagesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		async Task YukleAsync()
		{
			var liste = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres2);
			ViewBag.CategoryId = new SelectList(liste, "Id", "Name");
		}
		// GET: ProductImagesController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<ProductImage>>(_apiAdres); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			return View(model);
		}

		// GET: ProductImagesController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<ProductImage>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: ProductImagesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProductImagesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(ProductImage collection)
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
			await YukleAsync();
			return View(collection);
		}

		// GET: ProductImagesController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductImagesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, ProductImage collection)
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
			await YukleAsync();
			return View(collection);
		}

		// GET: ProductImagesController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductImagesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, ProductImage collection)
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
