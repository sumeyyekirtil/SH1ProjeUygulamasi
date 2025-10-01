using SH1ProjeUygulamasi.Core.Entities;
using System.Linq.Expressions;

namespace SH1ProjeUygulamasi.Service.Abstract
{
	//class: referance tipli
	//abstract: değer tipli
	public interface IService<T> where T : class, IEntity, new() //Service bir tip ile çalışacak T parametresi
	{
		//IService interface i dışarıdan T olarak bir parametre ile çalışacak, where T : class, IEntity, new() kodu buraya gelecek T tipinin şartlarını belirler, class olmalı IEntity interfacesi kullanmalı ve new lenebilir bir yapı olmalı(string olmamalı)
		//Senkron Metot İmzaları
		List<T> GelAll();
		List<T> GelAll(Expression<Func<T, bool>> expression);
		T Get(Expression<Func<T, bool>> expression);
		T Find(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
		int SaveChanges();

		//Asenkron Metotlar : Task
		Task<T> FindAsync(int id);
		Task<T> GetAsync(Expression<Func<T, bool>> expression);
		Task<List<T>> GetAllAsync(); 
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression);
		Task AddAsync(T entity);
		Task<int> SaveChangesAsync();
	}
}
