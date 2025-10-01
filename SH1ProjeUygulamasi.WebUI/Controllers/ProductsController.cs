﻿using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;

namespace SH1ProjeUygulamasi.WebUI.Controllers
{
	public class ProductsController : Controller
	{
		//private readonly DatabaseContext _context;

		//public ProductsController(DatabaseContext context)
		//{
		//	_context = context;
		//}

		private readonly IProductService _productService;

		public ProductsController(ProductService productService)
		{
			_productService = productService;
		}

		public IActionResult Index(string q = "")
		{
			//var model = _context.Products.Where(p => p.IsActive && p.Name.Contains(q)).ToList();
			var model = _productService.GetProducts(p => p.IsActive && p.Name.Contains(q));
			return View(model);
		}
		
		public IActionResult Detail(int? id)
		{
			//var model = _context.Products.FirstOrDefault(p => p.IsActive && p.Id == id);
			if (id is null)
			{
				return BadRequest("Geçersiz İstek!");
			}
			var model = _productService.GetProductByCategoryAndProductImages(id.Value);
			if (model == null)
			{
				return NotFound("Ürün Bulunamadı!");
			}
			return View(model);
		}
	}
}
