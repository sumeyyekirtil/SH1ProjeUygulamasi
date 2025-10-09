using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebAPIUsing.ViewComponents
{
	public class Category : ViewComponent
	{
		static string _apiAdres = "http://";
		HttpClient _httpClient = new();

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Sliders");
			return View(model);
		}
	}
}
