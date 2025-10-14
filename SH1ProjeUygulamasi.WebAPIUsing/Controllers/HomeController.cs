using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.WebAPIUsing.Models;
using SH1ProjeUygulamasi.WebAPIUsing.Tools;
using System.Diagnostics;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
	public class HomeController : Controller
	{

		private readonly HttpClient _httpClient;

		public HomeController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		static string _apiAdres = "http:";

		public async Task<IActionResult> IndexAsync()
		{
			var model = new HomePageViewModel
			{
				Sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders"),
				Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + " Products/GetHomePageProducts")
			};
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}
		
		public IActionResult JsileAPIKullanimOrnegi()
		{
			return View();
		}
		
		public IActionResult Detail(int? id)
		{
			if (id is null)
			{
				return BadRequest("Ge�ersiz �stek!");
			}
			return View();
		}

		[HttpPost]
		public IActionResult ContactUs(string nameSurname, string email, string message)
		{
			string mesaj = $"Ad Soyad:  {nameSurname} <hr> E mail:  {email} <hr> Mesaj:  {message}";
			try
			{
				MailHelper.SendMail("mail@gmail.com", "Siteden email geldi", message);
				TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                     <strong>Te�ekk�rler! Mesaj�n�z �letildi!</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
			catch (Exception)
			{
				TempData["Message"] = @"<div class=""alert alert-danger alert-dismissible fade show"" role=""alert"">
                     <strong>Hata Olu�tu! Mesaj g�nderilemedi</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
			}
			return RedirectToAction("ContactUs");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
