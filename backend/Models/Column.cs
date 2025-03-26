using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Column
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();
        public int BoardId { get; set; }
        [JsonIgnore]
        public Board? Board { get; set; }
        
    }
}
