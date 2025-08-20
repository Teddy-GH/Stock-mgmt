namespace FinShark.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        public string? Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int StockId { get; set; }
        //public Stock? Stock { get; set; }
    }
}
