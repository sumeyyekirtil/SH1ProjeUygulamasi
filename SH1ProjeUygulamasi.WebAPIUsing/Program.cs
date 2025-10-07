using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace SH1ProjeUygulamasi.WebAPIUsing
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSession();

			builder.Services.AddHttpClient(); // HttpClient � dependency injection y�ntemiyle kullanabilmek ve controller �n bunu tan�mas� i�in (mimleme i�ini kendisine b�rakmam�� oluyoruz) bu �ekilde servisi tan�mlam�� oluyoruz.

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); //oturum a�ma

			//alt�nda olmal�
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

			app.UseSession(); //session kullanmak i�in

			app.UseHttpsRedirection();
			app.UseRouting();

			app.UseAuthorization();

			app.MapStaticAssets();

			app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}")
				.WithStaticAssets();

			app.Run();
		}
	}
}
