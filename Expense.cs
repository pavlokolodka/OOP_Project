namespace OOP_Project
{
    public class Expense : IComparable<Expense>, IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        string Name { get; set; }
        ExpenseCategory Category { get; set; }
        decimal Amount { get; set; }
        int UserId { get; set; }
        Expense(int userId, string name, decimal amount, ExpenseCategory category)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Expense? other)
        {
            throw new NotImplementedException();
        }
    }
}
