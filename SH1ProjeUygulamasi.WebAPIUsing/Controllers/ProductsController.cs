using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
	public class ProductsController : Controller
	{
		private readonly HttpClient _httpClient;

		public ProductsController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		static string _apiAdres = "http://localhost:5063/Api/Products";

		public async Task<IActionResult> IndexAsync(string q = "")
		{
			var products = await _httpClient.GetFromJsonAsync<List<Product>>($"{_apiAdres}Products/GetProductsBySearch/{q}");
			return View(products);
		}

		public async Task<IActionResult> DetailAsync(int? id)
		{
			//var model = _context.Products.FirstOrDefault(p => p.IsActive && p.Id == id);
			if (id is null)
			{
				return BadRequest("Geçersiz İstek!");
			}
			var products = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}Products/{id}");
			//if (model == null)
			//{
			//	return NotFound("Ürün Bulunamadı!");
			//}
			var productImages = await _httpClient.GetFromJsonAsync<List<ProductImage>>($"{_apiAdres}ProductImages/GetProductImagesByProductId/{id}");
			//model.ProductImages = productImages;
			return View();
		}
	}
}
