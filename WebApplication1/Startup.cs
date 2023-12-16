using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Services;
namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure DbContext
            services.AddDbContext<FileContext>(options =>
                options.UseInMemoryDatabase("FileDatabase"));

            // Register services
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFolderService, FolderService>();

            // Configure MVC
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=File}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Folder}/{action=Index}/{id?}");
            });
        }
    }
}
