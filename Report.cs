namespace OOP_Project
{
    public class Report : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Title { get; set; }
        public  string TotalAmount { get; set; }
        public int UserId { get; set; }
        public string isViwed { get; set; }


        public Report(string title, decimal amount, int userId)
        {
            throw new NotImplementedException();
        }       
    }
}
