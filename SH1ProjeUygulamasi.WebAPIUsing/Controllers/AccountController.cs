using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Core.Models;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SH1ProjeUygulamasi.WebAPIUsing.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;

		public AccountController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		static string _apiAdres = "";

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{

			return View();
		}

	[HttpPost]
		public async Task<IActionResult> Login(UserLoginModel userLoginModel)
		{
			// Kullanıcı doğrulama işlemleri burada yapılacak (veritabanı kontrolü)
			//var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
			//var sonuc = await _httpClient.PostAsJsonAsync<User>();
			//if (ModelState.IsValid)
			//{
			//	//var kullanici = await sonuc.Content.ReadFromJsonAsync(_apiAdres + "Login");
			//	//var user = _userService.GetUser(u => u.Email == email && u.Password == password);
			//	if (user != null)
			//	{
			//		// Giriş başarılı, kullanıcıyı yönlendir
			//		var haklar = new List<Claim>() //kullanıcı hakları tanımladık
			//	{
			//		//new(ClaimTypes.Email, user.Email), //claim = hak (kullanıcıya tanımlanan haklar)
			//			new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User") //giriş yapan kullanıcı admin yetkisiyle değilse user yetkisiyle giriş yapsın.
			//	};
			//		var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); //kullanıcı için bir kimlik oluşturduk
			//		ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sınıftan bir nesne oluşturup bilgilerde saklı haklar ile kural oluşturulabilir
			//		HttpContext.SignInAsync(claimsPrincipal); //yukarıdaki yetkilerle sisteme giriş yaptık
			//		return RedirectToAction("Index", "Home");
			//	}
			//	else
			//	{
			//		// Giriş başarısız, hata mesajı göster
			//		ModelState.AddModelError("", "Giriş Başarısız!");
				//}
			//}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult LogOut() //çıkış yap aktivasyonu : layout
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public IActionResult Register()
		{
			return View();
		}

		public IActionResult AccessDenied() //varsayılan metod yolu açtık
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAsync(User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					user.IsActive = true;
					user.IsAdmin = false;
					user.CreateDate = DateTime.Now;
					var sonuc = await _httpClient.PostAsJsonAsync(_apiAdres + "Register", user);
					if (sonuc.IsSuccessStatusCode)
					{
						TempData["Message"] = @"<div class=""alert alert-success alert-dismissible fade show"" role=""alert"">
                     <strong>Kayıt İşlemi Başarılı! Giriş Yapabilirsiniz.!</strong>
                     <button type=""button"" class=""btn-close"" data-bs-dismiss=""alert"" aria-label=""Close""></button>
                     </div>";
						return RedirectToAction("Login", "Account");
					}
					ModelState.AddModelError("", "Kayıt Yapılamadı!");
								
				}
				catch (Exception)
				{
					ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu!");
				}
			}
			return View(user);
		}
	} 
}
