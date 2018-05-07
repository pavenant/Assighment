using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pierre.Avenant.Assignment.Core.Entities;
using Pierre.Avenant.Assignment.Core.Interfaces.Database;
using Pierre.Avenant.Assignment.Core.Interfaces.Excel;
using Pierre.Avenant.Assignment.Infrastructure.Database;
using Pierre.Avenant.Assignment.Infrastructure.Excel;

namespace Pierre.Avenant.Assignment.Web
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
            services.AddMvc();
            services.Configure<Configuration>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<IExcelFileProcessor, ExcelFileProcessor>();
            services.AddTransient<ICurrencyCodeRepository, CurrencyCodeRepository>(s => new CurrencyCodeRepository(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>(s => new AccountTransactionRepository(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            services.AddTransient<IFileUploadRepository, FileUploadRepository>(s => new FileUploadRepository(Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
