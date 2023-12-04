namespace OOP_Project
{
    public interface IExpenseReport
    {
        Report CreateReport(string title, List<Expense> expenses);
        void DeleteReport(int reportId);
        void ViewReport(Report report);
    }
}
