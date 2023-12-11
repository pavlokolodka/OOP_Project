using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OOP_Project
{
    class Program
    {
        private static User authenticatedUser;
        private static ExpenseReport expenseReport;
        private static UserManager userManager;
        private static ExpenseManager expenseManager;
        public delegate void UserLoginEventHandler(UserLoginEventArgs e);
        public static event UserLoginEventHandler UserLoggedIn;

        private static void GlobalLogger(string message) {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nSystem: " + message);
            Console.ResetColor();
        }
        private static void Greeting(string name)
        {
            Console.WriteLine($"\nWelcome, {name}!");
        }
    
        static void Main()
        {
            expenseReport = new ExpenseReport();
            userManager = new UserManager(new UserJSONDao());
            expenseManager = new ExpenseManager(new ExpenseJSONDao());


            // delegates
            UserLoggedIn += UserLoginLogger.LogUserLogin;
            userManager.userLogger = GlobalLogger;
            expenseReport.ReportCreated += (sender, e) =>
            {
                Console.WriteLine($"\nReport {e.CreatedReport.Title} created successfully");
            };


            bool exit = false;

            do
            {
                Console.WriteLine("1. Sign In");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            SignIn(Greeting);
                            break;
                        case 2:
                            SignUp();
                            break;
                        case 3:
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            } while (!exit);
        }       

        private static void SignIn(Action<string> greeting)
        {
            Console.Write("Enter your nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (!string.IsNullOrEmpty(nickname))
            {

                authenticatedUser = userManager.findUser(nickname);

                if (authenticatedUser != null && authenticatedUser.Password == password)
                {
                    greeting(authenticatedUser.FirstName);
                    UserLoggedIn?.Invoke(new UserLoginEventArgs(DateTime.Now, authenticatedUser.Nickname));
                    
                    UserMenu();
                }
                else
                {
                    Console.WriteLine("\nAuthentication failed. Please check your credentials.");
                }
            }
            else
            {
                Console.WriteLine("\nNickname cannot be empty. Please try again.");
            }
        }

        private static void SignUp()
        {
            Console.Write("Enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter your last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter your nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) &&
                !string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    var newUser = new User(firstName, lastName, password, nickname);
                    authenticatedUser = userManager.createUser(newUser);    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nUser with nickname {0} already exists", nickname);
                    return;
                }

                Console.WriteLine("\nSign up successful! Welcome, {0}!", authenticatedUser.FirstName);
                UserMenu();
            }
            else
            {
                Console.WriteLine("\nAll fields (first name, last name, nickname, password) must be provided. Please try again.");
            }
        }

        private static void CreateExpense()
        {
            try
            {
                string expenseName = GetValidInput("Enter expense name: ", ValidateExpenseName, "Expense name cannot be empty.");

                decimal expenseAmount = decimal.Parse(GetValidInput("Enter expense amount: ", ValidateExpenseAmount, "Invalid expense amount. Please enter a valid positive number."));

                Console.WriteLine("Expense Categories:");
                foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                {
                    Console.WriteLine($"- {category}");
                }

                ExpenseCategory expenseCategory = Enum.Parse<ExpenseCategory>(
                    GetValidInput("Enter expense category: ", ValidateExpenseCategory, "Invalid expense category. Please enter a valid category name.")
                        .ToLower(),
                    ignoreCase: true
                );

                Expense newExpense = new Expense(authenticatedUser.Id, expenseName, expenseAmount, expenseCategory);
                expenseManager.CreateExpense(newExpense);
                Console.WriteLine("\nExpense created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }

        private static string GetValidInput(string prompt, Func<string, bool> validationFunc, string errorMessage)
        {
            string userInput;
            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();

                if (!validationFunc(userInput))
                {
                    Console.WriteLine($"\nError: {errorMessage}");
                }

            } while (!validationFunc(userInput));

            return userInput;
        }

        private static bool ValidateExpenseName(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        private static bool ValidateExpenseAmount(string input)
        {
            return decimal.TryParse(input, out decimal amount) && amount >= 0;
        }

        private static bool ValidateExpenseCategory(string input)
        {
            if (decimal.TryParse(input, out _))
            {
                return false; 
            }

            return Enum.TryParse<ExpenseCategory>(input, true, out _);
        }
        private static void UserMenu()
        {
            bool logout = false;

            do
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. View profile");
                Console.WriteLine("2. Change Name");
                Console.WriteLine("3. Change Password");
                Console.WriteLine("4. Create Expense");
                Console.WriteLine("5. Delete Expense");
                Console.WriteLine("6. Update Expense");
                Console.WriteLine("7. View All Expenses");
                Console.WriteLine("8. Generate Report");
                Console.WriteLine("9. View All Reports");
                Console.WriteLine("10. View One Report");
                Console.WriteLine("11. Logout");
                Console.Write("Enter your choice: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine(authenticatedUser.ToString());
                            break;
                        case 2:
                            Console.Write("Enter your first name: ");
                            string firstName = Console.ReadLine();
                            Console.Write("Enter your last name (optional): ");
                            string lastName = Console.ReadLine();

                            if (!string.IsNullOrEmpty(firstName))
                            {
                                if (string.IsNullOrEmpty(lastName))
                                {
                                    authenticatedUser.FirstName = firstName;
                                }
                                else
                                {
                                    authenticatedUser.FirstName = firstName;
                                    authenticatedUser.LastName = lastName;
                                }


                                userManager.updateUser(authenticatedUser);
                                Console.Write("\nThe name has been successfully changed");
                            } else
                            {
                                Console.Write("\nInput the valid name");
                            }


                            break;
                        case 3:
                            Console.Write("Enter your new password: ");
                            string password = Console.ReadLine();

                            if (!string.IsNullOrEmpty(password))
                            {
                                if (password != authenticatedUser.Password)
                                {
                                    userManager.updateUser(authenticatedUser);
                                    Console.Write("\nThe name has been successfully changed");
                                    break;
                                }

                                Console.Write("\nThe new password must be different from the old one: ");
                            }
                            break;
                        case 4:
                            CreateExpense();
                            break;
                        case 5:
                            Console.WriteLine("\nEnter a name of expense to delete");
                            string expenseName = Console.ReadLine();

                            var userExpenses = expenseManager.FindExpenses(authenticatedUser.Id);

                            if (userExpenses.Count == 0)
                            {
                                Console.WriteLine("\nYou have 0 expenses yet");
                                break;
                            }

                            var expenseToDelete = userExpenses.FirstOrDefault(e => e.Name == expenseName);

                            if (expenseToDelete == null)
                            {
                                Console.WriteLine($"\nCannot find expense {expenseName}");
                                break;

                            }

                            expenseManager.DeleteExpense(expenseToDelete.Id);
                            Console.WriteLine("\nSuccessfully deleted");

                            break;
                        case 6:
                            string expenseNameToUpdate;
                            expenseNameToUpdate = GetValidInput("Enter a name of expense to update: ", ValidateExpenseName, "Invalid expense name.");
                      
                            var createdExpenses = expenseManager.FindExpenses(authenticatedUser.Id);

                            if (createdExpenses.Count == 0)
                            {
                                Console.WriteLine("\nYou have 0 expenses yet");
                                break;
                            }

                            var expenseToUpdate = createdExpenses.FirstOrDefault(e => e.Name == expenseNameToUpdate);

                            if (expenseToUpdate == null)
                            {
                                Console.WriteLine($"\nCannot find expense {expenseNameToUpdate}");
                                break;
                            }

                            // NAME
                            string newExpenseNameToUpdate;
                            do
                            {
                                newExpenseNameToUpdate = GetValidInput("Enter a new name of expense to update: ", ValidateExpenseName, "Invalid expense name.");

                                Expense existed = createdExpenses.FirstOrDefault(e => e.Name == newExpenseNameToUpdate);

                                if (existed != null && newExpenseNameToUpdate != expenseToUpdate.Name)
                                {
                                    Console.WriteLine("\nThe expense with such name already exists.");
                                    continue;
                                }

                                break;
                            } while (true);

                           decimal expenseAmount =  decimal.Parse(GetValidInput("Enter new amount of expense to update: ", ValidateExpenseAmount, "Invalid expense amount."));
                     

                            // CATEGORY
                            Console.WriteLine("Expense Categories:");
                            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                            {
                                Console.WriteLine($"- {category}");
                            }

                            string expenseCategoryToUpdate = GetValidInput("Enter a new category: ", ValidateExpenseCategory, "Invalid expense category. Please enter a valid category name.");

                           try
                            {
                                expenseToUpdate.Category = Enum.Parse<ExpenseCategory>(expenseCategoryToUpdate, true);
                                expenseToUpdate.Name = newExpenseNameToUpdate;
                                expenseToUpdate.Amount = expenseAmount;

                                expenseManager.UpdateExpense(expenseToUpdate);
                                Console.WriteLine("\nSuccessfully updated");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                break;
                            }


                            break;
                        case 7:
                            var expenses = expenseManager.FindExpenses(authenticatedUser.Id);

                            if (expenses.Count == 0)
                            {
                                Console.WriteLine("\nYou have 0 expenses yet");
                                break;
                            }

                            Console.WriteLine("\nExpenses:");
                            foreach (Expense expense in expenses)
                            {
                                Console.WriteLine(expense.ToString());
                            }

                            break;
                        case 8:
                            Console.Write("\nEnter expense names separated by commas: ");
                            string inputExpenseNames = Console.ReadLine();

                            if (inputExpenseNames.Count() == 0)
                            {
                                Console.Write("\nInvalid input format");
                                break;
                            }

                            string[] expenseNames = inputExpenseNames.Split(',');

                            
                            var allUserExpenses = expenseManager.FindExpenses(authenticatedUser.Id);
                            var filteredExpenses = allUserExpenses.Where(expense => expenseNames.Contains(expense.Name)).ToList();

                            if (filteredExpenses.Count <= 0)
                            {
                                Console.WriteLine("\nNo expenses matched the given names. Cannot create a report.");
                                break;
                            }

                            Console.Write("Enter report title: ");
                            string reportTitle = Console.ReadLine();

                            if (string.IsNullOrEmpty(reportTitle))
                            {
                                Console.WriteLine("\nInvalid report name.");
                                break;
                            }

                            expenseReport.CreateReport(reportTitle, filteredExpenses);                          

                            break;
                        case 9:
                            var reports = expenseReport.Reports;

                            if (reports.Count == 0)
                            {
                                Console.WriteLine("\nYou have 0 reports so far.");
                            }

                            Console.WriteLine("\nReports");
                            foreach (Report report in reports)
                            {
                                Console.WriteLine(report.ToString());
                                report.isViwed = true;
                            }

                            break;
                        case 10:
                            Console.WriteLine("Input report name");
                            string reportName = Console.ReadLine();

                            if (string.IsNullOrEmpty(reportName))
                            {
                                Console.WriteLine("\nInvalid report name.");
                                break;
                            }


                            var userReports = expenseReport.Reports;

                            if (userReports.Count == 0)
                            {
                                Console.WriteLine("\nYou have 0 reports so far.");
                                break;
                            }

                            var reportToView = userReports.FirstOrDefault(r => r.Title == reportName, null);
                          
                            if (reportToView == null)
                            {
                                Console.WriteLine("\nCannot find a report.");
                                break;
                            }

                            expenseReport.ViewReport(reportToView);
                            break;
                        case 11:
                            logout = true;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }

            } while (!logout);
        }
    }
}
