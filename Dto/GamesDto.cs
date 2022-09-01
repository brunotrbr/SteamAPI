using System.Text.Json.Serialization;

namespace SteamAPI.Dto
{
    public class GamesDto
    {
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

        public GamesDto(int appId, string name, string developer, string platforms, string categories, string genres)
        {
            AppId = appId;
            Name = name;
            Developer = developer;
            Platforms = platforms;
            Categories = categories;
            Genres = genres;
        }
    }
}
