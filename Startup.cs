using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserRegWithAngularInNetCoreWebApi.Model;

namespace UserRegWithAngularInNetCoreWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //Add AddMVCCore as a service
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //Add DbContext and connectionString as a service
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
               Configuration.GetConnectionString("AppDbContextConnection")));
            //Add AddDefaultIdentity to services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            //.AddDefaultTokenProviders();
            //Configure Data validation for user registration
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
            });
            services.Configure<IdentityUser>(options =>
            {

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Add UseAuthentication
            app.UseAuthentication();

            app.UseAuthorization();

            //Add MVC to IApplicationBuilder
            //app.UseMvc();
            // app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
