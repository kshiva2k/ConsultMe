using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultMe.Data.Data;
using ConsultMe.Data.Models;
using ConsultMe.Data.Repository;
using ConsultMe.Service.Repository;
using ConsultMe.Service.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsultMe
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string conStr = this.Configuration.GetConnectionString("DefaultConnection");
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                   options =>
                   {
                       options.LoginPath = new PathString("/Home/Index");
                       options.AccessDeniedPath = new PathString("/Home/Index");
                   });
            services.AddDbContext<consultmeContext>(options => options.UseMySQL(conStr));
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);//You can set Time   
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUserServiceRepository, UserService>();
            services.AddSingleton<IAppointmentServiceRepository, AppointmentService>();
            services.AddSingleton<ICustomerServiceRepository, CustomerService>();
            services.AddSingleton<IPatientServiceRepository, PatientService>();
            services.AddSingleton<ISubscriptionServiceRepository, SubscriptionService>();
            services.AddSingleton<ILookupServiceRepository, LookupService>();
            services.AddSingleton<IAppointmentRepository, AppointmentData>();
            services.AddSingleton<IClinicDataRepository, ClinicData>();
            services.AddSingleton<ICustomerDataRepository, CustomerData>();
            services.AddSingleton<IFeedbackRepository, FeedbackData>();
            services.AddSingleton<ILookupRepository, LookupData>();
            services.AddSingleton<IPatientRepository, PatientData>();
            services.AddSingleton<ISubscriptionRepository, SubscriptionData>();
            services.AddSingleton<IUserRepository, UserData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
