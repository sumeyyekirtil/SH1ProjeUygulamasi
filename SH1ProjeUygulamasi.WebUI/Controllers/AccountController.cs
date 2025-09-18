using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Data;
using System.Drawing;
using System.Security.Claims;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly DatabaseContext _context;

		public AccountController(DatabaseContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{

			return View();
		}

		[HttpPost]
		public IActionResult Login(string email, string password)
		{
			// Kullanıcı doğrulama işlemleri burada yapılacak (veritabanı kontrolü)
			var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
			if (user != null)
			{
				// Giriş başarılı, kullanıcıyı yönlendir
				var haklar = new List<Claim>() //kullanıcı hakları tanımladık
				{
					new(ClaimTypes.Email, user.Email), //claim = hak (kullanıcıya tanımlanan haklar)
						new(ClaimTypes.Role, "Admin")
				};
				var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); //kullanıcı için bir kimlik oluşturduk
				ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi); //bu sınıftan bir nesne oluşturup bilgilerde saklı haklar ile kural oluşturulabilir
				HttpContext.SignInAsync(claimsPrincipal); //yukarıdaki yetkilerle sisteme giriş yaptık
				return RedirectToAction("Index", "Home");
			}
			else
			{
				// Giriş başarısız, hata mesajı göster
				//ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre."; //2.yol
				@TempData["Message"] = "<div class='alert alert-danger'>Giriş Başarısız</div>";
			}
			return RedirectToAction("Login", "Account");
		}

		public ActionResult LogOut() //çıkış yap aktivasyonu : layout
		{
			HttpContext.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
