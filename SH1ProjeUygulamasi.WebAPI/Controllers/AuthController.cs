using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Core.Models;
using SH1ProjeUygulamasi.Data;
using System.Threading.Tasks;

namespace SH1ProjeUygulamasi.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly DatabaseContext _context;

		public AuthController(DatabaseContext context)
		{
			_context = context;
		}

		// POST api/<AuthController>
		[HttpPost("Login")]
		public async Task<ActionResult<User>> Login([FromBody] UserLoginModel userLoginModel)
		{
			var account = await _context.Users.FirstOrDefaultAsync(u => u.IsActive && u.Email == userLoginModel.Email && u.Password == userLoginModel.Password);


			return account == null ? NotFound() : Ok(account);
		}
		
		[HttpPost("Register")]
		public async Task<ActionResult<User>> Register([FromBody] User user)
		{
			var account = await _context.Users.FirstOrDefaultAsync(u => u.IsActive && u.Email == user.Email);

			if (account is not null)
			{
				return Conflict(new {errMes = account.Email + " ile daha önce kayıt var, Giriş ekranından girişinizi gerçekleştirebilirsiniz ^_^"}); //kayıtlarda çakışma durumunda kullanılabilen geri dönüş türü
			}
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return Ok(account);
		}
		
	}
}
