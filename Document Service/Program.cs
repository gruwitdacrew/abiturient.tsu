
using Document_Service.DBContext;
using Document_Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Users_Service.Models;

namespace Document_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DocumentDB")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<Education>();
            builder.Services.AddScoped<Passport>();
            builder.Services.AddScoped<Scan>();

            AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
            builder.Configuration.Bind("Authentication", authenticationConfiguration);
            builder.Services.AddSingleton(authenticationConfiguration);

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;

                    AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
                    builder.Configuration.Bind("Authentication", authenticationConfiguration);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
                        ValidIssuer = authenticationConfiguration.Issuer,
                        ValidAudience = authenticationConfiguration.Audience,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                });

            builder.Services.AddSingleton<RabbitMQService>();
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();

                var rabbit = app.Services.GetRequiredService<RabbitMQService>();
                rabbit = new RabbitMQService(app.Services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}