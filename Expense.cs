namespace OOP_Project
{
    public class Expense : IComparable<Expense>, IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Name { get; set; }
        public ExpenseCategory Category { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public Expense(int userId, string name, decimal amount, ExpenseCategory category)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Expense? other)
        {
            throw new NotImplementedException();
        }
    }
}
