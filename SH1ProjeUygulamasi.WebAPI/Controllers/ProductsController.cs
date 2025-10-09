﻿using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IService<Product> _service;

		public ProductsController(IService<Product> service)
		{
			_service = service;
		}

		// GET: api/<ProductsController>
		[HttpGet]
		public async Task<IEnumerable<Product>> Get()
		{
			return await _service.GetAllAsync();
		}
		[HttpGet("GetHomePageProducts")]
		public async Task<IEnumerable<Product>> GetHomePageProducts()
		{
			//başka sayfada eksiltili çağıracağımız farklı seçimli metot yazılır
			return await _service.GetAllAsync(p => p.IsActive && p.IsHome);
		}

		[HttpGet("GetProductsPageCategoryId/{id}")]
		public async Task<IEnumerable<Product>> GetProductsPageCategoryId(int id)
		{
			//başka sayfada eksiltili çağıracağımız farklı seçimli metot yazılır
			return await _service.GetAllAsync(p => p.IsActive && p.CategoryId == id);
		}

		[HttpGet("GetProductsBySearch/{q}")]
		public async Task<IEnumerable<Product>> GetProductsBySearch(string q)
		{
			//başka sayfada eksiltili çağıracağımız farklı seçimli metot yazılır
			return await _service.GetAllAsync(p => p.IsActive && p.Name.Contains(q));
		}

		// GET api/<ProductsController>/5
		[HttpGet("{id}")]
		public async Task<Product> GetAsync(int id)
		{
			return await _service.FindAsync(id);
		}

		// POST api/<ProductsController>
		[HttpPost]
		public async Task PostAsync([FromBody] Product value)
		{
			await _service.AddAsync(value);
			await _service.SaveChangesAsync();
		}

		// PUT api/<ProductsController>/5
		[HttpPut("{id}")]
		public async Task PutAsync(int id, [FromBody] Product value)
		{
			_service.Update(value);
			await _service.SaveChangesAsync();
		}

		// DELETE api/<ProductsController>/5
		[HttpDelete("{id}")]
		public async Task DeleteAsync(int id)
		{
			var kayit = await GetAsync(id);
			_service.Delete(kayit);
			await _service.SaveChangesAsync();
		}
	}
}
