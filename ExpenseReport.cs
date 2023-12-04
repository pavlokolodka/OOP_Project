namespace OOP_Project
{
    public class ExpenseReport : IExpenseReport
    {
        public List<Report> Reports { get; private set; }

        public Report CreateReport(string title, List<Expense> expenses)
        {
            throw new NotImplementedException();
        }

        public void DeleteReport(int reportId)
        {
            throw new NotImplementedException();
        }    

        public void ViewReport(Report report)
        {
            throw new NotImplementedException();
        }

    }
}
