using System.Text.Json.Serialization;

namespace SteamAPI.Dto
{
    public class GamesPatchDto
    {
        [JsonPropertyName("platforms")]
        public string Platforms { get; set; }

        public GamesPatchDto(string platforms)
        {
            Platforms = platforms;
        }
    }
}
