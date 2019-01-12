using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using customeIdentity.Database;
using customeIdentity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace customeIdentity
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration{get;}
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options=>{
                options.UseMySql(Configuration.GetConnectionString("Defualt"));
            });

            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<Role>>();
            services.AddIdentity<User, Role>()
                // .AddUserManager<UserManager<User>>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
