using Microsoft.EntityFrameworkCore;
using SteamAPI.Models;
using System.Text.Json;

namespace SteamAPI.Context
{
    public class DataGenerator
    {
        private readonly InMemoryContext _inMemoryContext;
        private readonly List<string> _listNames;
        private readonly List<string> _listRoles;

        public DataGenerator(InMemoryContext inMemoryContext)
        {
            _inMemoryContext = inMemoryContext;
            _listNames = new List<string>() { "Bernardo", "José", "Washington", "Cleiton", "Cleyton", "Romário", "Maria", "Mariele", "Jussara", "Zoraide", };
            _listRoles = new List<string>() { "Master", "Manager", "Guest", "Developer", "Junior" };
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

            if (!_inMemoryContext.Users.Any())
            {
                List<Users> items = new List<Users>();
                var random = new Random();

                for(int i = 0; i < 10; i++)
                {
                    var name = $"{_listNames.ElementAt(random.Next(_listNames.Count))} {_listNames.ElementAt(random.Next(_listNames.Count))}";
                    var username = name.Replace(" ", "");
                    Users user = new Users();
                    user.Name = name;
                    user.Password = $"{username}{i}";
                    user.Username = username;
                    user.Role = $"{_listRoles.ElementAt(random.Next(_listRoles.Count))}";
                    items.Add(user);
                }
                _inMemoryContext.Users.AddRange(items);
                _inMemoryContext.SaveChanges();
            }
        }
    }
}

