
using CMD.Application.Interfaces;
using CMD.Application.Services;
using CMD.Infrastructure.Data;
using CMD.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMD.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add repositories and services
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IPatientService, PatientService>();

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Replace with your allowed origins
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                              
                    });
            });

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(CMD.Application.Mappings.MappingProfile));

            // Add Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://your-auth-service-url"; // URL of your central auth service
                    options.Audience = "patient-api"; // Audience for this specific API
                    options.RequireHttpsMetadata = false; // Set to true in production
                });

            //When deploying for production, comment the above one and use the below one
            /*    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Auth:Authority"]; // URL of your production auth service
                    options.Audience = Configuration["Auth:Audience"]; // Production audience value
                    options.RequireHttpsMetadata = true; // Ensure HTTPS is required
                });*/


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Mock the authentication for development and testing
            if (app.Environment.IsDevelopment())
            {
                app.Use(async (context, next) =>
                {

                    var identity = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "TestUser"),
                        new Claim(ClaimTypes.Role, "Admin"),
                    }, "TestAuth");

                    context.User = new ClaimsPrincipal(identity);
                    await next();
                });
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    DbInitializer.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }



            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowSpecificOrigins");

            app.MapControllers();

            app.Run();
        }


        //To test the project by generating the JWT token , Create a new project and create the class TestAuthHelper.cs and then add the method mentioned below.
        // CMD.Patient.Tests/Helpers/TestAuthHelper.cs

            /*using Microsoft.IdentityModel.Tokens;
            using System;
            using System.IdentityModel.Tokens.Jwt;
            using System.Security.Claims;
            using System.Text;

            namespace CMD.Patient.Tests.Helpers
                {
                    public static class TestAuthHelper
                    {
                        public static string GenerateMockJwtToken(string username, string role)
                        {
                            var claims = new[]
                            {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, role),
                        };

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourTestSecretKey"));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                            var token = new JwtSecurityToken(
                                issuer: "TestIssuer",
                                audience: "patient-api",
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(30),
                                signingCredentials: creds);

                            return new JwtSecurityTokenHandler().WriteToken(token);
                        }
                    }
                }*/
    }
}
