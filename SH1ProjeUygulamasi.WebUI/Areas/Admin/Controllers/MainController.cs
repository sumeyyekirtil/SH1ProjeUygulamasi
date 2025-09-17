using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SH1ProjeUygulamasi.WebUI.Areas.Admin.Controllers
{
	[Authorize]
	[Area("Admin")]
	public class MainController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}