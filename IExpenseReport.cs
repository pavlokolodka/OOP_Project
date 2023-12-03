namespace OOP_Project
{
    public interface IExpenseReport
    {
        Report CreateReport(string title, List<Expense> expenses);
        Report DeleteReport(int reportId, List<Expense> expenses);
        void ViewReport(Report report);
    }
}
