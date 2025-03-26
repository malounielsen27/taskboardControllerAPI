using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Board
    {
        public int Id { get; set; }
        public required string Title { get; set; }
       
        public ICollection<Column> Columns { get; set;  } = new List<Column>();
        
    }
}
