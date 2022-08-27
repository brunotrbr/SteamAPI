using System.ComponentModel.DataAnnotations;

namespace SteamAPI.Models
{
    public class Platforms
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
