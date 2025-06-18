using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ClientPortal.Data;
using ClientPortal.Models;
using ClientPortal.Services;
using Codes1.Service.Interfaces;
using Codes1.Service.Services;
using Codes1.Service.Data;
using ClientPortal.Services._Interfaces;
using AutoMapper;
using Codes1.Service.Domain;
using Excel.Service.Services.Interfaces;
using Excel.Service.Services;
using VendorImport.Service.Data;
using VendorImport.Service.Interfaces;
using VendorImport.Service;
using Booking.Service.Services._Interfaces;
using Booking.Service.Services;
using Booking.Service.Data;
using LegacyData.Service.Services;
using LegacyData.Service.Data;
using RSIData.Service.Services;
using RSIData.Context.Interfaces;
using LegacyData.Service.Interfaces;
using Codes.Service.Interfaces;
using Codes.Service.Services;
using Codes.Service.Data;
using RSIData.Context.Data;
using KeepLivingLife.Service.Services;
using MailChimp.Service.Interfaces;
using MailChimp.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Communication.Service.Interfaces;
using Communication.Service.Implementations;
using HubSpot.Service.Interfaces;
using HubSpot.Service;
using Bearer.Service.Interfaces;
using Bearer.Service;
using Microsoft.Extensions.Hosting;
using OAuth2.Service.Interfaces;
using OAuth2.Service;
using Microsoft.AspNetCore.Mvc;

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

            services.AddDbContext<Application1DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.User.RequireUniqueEmail = false;
                })
                .AddEntityFrameworkStores<Application1DbContext>()
                .AddDefaultTokenProviders();

            services.AddDbContext<Codes1DbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CodeGeneratorConnection")));

            

            services.AddDbContext<CodesDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CodeGeneratorConnection")));

            services.AddDbContext<VendorDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VendorConnection")));

            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BookingConnection")));

            services.AddDbContext<RSIDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LegacyConnection")));

            RegisterApplicationServices(services);

            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            /*services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                           options.SerializerSettings.ContractResolver =
                              new CamelCasePropertyNamesContractResolver());*/

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddMvc(o => o.EnableEndpointRouting = false);

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });

            // Extra if statement needed because of this issue:
            // https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/issues/49
            if (!IsAutoMapperInitialized)
            {
                IsAutoMapperInitialized = true;
                services.AddAutoMapper(typeof(Startup));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Local")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            //app.UseCors("SiteCorsPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            //if (env.IsDevelopment())
            //{
             //   app.UseDeveloperExceptionPage();
           // }

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            //app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
        }

        private void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IEmail1Sender, Email1Sender>();
            services.AddTransient<ICode1Service, Code1Service>();
            services.AddTransient<Codes.Service.Interfaces.V2.ICodesService, Codes.Service.Services.V2.CodesService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IVideo1Service, Video1Service>();
            services.AddTransient<ICampaign1Service, Campaign1Service>();
            services.AddTransient<IDashboard1Service, Dashboard1Service>();
            services.AddTransient<ISearch1Service, Search1Service>();
            services.AddTransient<ICard1Service, Card1Service>();
            services.AddTransient<IPurchase1Service, Purchase1Service>();
            services.AddTransient<IAccount1Service, Account1Service>();
            services.AddTransient<IDocument1Service, Document1Service>();
            services.AddTransient<IViewData1Service, ViewData1Service>();
            services.AddTransient<ICodeGenerator1Service, CodeGenerator1Service>();
            services.AddTransient<IReport1Service, Report1Service>();
            services.AddTransient<IReportCommission1Service, ReportCommission1Service>();
            services.AddTransient<IReportProduction1Service, ReportProduction1Service>();
            services.AddTransient<IDashboardDistribution1Service, DashboardDistribution1Service>();
            services.AddTransient<IAccountQueryFactory, AccountQueryFactory>();
            services.AddTransient<IVendorImportService, VendorImportService>();
            services.AddTransient<IExportService, ExportService>();
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ILegacyService, LegacyService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<ICodeService, CodeService>();
            services.AddTransient<IKeepLivingLifeService, KeepLivingLifeService>();
            services.AddTransient<IBulkEmailService, BulkEmailService>();
            services.AddTransient<IMailChimpMethod, MailChimpMethods>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IMemberPackageService, MemberPackageService>();
            services.AddTransient<IRSIEmailService, RSIEmailService>();
            services.AddTransient<IEmailSender, MandrillEmailSender>();
            services.AddTransient<IClientBaseService, ClientBaseServices>();
            services.AddTransient<IHubSpotService, HubSpotService>();
            services.AddTransient<IBearerService, BearerService>();
            services.AddTransient<IOAuth2Service, OAuth2Service>();
            services.AddTransient<Codes1.Service.Interfaces.V2.ICodeServices, Codes1.Service.Services.V2.CodeServices>();
        }
    }
}
