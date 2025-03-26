namespace backend.Dtos
{
    public class CardRequest
    {
        public required string Title { get; set;  }
        public required string Description { get; set; }
        public int ColumnId { get; set; }

    }
}
