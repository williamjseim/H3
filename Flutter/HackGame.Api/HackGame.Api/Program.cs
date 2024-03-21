
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using HackGame.Api.Data;
using HackGame.Api.TokenAuthorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Http;

namespace HackGame.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSignalR();
            builder.WebHost.ConfigureKestrel(options=>{
                options.Limits.MaxRequestLineSize = 1048576;
            });

            //builder.Services.AddDbContext<HackerGameDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddTransient<JwtAuthorization>();

            builder.Services.AddDbContext<HackerGameDbContext>(options =>
            options.UseMySql(builder.Configuration.GetConnectionString("Default"),ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("Default"))));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("C:\\Users\\zbcwise\\Desktop\\h3database-firebase-adminsdk-ptmfl-20b212d236.json"),
                ProjectId = "h3database",
            });

            app.UseHttpsRedirection();

            app.UseCors(policy => policy.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.MapControllers();


            app.Run();
        }
    }
}