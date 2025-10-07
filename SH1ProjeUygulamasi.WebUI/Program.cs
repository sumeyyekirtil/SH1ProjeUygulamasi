using SH1ProjeUygulamasi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using SH1ProjeUygulamasi.Service.Abstract;
using SH1ProjeUygulamasi.Service.Concrete;
using System.Security.Claims;

namespace SH1ProjeUygulamasi.WebUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSession();

			builder.Services.AddDbContext<DatabaseContext>(); //uygulamayý cs dosyasýna ekledik baðlantý adresi için

			builder.Services.AddScoped<ICategoryService, CategoryService>(); //add scoped ile uygulamaya ICategoryService i kullanma isteði gelirse CategoryService sýnýfýndan bir nesne oluþtur ve onu kullan.

			//birden fazla kez kullanýlan servisler için istek baþýna bir kez nesne oluþturduðundan kaynaklarý daha verimli kullanýr. AddTransient ise tamamen stateless iþlemler için mükemmeldir, çünkü her seferinde yeni bir nesne oluþturulmasý gerekiyorsa, bu yaklaþým en iyi performansý saðlar.
			//Dependencies inverces Dependencies inverces 

			//diðer services ekleme yollarý
			//builder.Services.AddSingleton<ICategoryService, CategoryService>(); //bir sýnýfýn uygulama boyunca tek bir instance (örnek) ile kullanýlmasýný saðlar. Yani uygulama baþladýðýnda bir instance oluþturulur ve bu instance, uygulama kapanana kadar ayný kalýr. Bu, özellikle paylaþýlan veri ya da ayarlarýn kullanýldýðý yerlerde kullanýþlýdýr.

			//builder.Services.AddTransient<ICategoryService, CategoryService>(); //her talep edildiðinde yeni bir instance oluþturur. Yani bir sýnýf, uygulamanýn herhangi bir yerinde çaðrýldýðýnda her seferinde yepyeni bir nesne oluþturulup kullanýlýr. Bu, hafif ve kýsa ömürlü nesneler için idealdir.
			builder.Services.AddTransient<IProductService, ProductService>(); //Servisi tanýtmazsak hata alýp eþleþme için resolve çýktý verir bulamaz !!!

			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddScoped(typeof(IService<>), typeof(Service<>)); //Generic Service


			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); //oturum açma

			//altýnda olmalý
			//Authorization: Yetkilendirme: once servis olarak ekliyoruz
			builder.Services.AddAuthorization(x =>
			{
				x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin")); //bundan sonra controller lara policy belirtmeliyiz
				x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSession(); //session kullanmak için

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
