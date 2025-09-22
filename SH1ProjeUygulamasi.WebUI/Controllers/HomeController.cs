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
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context; //add parameters

		public HomeController(ILogger<HomeController> logger, DatabaseContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index(int id, Slider slider)
        {
            return View(_context.Sliders.Find(id));
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
