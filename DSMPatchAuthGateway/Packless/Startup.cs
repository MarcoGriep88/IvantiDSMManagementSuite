using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Packless.Database;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace Packless
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
            services.AddDbContext<PacklessDbContext>(options => options.UseMySql(Configuration["ConnectionString:MySQLLocal"]));

            services.AddCors(o => o.AddPolicy("DevPolicy", builder =>
            {
                //builder.WithOrigins("https://localhost:44319", "http://packless.marcogriep.de", "https://locahost:4200")
                       builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
               }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IAuthRepository, AuthRepository>(); //Service Registrieren: Once per Request created, uses the same instance of the same request source

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    //SaveSigninToken = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                        GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.
                    //    GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                    //ValidateLifetime = true,
                    //ClockSkew = TimeSpan.Zero
                };
                options.RequireHttpsMetadata = false;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                //options.KnownProxies.Add(IPAddress.Parse(""))
            });

            services.AddAuthorization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();

            //app.UseHttpsRedirection(); //Wenn aktiviert, JwtAuthentication funktioniert nicht... ?!
            app.UseMvc();
        }
    }
}
