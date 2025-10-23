using Microsoft.EntityFrameworkCore;
using ST10026525.PROG3B.POE.Data;

namespace ST10026525.PROG3B.POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
          

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("EventsDB")));
          
            builder.Services.AddSession();

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Menu}/{action=MainMenu}/{id?}");

            app.Run();
        }
    }
}
