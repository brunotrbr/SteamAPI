using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SteamAPI.Models
{
    public class Games
    {
		[Key]
        [JsonPropertyName("id")]
		public int Id { get; set; }
        [JsonPropertyName("appid")]
        public int AppId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("developer")]
        public string Developer { get; set; }
		[JsonPropertyName("platforms")]
        public string Platforms { get; set; }
		[JsonPropertyName("categories")]
        public string Categories { get; set; }
		[JsonPropertyName("genres")]
        public string Genres { get; set; }

        public Games(int id, int appId, string name, string developer, string platforms, string categories, string genres)
        {
            Id = id;
            AppId = appId;
            Name = name;
            Developer = developer;
            Platforms = platforms;
            Categories = categories;
            Genres = genres;
        }
    }
}
