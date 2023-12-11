namespace OOP_Project
{
    public class ExpenseReport : IExpenseReport
    {
        public event EventHandler<ReportEventArgs>? ReportCreated;

        public List<Report> Reports { get; private set; }

        public ExpenseReport()
        {
            Reports = new List<Report>();
        }

        public Report CreateReport(string title, List<Expense> expenses)
        {
            if (expenses == null || expenses.Count == 0)
            {
                throw new ArgumentException("Expenses cannot be null or empty.");
            }

            var totalAmount = expenses.Sum(e => e.Amount);
            var report = new Report(title, totalAmount, expenses.First().UserId);
            
            report.Id = GenerateId();
            Reports.Add(report);

            OnReportCreated(new ReportEventArgs(report));

            return report;
        }

        public void DeleteReport(int reportId)
        {
            var reportToRemove = Reports.FirstOrDefault(r => r.Id == reportId);

            if (reportToRemove == null)
            {
                throw new InvalidOperationException($"Report with ID {reportId} not found.");
            }
            
             Reports.Remove(reportToRemove);
          
        }

        public void ViewReport(Report report)
        {
            if (report == null)
            {
                throw new ArgumentNullException(nameof(report), "Report cannot be null.");
            }
            
            Console.WriteLine($"\nReport ID: {report.Id}");
            Console.WriteLine($"Title: {report.Title}");
            Console.WriteLine($"Total Amount: {report.TotalAmount:C}");
            Console.WriteLine($"User ID: {report.UserId}");
            Console.WriteLine($"Created At: {report.CreatedAt}");
            Console.WriteLine($"Updated At: {report.UpdatedAt}");
            Console.WriteLine($"Viewed: {report.isViwed}");

               
            report.isViwed = true;
            
        }

        protected virtual void OnReportCreated(ReportEventArgs e)
        {
            ReportCreated?.Invoke(this, e);
        }

        private int GenerateId()
        {
            return Reports.Count + 1;
        }
    }
}
