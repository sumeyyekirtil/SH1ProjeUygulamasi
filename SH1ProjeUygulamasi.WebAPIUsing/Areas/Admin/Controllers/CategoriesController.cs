using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using System.Threading.Tasks;

namespace SH1ProjeUygulamasi.WebAPIUsing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		static string _apiAdres = "http://localhost:5063/api/Categories";
		HttpClient _httpClient = new HttpClient(); //.net framework deki yapıyı kullanarak
		// GET: CategoriesController
		public async Task<ActionResult> Index()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			return View(model);
		}

		// GET: CategoriesController/Details/5
		public async Task<ActionResult> Details(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// GET: CategoriesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateAsync(Category collection)
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

		// GET: CategoriesController/Edit/5
		public async Task<ActionResult> EditAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: CategoriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int id, Category collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var response = await _httpClient.PutAsJsonAsync(_apiAdres, collection);
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

		// GET: CategoriesController/Delete/5
		public async Task<ActionResult> DeleteAsync(int id)
		{
			var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");
			return View(model);
		}

		// POST: CategoriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteAsync(int id, Category collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var response = await _httpClient.DeleteAsync(_apiAdres);
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
	}
}
