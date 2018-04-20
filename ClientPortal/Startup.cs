using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClientPortal.Data;
using ClientPortal.Models;
using ClientPortal.Services;
using Codes.Service.Interfaces;
using Codes.Service.Services;
using Codes.Service.Data;
using ClientPortal.Services._Interfaces;
using AutoMapper;

namespace ClientPortal
{
    public class Startup
    {
        private static bool IsAutoMapperInitialized;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<CodesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CodeGeneratorConnection")));

            RegisterApplicationServices(services);

            services.AddMvc();

            // Extra if statement needed because of this issue:
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/49
            if (!IsAutoMapperInitialized)
            {
                IsAutoMapperInitialized = true;
                services.AddAutoMapper(typeof(Startup));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ICodeService, CodeService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVideoService, VideoService>();
            services.AddTransient<ICampaignService, CampaignService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<ICardService, CardService>();
            services.AddTransient<IPurchaseService, PurchaseService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IViewDataService, ViewDataService>();
        }
    }
}
