using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Card
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public int ColumnId { get; set; }
        [JsonIgnore]
        public  Column? Column { get; set; }

    }
}
