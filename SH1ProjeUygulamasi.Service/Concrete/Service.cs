using Microsoft.EntityFrameworkCore;
using SH1ProjeUygulamasi.Core.Entities;
using SH1ProjeUygulamasi.Data;
using SH1ProjeUygulamasi.Service.Abstract;
using System.Linq.Expressions;


namespace SH1ProjeUygulamasi.Service.Concrete
{
	//T ile generic yapı kuruldu - dışarıdan bir parametre alınıyor sınır belirlendi
	//generic - genel yapı c# olan bir kural
	public class Service<T> : IService<T> where T : class, IEntity, new()
	{
		public readonly DatabaseContext _context;

		public Service(DatabaseContext context)
		{
			_context = context;
		}

		//her metodun asenkronu yok yük getirdiği için
		public void Add(T entity)
		{
			_context.Add(entity);
		}

		public async Task AddAsync(T entity)
		{
			await _context.AddAsync(entity);
		}

		public void Delete(T entity)
		{
			_context.Remove(entity);
		}

		public T Find(int id)
		{
			return _context.Set<T>().Find(id);
		}

		public async Task<T> FindAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public List<T> GelAll()
		{
			return _context.Set<T>().ToList();
		}

		public List<T> GelAll(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().Where(expression).ToList();
		}

		public T Get(Expression<Func<T, bool>> expression)
		{
			return _context.Set<T>().FirstOrDefault(expression);
		}

		public async Task<List<T>> GetAllAsync()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
		{
			return await _context.Set<T>().Where(expression).ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(expression);
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public Task<int> SaveChangesAsync()
		{
			return _context.SaveChangesAsync();
		}

		public void Update(T entity)
		{
			_context.Update(entity);
		}
	}
}
