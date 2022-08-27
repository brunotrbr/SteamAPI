using System.ComponentModel.DataAnnotations;

namespace SteamAPI.Models
{
    public class SteamspyTags
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
