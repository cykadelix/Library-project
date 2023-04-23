using Library_project.Data;
using Library_project.Settings;
using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.EntityFrameworkCore;

using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("local_lib");

        // Add services to the container.
        builder.Services.AddControllersWithViews();
       
        builder.Services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("Server=azurelibrarydatabase.postgres.database.azure.com;Database=Library;Port=5432;User Id=chavemm;Password=Postgres-2023!;Ssl Mode=Allow;")));
        
       
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
        app.UseAuthentication();
        app.UseAuthorization();

        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

    }
}

