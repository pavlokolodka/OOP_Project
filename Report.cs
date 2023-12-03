namespace OOP_Project
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        string Title { get; set; }
        string TotalAmount { get; set; }
        int UserId { get; set; }
        string isViwed { get; set; }


        Report(string title, decimal amount, int userId)
        {
            throw new NotImplementedException();
        }       
    }
}
