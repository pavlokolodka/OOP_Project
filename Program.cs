using System;
using System.Collections.Generic;

namespace OOP_Project
{
    class Program
    {
        private static User authenticatedUser;
        private static ExpenseReport expenseReport;
        private static UserManager userManager;
        private static ExpenseManager expenseManager;
        
        static void Main()
        {
            expenseReport = new ExpenseReport();
            userManager = new UserManager(new UserJSONDao());
            expenseManager = new ExpenseManager(new ExpenseJSONDao());


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
                            SignIn();
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

        private static void SignIn()
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
                    Console.WriteLine($"\nWelcome, {authenticatedUser.FirstName}!");
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
                Console.Write("Enter expense name: ");
                string expenseName = Console.ReadLine();

                Console.Write("Enter expense amount: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal expenseAmount))
                {

                    if (expenseAmount < 0)
                    {
                        throw new ArgumentException("Expense amount cannot be negative.", nameof(expenseAmount));
                    }

                    Console.WriteLine("Expense Categories:");
                    foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                    {
                        Console.WriteLine($"- {category}");
                    }

                    Console.Write("Enter expense category: ");
                    string inputCategory = Console.ReadLine();

                    if (decimal.TryParse(inputCategory, out _))
                    {
                        throw new FormatException("Invalid expense category. Please enter a valid category name.");
                    }

                    inputCategory = inputCategory?.ToLower(); 

                    if (Enum.TryParse<ExpenseCategory>(inputCategory, true, out ExpenseCategory expenseCategory))
                    {
                        try
                        {
                            Expense newExpense = new Expense(authenticatedUser.Id, expenseName, expenseAmount, expenseCategory);
                            expenseManager.CreateExpense(newExpense);
                            Console.WriteLine("Expense created successfully!");
                        } catch(Exception ex) {
                            Console.WriteLine(ex.Message);
                            return;
                        }                       
                    }
                    else
                    {
                        throw new ArgumentException("Invalid expense category.");
                    }
                }
                else
                {
                    throw new FormatException("Invalid expense amount. Please enter a valid number.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
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
                            Console.WriteLine("\nEnter a name of expense to update");
                            string expenseNameToUpdate = Console.ReadLine();

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
                            Console.WriteLine("\nEnter a new name of expense to update");
                            string newExpenseNameToUpdate = Console.ReadLine();

                            if (string.IsNullOrEmpty(newExpenseNameToUpdate)) {
                                Console.WriteLine("\nInvalid expense name.");
                                break;
                            }

                            Expense existed = createdExpenses.FirstOrDefault(e => e.Name == newExpenseNameToUpdate);

                            if (existed != null)
                            {
                                Console.WriteLine("\nThe expense with such name already exists.");
                            }

                            // AMOUNT
                            Console.WriteLine("\nEnter new amount of expense to update");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal expenseAmount))
                            { 
                                Console.WriteLine("\nInvalid expense amount. Please enter a valid number.");
                                break;
                            }

                            // CATEGORY
                            Console.WriteLine("Expense Categories:");
                            foreach (ExpenseCategory category in Enum.GetValues(typeof(ExpenseCategory)))
                            {
                                Console.WriteLine($"- {category}");
                            }
                            Console.WriteLine("\nEnter a new category");
                            string expenseCategoryToUpdate = Console.ReadLine();

                            if (decimal.TryParse(expenseCategoryToUpdate, out _))
                            {
                                Console.WriteLine("\nInvalid expense category. Please enter a valid category name.");
                                break;
                            }

                            expenseCategoryToUpdate = expenseCategoryToUpdate?.ToLower();

                            if (Enum.TryParse<ExpenseCategory>(expenseCategoryToUpdate, true, out ExpenseCategory expenseCategory))
                            {
                                try
                                {
                                    expenseToUpdate.Category = expenseCategory;
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
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid expense category.");
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
                            Console.WriteLine($"Report created successfully");

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
