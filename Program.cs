using SteamAPI.Context;

namespace SteamAPI
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

            var postgresEndpoint = builder.Configuration["postgres_endpoint"];
            var postgresPort = builder.Configuration["postgres_port"];
            var postgresDB = builder.Configuration["postgres_database"];
            var postgresUser = builder.Configuration["postgres_user"];
            var postgresPass = builder.Configuration["postgres_pass"];

            builder.Services.AddNpgsql<PostgresContext>($"Host={postgresEndpoint};Port={postgresPort};Database={postgresDB};Username={postgresUser};Password={postgresPass}");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}