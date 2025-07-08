using System.Text.Json.Serialization;

namespace CheapGames.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [JsonIgnore]
        public List<Game> Games { get; set; } = new List<Game>();
    }
}
