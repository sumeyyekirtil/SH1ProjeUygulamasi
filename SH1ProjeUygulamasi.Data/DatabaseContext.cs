using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SH1ProjeUygulamasi.Core.Entities;

namespace SH1ProjeUygulamasi.Data
{
	//Hiyerarşik bir dğzen ile bağlantı yapıldı
	//referance = dependences ile core projesi erişim
	//referance = dependences ile web.uı - core projesi erişim
	public class DatabaseContext : DbContext //entityframework using den miras alındı
	{
		//IEntity hariç class kullanabilmek için dbset eklendi
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Slider> Sliders { get; set; }

		//override on ile iki modeli entegre ettik db bağlantı için
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=ASUS-PRO; database=SH1ProjeUygulamasi; integrated security=true; TrustServerCertificate=True;").ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
		}
		//örnek veri eklemesi yapılır
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasData(
				new User()
				{
					Id = 1,
					CreateDate = DateTime.Now,
					UserName = "Test",
					Email = "test@gmail.com",
					IsActive = true,
					IsAdmin = true,
					Password = "pass123"
				}
			);
		}
	}
}
