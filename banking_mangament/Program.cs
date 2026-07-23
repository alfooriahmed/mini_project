
using System;
using System.Collections.Generic;

namespace banking_mangament
{
    internal class Program
    {
        // ===================== SHARED DATA STORAGE =====================

        // These three lists always stay synchronized by index.
        static List<string> customerNames = new List<string>();
        static List<string> accountNumbers = new List<string>();
        static List<double> balances = new List<double>();


        // ===================== MAIN PROGRAM =====================

        static void Main(string[] args)
        {
            bool exitApp = false;

            while (!exitApp)
            {
                Console.WriteLine("\n===== Welcome to Spark Bank =====");
                Console.WriteLine("1. Add New Account");
                Console.WriteLine("2. Deposit Money");
                Console.WriteLine("3. Withdraw Money");
                Console.WriteLine("4. Show Balance");
                Console.WriteLine("5. Transfer Amount");
                Console.WriteLine("6. List All Accounts");
                Console.WriteLine("7. Close an Account");
                Console.WriteLine("8. Exit");

                Console.Write("Choose an option: ");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 8.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddAccount();
                        break;

                    case 2:
                        DepositMoney();
                        break;

                    case 3:
                        WithdrawMoney();
                        break;

                    case 4:
                        ShowBalance();
                        break;

                    case 5:
                        TransferAmount();
                        break;

                    case 6:
                        ListAllAccounts();
                        break;

                    case 7:
                        CloseAccount();
                        break;

                    case 8:
                        exitApp = true;
                        Console.WriteLine("Thank you for banking with Spark Bank. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option, please choose between 1 and 8.");
                        break;
                }
            }
        }


        // ===================== SERVICE 1 =====================
        // Add a new customer account

        static void AddAccount()
        {
            Console.WriteLine("\n===== Add New Account =====");

            // Ask for customer name
            Console.Write("Enter customer name: ");
            string customerName = Console.ReadLine();

            // Validate customer name
            if (string.IsNullOrWhiteSpace(customerName))
            {
                Console.WriteLine("Error: Customer name cannot be empty.");
                return;
            }

            // Ask for account number
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            // Validate account number
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                Console.WriteLine("Error: Account number cannot be empty.");
                return;
            }

            // Check if account number already exists
            if (accountNumbers.Contains(accountNumber))
            {
                Console.WriteLine("Error: This account number is already in use.");
                return;
            }

            // Ask for initial deposit
            Console.Write("Enter initial deposit amount: ");

            double initialDeposit;

            try
            {
                initialDeposit = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            // Initial deposit cannot be negative
            if (initialDeposit < 0)
            {
                Console.WriteLine("Error: Initial deposit cannot be negative.");
                return;
            }

            // Add all three pieces of information together
            // at the same index.
            customerNames.Add(customerName);
            accountNumbers.Add(accountNumber);
            balances.Add(initialDeposit);

            Console.WriteLine("\nAccount created successfully!");
            Console.WriteLine($"Customer Name: {customerName}");
            Console.WriteLine($"Account Number: {accountNumber}");
            Console.WriteLine($"Initial Balance: {initialDeposit:F2} OMR");
        }


        // ===================== SERVICE 2 =====================
        // Deposit money into an existing account

        static void DepositMoney()
        {
            Console.WriteLine("\n===== Deposit Money =====");

            // Ask for account number
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            // Find the account index
            int index = accountNumbers.IndexOf(accountNumber);

            // Check if account exists
            if (index == -1)
            {
                Console.WriteLine("Error: Account number not found.");
                return;
            }

            // Ask for deposit amount
            Console.Write("Enter deposit amount: ");

            double depositAmount;

            try
            {
                depositAmount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            // Deposit must be positive
            if (depositAmount <= 0)
            {
                Console.WriteLine("Error: Deposit amount must be greater than zero.");
                return;
            }

            // Add deposit to the balance
            balances[index] = balances[index] + depositAmount;

            Console.WriteLine("\nDeposit successful!");
            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Deposited Amount: {depositAmount:F2} OMR");
            Console.WriteLine($"Updated Balance: {balances[index]:F2} OMR");
        }


        // ===================== SERVICE 3 =====================
        // Withdraw money from an existing account

        static void WithdrawMoney()
        {
            Console.WriteLine("\n===== Withdraw Money =====");

            // Ask for account number
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            // Find account index
            int index = accountNumbers.IndexOf(accountNumber);

            // Check if account exists
            if (index == -1)
            {
                Console.WriteLine("Error: Account number not found.");
                return;
            }

            // Ask for withdrawal amount
            Console.Write("Enter withdrawal amount: ");

            double withdrawalAmount;

            try
            {
                withdrawalAmount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            // Withdrawal must be positive
            if (withdrawalAmount <= 0)
            {
                Console.WriteLine("Error: Withdrawal amount must be greater than zero.");
                return;
            }

            // Check if there is enough money
            if (withdrawalAmount > balances[index])
            {
                Console.WriteLine("Error: Insufficient balance.");
                Console.WriteLine($"Current Balance: {balances[index]:F2} OMR");
                return;
            }

            // Subtract withdrawal from balance
            balances[index] = balances[index] - withdrawalAmount;

            Console.WriteLine("\nWithdrawal successful!");
            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Withdrawn Amount: {withdrawalAmount:F2} OMR");
            Console.WriteLine($"Updated Balance: {balances[index]:F2} OMR");
        }


        // ===================== SERVICE 4 =====================
        // Show account balance and details

        static void ShowBalance()
        {
            Console.WriteLine("\n===== Show Balance =====");

            // Ask for account number
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            // Find account index
            int index = accountNumbers.IndexOf(accountNumber);

            // Check if account exists
            if (index == -1)
            {
                Console.WriteLine("Error: Account number not found.");
                return;
            }

            // Display account information
            Console.WriteLine("\n===== Account Details =====");
            Console.WriteLine($"Customer Name: {customerNames[index]}");
            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Current Balance: {balances[index]:F2} OMR");
        }


        // ===================== SERVICE 5 =====================
        // Transfer money from one account to another

        static void TransferAmount()
        {
            Console.WriteLine("\n===== Transfer Amount =====");

            // Ask for sender account
            Console.Write("Enter sender account number: ");
            string senderAccount = Console.ReadLine();

            // Find sender index
            int senderIndex = accountNumbers.IndexOf(senderAccount);

            // Check if sender exists
            if (senderIndex == -1)
            {
                Console.WriteLine("Error: Sender account number not found.");
                return;
            }

            // Ask for receiver account
            Console.Write("Enter receiver account number: ");
            string receiverAccount = Console.ReadLine();

            // Find receiver index
            int receiverIndex = accountNumbers.IndexOf(receiverAccount);

            // Check if receiver exists
            if (receiverIndex == -1)
            {
                Console.WriteLine("Error: Receiver account number not found.");
                return;
            }

            // Prevent transferring to the same account
            if (senderIndex == receiverIndex)
            {
                Console.WriteLine("Error: Sender and receiver cannot be the same account.");
                return;
            }

            // Ask for transfer amount
            Console.Write("Enter transfer amount: ");

            double transferAmount;

            try
            {
                transferAmount = double.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Please enter a valid number.");
                return;
            }

            // Transfer amount must be positive
            if (transferAmount <= 0)
            {
                Console.WriteLine("Error: Transfer amount must be greater than zero.");
                return;
            }

            // Check sender balance
            if (transferAmount > balances[senderIndex])
            {
                Console.WriteLine("Error: Sender does not have enough balance.");
                Console.WriteLine($"Sender's Current Balance: {balances[senderIndex]:F2} OMR");
                return;
            }

            // Subtract money from sender
            balances[senderIndex] = balances[senderIndex] - transferAmount;

            // Add money to receiver
            balances[receiverIndex] = balances[receiverIndex] + transferAmount;

            Console.WriteLine("\nTransfer successful!");

            Console.WriteLine("\nSender Details:");
            Console.WriteLine($"Account Number: {accountNumbers[senderIndex]}");
            Console.WriteLine($"Updated Balance: {balances[senderIndex]:F2} OMR");

            Console.WriteLine("\nReceiver Details:");
            Console.WriteLine($"Account Number: {accountNumbers[receiverIndex]}");
            Console.WriteLine($"Updated Balance: {balances[receiverIndex]:F2} OMR");
        }


        // ===================== CUSTOM SERVICE 6 =====================
        // Display all accounts

        static void ListAllAccounts()
        {
            Console.WriteLine("\n===== All Accounts =====");

            // Check if there are no accounts
            if (accountNumbers.Count == 0)
            {
                Console.WriteLine("There are currently no accounts in the system.");
                return;
            }

            // Loop through all accounts
            for (int i = 0; i < accountNumbers.Count; i++)
            {
                Console.WriteLine($"\nAccount {i + 1}");
                Console.WriteLine($"Customer Name: {customerNames[i]}");
                Console.WriteLine($"Account Number: {accountNumbers[i]}");
                Console.WriteLine($"Balance: {balances[i]:F2} OMR");
            }

            Console.WriteLine($"\nTotal Number of Accounts: {accountNumbers.Count}");
        }


        // ===================== CUSTOM SERVICE 7 =====================
        // Close an existing account

        static void CloseAccount()
        {
            Console.WriteLine("\n===== Close Account =====");

            // Ask for account number
            Console.Write("Enter account number to close: ");
            string accountNumber = Console.ReadLine();

            // Find account index
            int index = accountNumbers.IndexOf(accountNumber);

            // Check if account exists
            if (index == -1)
            {
                Console.WriteLine("Error: Account number not found.");
                return;
            }

            // Display account before closing
            Console.WriteLine("\nAccount found:");
            Console.WriteLine($"Customer Name: {customerNames[index]}");
            Console.WriteLine($"Account Number: {accountNumbers[index]}");
            Console.WriteLine($"Current Balance: {balances[index]:F2} OMR");

            // Ask for confirmation
            Console.Write("Are you sure you want to close this account? (Y/N): ");
            string confirmation = Console.ReadLine();

            if (confirmation.ToLower() != "y")
            {
                Console.WriteLine("Account closure cancelled.");
                return;
            }

            // Remove the account from all three lists
            // at the same index.
            customerNames.RemoveAt(index);
            accountNumbers.RemoveAt(index);
            balances.RemoveAt(index);

            Console.WriteLine("Account closed successfully.");
        }
    }
}
