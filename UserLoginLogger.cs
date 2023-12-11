using OOP_Project;
using System;
using System.IO;
using System.Text.Json;

public class UserLoginLogger
{
    private static string logFilePath = "loginLog.json";

    public static void LogUserLogin(UserLoginEventArgs e)
    {
        try
        {
            LoginLogEntry logEntry = new LoginLogEntry
            {
                LoginTime = e.LoginTime,
                Username = e.Username
            };

            string jsonLogEntry = JsonSerializer.Serialize(logEntry);

            File.AppendAllText(logFilePath, jsonLogEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}

public class LoginLogEntry
{
    public DateTime LoginTime { get; set; }
    public string Username { get; set; }
}
