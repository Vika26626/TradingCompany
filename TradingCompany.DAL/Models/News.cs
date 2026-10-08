namespace TradingCompany.DAL.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserId { get; set; }
    }
}