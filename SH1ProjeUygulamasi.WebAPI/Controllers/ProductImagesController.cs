using Microsoft.AspNetCore.Mvc;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImagesController : ControllerBase
	{
		private readonly IService<ProductImage> _service; //entity eklenmesi lazım class a ypksa tanımlama gerçekleşmez

		public ProductImagesController(IService<ProductImage> service)
		{
			_service = service;
		}

		// GET: api/<ProductImagesController>
		[HttpGet]
		public async Task<IEnumerable<ProductImage>> Get()
		{
			//içeride asenkron metot çalışıyorsa isim asenkron olmalı
			//task görev demek
			//actionresult not found - ok dönmeye yarar
			//actionresult içindeki class geriye dönen nesne demek
			return await _service.GetAllAsync();
		}

		// GET api/<ProductImagesController>/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductImage>> Get(int id)
		{
			var model = await _service.FindAsync(id);
			if (model == null) //gönderilen ile eşleşen kayıt bulunamazsa notfound döner
			{
				return NotFound();
			}
			return Ok(model);
		}

		[HttpGet("GetProductImagesByProductId/{id}")]
		public async Task<IEnumerable<ProductImage>> GetProductImagesByProductId(int id)
		{
			//başka sayfada eksiltili çağıracağımız farklı seçimli metot yazılır
			return await _service.GetAllAsync(p => p.ProductId == id);
		}

		// POST api/<ProductImagesController>
		[HttpPost]
		public async Task<ActionResult<ProductImage>> PostAsync([FromBody] ProductImage value)
		{
			await _service.AddAsync(value);
			await _service.SaveChangesAsync();
			return Ok(value);
		}

		// PUT api/<ProductImagesController>/5
		[HttpPut("{id}")]
		public async Task<ActionResult<ProductImage>> PutAsync(int id, [FromBody] ProductImage value)
		{
			_service.Update(value);
			await _service.SaveChangesAsync();
			return Ok(value); //action result dan sonra metot hatasını çözer
		}

		// DELETE api/<ProductImagesController>/5
		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAsync(int id) //return - notfound hatası için actionresult kullanımı
		{
			var model = await _service.FindAsync(id);
			if (model == null) //gönderilen ile eşleşen kayıt bulunamazsa notfound döner
			{
				return NotFound();
			}
			_service.Delete(model);
			await _service.SaveChangesAsync();
			return Ok(model);
		}
	}
}
