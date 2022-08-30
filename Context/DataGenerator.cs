using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using System.Text.Json;

namespace SteamAPI.Context
{
    public class DataGenerator
    {
        private readonly InMemoryContext _inMemoryContext;

        public DataGenerator(InMemoryContext inMemoryContext)
        {
            _inMemoryContext = inMemoryContext;
        }

        public void Generate()
        {
            if (!_inMemoryContext.Games.Any())
            {
                List<Games> items;
                using (StreamReader r = new StreamReader("steamData.json"))
                {
                    string json = r.ReadToEnd();
                    items = JsonSerializer.Deserialize<List<Games>>(json);
                }

                _inMemoryContext.Games.AddRange(items);
                _inMemoryContext.SaveChanges();
            }
        }
    }
}

