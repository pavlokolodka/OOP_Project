namespace OOP_Project
{
    public class Expense : IComparable<Expense>, IEntity
    {
        private decimal _amount;
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Name { get; set; }
        public ExpenseCategory Category { get; set; }
        public decimal Amount
        {
            get => _amount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Amount cannot be negative.", nameof(Amount));
                }
                _amount = value;
            }
        }
        public int UserId { get; set; }
        public Expense(int userId, string name, decimal amount, ExpenseCategory category)
        {
            UserId = userId;
            Name = name;
            Amount = amount;
            Category = category;
        }

        public int CompareTo(Expense? other)
        {
            if (other == null)
            {
                return 1; 
            }
           
            return Amount.CompareTo(other.Amount);
        }
    }
}
