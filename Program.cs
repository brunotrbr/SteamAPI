using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SteamAPI.AuthorizationAndAuthentication;
using SteamAPI.Context;
using SteamAPI.Filters;
using SteamAPI.Interfaces;
using SteamAPI.Repositories;
using System.Text;

namespace SteamAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(cors => cors.AddPolicy("AllowOriginAndMethod", options => options
                // .WithOrigins(new[] { "*" }) // libera pra todos os domínios
                .WithOrigins(new[] { "https://objectstorage.us-ashburn-1.oraclecloud.com" })
                .WithMethods(new[] { "GET", "DELETE" })
                ));

            // Add services to the container.
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(typeof(CustomActionFilterGlobal));
                options.Filters.Add(typeof(CustomExceptionFilter));
                options.Filters.Add(typeof(CustomLogsFilter));
                //options.Filters.Add(typeof(AuthorizationFilter));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Informe o token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }

                });
            });

            #region Conexao In Memory database

            builder.Services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("Steam"));

            #endregion

            #region Injecao de dependencia do Repository

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped(typeof(IUsersRepository), typeof(UsersRepository));

            #endregion

            #region Injeção de dependência do JWT Token
            var tokenConfiguration = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(builder.Configuration.GetSection("TokenConfiguration")).Configure(tokenConfiguration);
            builder.Services.AddSingleton(tokenConfiguration);
            var generateToken = new GenerateToken(tokenConfiguration);
            builder.Services.AddScoped(typeof(GenerateToken));
            #endregion

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = tokenConfiguration.Audience,
                    ValidIssuer = tokenConfiguration.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
                };
            });

            builder.Services.AddAuthorization(options => options.AddPolicy("ValidateClaimModule", policy => policy.RequireClaim("module", "teste")));

            #region Registra o Data Generator

            builder.Services.AddTransient<DataGenerator>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.UseCors("AllowOriginAndMethod");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            #region Popula o banco de dados
            var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopedFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<DataGenerator>();
                service.Generate();
            }
            #endregion

            app.Run();
        }
    }
}