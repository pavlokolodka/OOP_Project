namespace OOP_Project
{
    public class ExpenseManager
    {
        private Dao<Expense> ExpenseDao;

        public Expense CreateExpense(Expense expense)
        {
            return ExpenseDao.Create(expense);  
        }
        public List<Expense> FindExpenses(int userId)
        {
            return (List<Expense>)ExpenseDao.Find(e => e.UserId == userId);
        }

        public void UpdateExpense(Expense expense)
        {
            ExpenseDao.Update(expense); 
        }

        public void DeleteExpense(int expenseId)
        {
            ExpenseDao.Delete(expenseId);
        }

        public ExpenseManager(Dao<Expense> expenseDao)
        {
            ExpenseDao = expenseDao;
        }       
    }
}
