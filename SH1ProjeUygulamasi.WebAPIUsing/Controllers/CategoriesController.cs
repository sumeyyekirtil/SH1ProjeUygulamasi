using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly HttpClient _httpClient;

		public CategoriesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		static string _apiAdres = "http://localhost:5063/Api/";

		public async Task<IActionResult> IndexAsync(int? id)
		{
			if (id == null) //eğer adres çubuğundan id gelmezse
			{
				return BadRequest(); //feriye geçersiz istek hatası dön.
			}

			var model = await _httpClient.GetFromJsonAsync<List<Category>>($"{_apiAdres}Categories/{id}"); //istek atıldığında json gelicek, gelen json list of category e çevirecek
			if (model == null)
			{
				return NotFound();
			}
			var products = await _httpClient.GetFromJsonAsync<List<Product>>($"{_apiAdres}Products/GetProductsPageCategoryId/{id}");
			if (products is not null)
			{
				model.Products = products;
			}
			return View(model);
		}
	}
}
