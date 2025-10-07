using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SH1ProjeUygulamasi.Core.Entities;
using System.Threading.Tasks;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")] //verilmezse sayfa bulunamıyor hatası alınır
	public class ProductsController : Controller
	{
		private readonly HttpClient _httpClient; //dependences injection ile yapıldı (inversion için)
		static string _apiAdres = "http://localhost:5063/Api/Products";
		static string _apiAdres2 = "http://localhost:5063/Api/Categories";

		public ProductsController(HttpClient httpClient)
		//httpclient resolve hatası için program cs ye service ekleme yapılır
		{
			_httpClient = httpClient;
		}

		// GET: ProductsController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			return View(model);
		}

		// GET: ProductsController/Details/5
		public async Task<ActionResult> DetailsAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: ProductsController/Create
		public async Task<ActionResult> CreateAsync()
		{
			await YukleAsync(); //dropdown için kategorileri getir
			return View();
		}

		// POST: ProductsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Product collection)
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

		// GET: ProductsController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			await YukleAsync();
			return View();
		}

		// POST: ProductsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, Product collection)
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

		// GET: ProductsController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: ProductsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Product collection)
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

		async Task YukleAsync()
		{
			var kategoriler = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres2);
			ViewBag.CategoryId = new SelectList(kategoriler, "Id", "Name");
		}
	}
}
