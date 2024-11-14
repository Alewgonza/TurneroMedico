using Microsoft.EntityFrameworkCore;
using TurneroMedico.Context;
namespace TurneroMedico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<TurnosDatabaseContext>(options => options.UseSqlServer(builder.Configuration["ConnectionString:TurnosDBConnection"]));
            builder.Services.AddDbContext<TurnosDatabaseContext>(options =>
            {
                // https://stackoverflow.com/questions/48202403/instance-of-entity-type-cannot-be-tracked-because-another-instance-with-same-key
                options.UseSqlServer(builder.Configuration["ConnectionString:TurnosDBConnection"]);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
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
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.Run();
        }
    }
}
