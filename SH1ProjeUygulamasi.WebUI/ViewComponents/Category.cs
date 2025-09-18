using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;

namespace SH1ProjeUygulamasi.WebUI.ViewComponents
{
	public class Category : ViewComponent
	{
		private readonly DatabaseContext _context;

		public Category(DatabaseContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Categories);
		}
	}
}
