using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.WebUI.Tools;

namespace SH1ProjeUygulamasi.WebUI.Areas.Admin.Controllers
{
	[Authorize(Policy = "AdminPolicy")] //Rol yetkilendirmeli kullanım: sadece admin yetkisi ile giriş yapanlar bu ekranlara erişsin.
	//[Authorize]
	[Area("Admin")]
	public class UsersController : Controller
	{
		private readonly DatabaseContext _context;

		public UsersController(DatabaseContext context)
		{
			_context = context;
		}

		// GET: UsersController
		public ActionResult Index()
		{
			return View(_context.Users);
		}

		// GET: UsersController/Details/5
		public ActionResult Details(int id)
		{
			return View(_context.Categories.Find(id));
		}

		// GET: UsersController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UsersController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(User collection)
		{
			if (!ModelState.IsValid)
				return View(collection);
			try
			{
				_context.Users.Add(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				ModelState.AddModelError("", "Hata Oluştu!");
			}
			return View(collection);
		}

		// GET: UsersController/Edit/5
		public ActionResult Edit(int id)
		{
			return View(_context.Categories.Find(id));
		}

		// POST: UsersController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, User collection)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_context.Users.Update(collection);
					_context.SaveChanges();
					return RedirectToAction(nameof(Index));
				}
				catch
				{
					ModelState.AddModelError("", "Hata Oluştu!");
				}
			}
			return View(collection);
		}

		// GET: UsersController/Delete/5
		public ActionResult Delete(int id)
		{
			return View(_context.Categories.Find(id));
		}

		// POST: UsersController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				//_context.Users.Remove(collection);
				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
