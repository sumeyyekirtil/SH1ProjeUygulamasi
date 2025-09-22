using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.WebUI.Models;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

		public HomeController(DatabaseContext context)
		{
			_context = context;
		}

		public IActionResult Index()
        {
            var model = new HomePageViewModel
            {
                Sliders = _context.Sliders.ToList(),
                Products = _context.Products.Where(p => p.IsActive && p.IsHome).ToList()
            };
			return View(model); //toList eklenmez ise foreach d�ng�s�nde liste olmad��� i�in ��kt� vermez -> al�nan hata Invalid DbSet1 hatas�d�r
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
