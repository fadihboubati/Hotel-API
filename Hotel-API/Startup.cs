using Hotel_API.Data;
using Hotel_API.Models;
using Hotel_API.Models.Interfaces;
using Hotel_API.Models.Interfaces.Services;
using Hotel_API.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(options =>
            {
                // Our DATABASE_URL from js days
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            //Register IDENTITY
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>();
                    //.AddDefaultTokenProviders();

            // MAPPING - register my Dependency Injection Services
            services.AddTransient<IHotel, HotelService>();
            services.AddTransient<IRoom, RoomService>();
            services.AddTransient<IAmenity, AmentyService>();
            services.AddTransient<IHotelRoom, HotelRoomService>();
            services.AddTransient<IUserService, IdentityUserService>();
            services.AddScoped<JwtTokenService>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(options =>
            {
                // Make sure get the "using Statement"
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Hotal API",
                    Version = "v1",
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationParameters(Configuration);
            });

            services.AddAuthorization(options =>
            {
                // Add "Name of Policy", and the Lambda returns a definition
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            // The call to app.UseAuthorization() must appear between app.UseRouting() and app.UseEndpoints(...).
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseSwagger(options => {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Hotel API");
                options.RoutePrefix = "docs";
            });
        }
    }
}
