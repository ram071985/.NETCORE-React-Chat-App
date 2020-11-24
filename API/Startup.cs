using Core.DataAccess;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {

        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddControllers();
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "client/build";
            });

            services.AddScoped<IAuthorizeUserService, AuthorizeUserService>();
            services.AddScoped<ICreateMessageService, CreateMessageService>();
            services.AddScoped<IGetMessagesService, GetMessagesService>();
            services.AddScoped<ICreateNewUserService, CreateNewUserService>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IUserUpdateService, UserUpdateService>();
            services.AddScoped<IDbConnection, DbConnection>();
            services.AddScoped<ISessionDataAccess, SessionDataAccess>();
            services.AddScoped<IMessageDataAccess, MessageDataAccess>();
            services.AddScoped<IUserDataAccess, UserDataAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        
            app.UseExceptionHandler("/error");
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client";
                if(env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            }
            );
        }
    }
}
