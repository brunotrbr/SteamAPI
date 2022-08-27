using System.ComponentModel.DataAnnotations;

namespace SteamAPI.Models
{
    public class Games
    {
		[Key]
		public int Id { get; set; }
		public int AppId { get; set; }
		public string Name { get; set; }
		public DateTime ReleaseDate { get; set; }
		public bool English { get; set; }
		public string Developer { get; set; }
		public string Publisher { get; set; }
		public Platforms Platforms { get; set; }
		public int RequiredAge { get; set; }
		public Categories Categories { get; set; }
		public Genres Genres { get; set; }
		public SteamspyTags SteamspyTags { get; set; }
		public int Achievements { get; set; }
		public int PositiveRatings { get; set; }
		public int NegativeRatings { get; set; }
		public int AveragePlaytime { get; set; }
		public int MedianPlaytime { get; set; }
		public string Owners { get; set; }
		public decimal Price { get; set; }
	}
}
