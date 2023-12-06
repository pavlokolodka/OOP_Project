namespace OOP_Project
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public bool isViwed { get; set; } = false;


        public Report(string title, decimal amount, int userId)
        {
            Title = title;
            TotalAmount = amount;
            UserId = userId;
        }       
    }
}
