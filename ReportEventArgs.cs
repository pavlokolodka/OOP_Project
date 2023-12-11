namespace OOP_Project
{
    public class ReportEventArgs : EventArgs
    {
        public Report CreatedReport { get; }

        public ReportEventArgs(Report createdReport)
        {
            CreatedReport = createdReport;
        }

    }

    public class UserLoginEventArgs : EventArgs
    {
        public DateTime LoginTime { get; }
        public string Username { get; }

        public UserLoginEventArgs(DateTime loginTime, string username)
        {
            LoginTime = loginTime;
            Username = username;
        }
    }

}
