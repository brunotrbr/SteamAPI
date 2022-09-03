using Microsoft.EntityFrameworkCore;
using SteamAPI.Context;
using SteamAPI.Filters;
using SteamAPI.Interfaces;
using SteamAPI.Repositories;

namespace SteamAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers(options =>
                options.Filters.Add(typeof(CustomActionFilterGlobal)));
            builder.Services.AddControllers(options => 
            options.Filters.Add(typeof(CustomExceptionFilter)));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Conexao In Memory database

            builder.Services.AddDbContext<InMemoryContext>(options => options.UseInMemoryDatabase("Steam"));

            #endregion

            #region Injecao de dependencia do Repository
            
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            
            #endregion

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