using SH1ProjeUygulamasi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;

namespace SH1ProjeUygulamasi.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

          builder.Services.AddDbContext<DatabaseContext>(); //uygulamay� cs dosyas�na ekledik ba�lant� adresi i�in

            builder.Services.AddScoped<ICategoryService, CategoryService>(); //add scoped ile uygulamaya ICategoryService i kullanma iste�i gelirse CategoryService s�n�f�ndan bir nesne olu�tur ve onu kullan.

			//birden fazla kez kullan�lan servisler i�in istek ba��na bir kez nesne olu�turdu�undan kaynaklar� daha verimli kullan�r. AddTransient ise tamamen stateless i�lemler i�in m�kemmeldir, ��nk� her seferinde yeni bir nesne olu�turulmas� gerekiyorsa, bu yakla��m en iyi performans� sa�lar.
			//Dependencies inverces Dependencies inverces 

			//di�er services ekleme yollar�
			//builder.Services.AddSingleton<ICategoryService, CategoryService>(); //bir s�n�f�n uygulama boyunca tek bir instance (�rnek) ile kullan�lmas�n� sa�lar. Yani uygulama ba�lad���nda bir instance olu�turulur ve bu instance, uygulama kapanana kadar ayn� kal�r. Bu, �zellikle payla��lan veri ya da ayarlar�n kullan�ld��� yerlerde kullan��l�d�r.

			//builder.Services.AddTransient<ICategoryService, CategoryService>(); //her talep edildi�inde yeni bir instance olu�turur. Yani bir s�n�f, uygulaman�n herhangi bir yerinde �a�r�ld���nda her seferinde yepyeni bir nesne olu�turulup kullan�l�r. Bu, hafif ve k�sa �m�rl� nesneler i�in idealdir.
			builder.Services.AddTransient<IProductService, ProductService>(); //Servisi tan�tmazsak hata al�p e�le�me i�in resolve ��kt� verir bulamaz !!!


			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

			app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
		    );

			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
