using System;

namespace BankAccountApp
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("Insufficient balance for debit.")
        {
        }

        public InsufficientBalanceException(string message) : base(message)
        {
        }

        public InsufficientBalanceException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class InvalidAmountException : Exception
    {
        public InvalidAmountException() : base("Invalid amount.")
        {
        }

        public InvalidAmountException(string message) : base(message)
        {
        }

        public InvalidAmountException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    public class BankAccount
    {
        public string AccountNumber { get; }
        public double Balance { get; private set; }
        public string AccountHolderName { get; }
        public string AccountType { get; }
        public DateTime DateOpened { get; }

        public BankAccount(string accountNumber, double initialBalance, string accountHolderName, string accountType, DateTime dateOpened)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be null or empty.", nameof(accountNumber));
            if (initialBalance < 0)
                throw new ArgumentException("Initial balance cannot be negative.", nameof(initialBalance));
            if (string.IsNullOrWhiteSpace(accountHolderName))
                throw new ArgumentException("Account holder name cannot be null or empty.", nameof(accountHolderName));
            if (string.IsNullOrWhiteSpace(accountType))
                throw new ArgumentException("Account type cannot be null or empty.", nameof(accountType));

            AccountNumber = accountNumber;
            Balance = initialBalance;
            AccountHolderName = accountHolderName;
            AccountType = accountType;
            DateOpened = dateOpened;
        }

        public void Credit(double amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Credit amount must be positive.");

            Balance += amount;
        }

        public void Debit(double amount)
        {
            if (amount <= 0)
                throw new InvalidAmountException("Debit amount must be positive.");

            if (Balance >= amount)
            {
                Balance -= amount;
            }
            else
            {
                throw new InsufficientBalanceException();
            }
        }

        public double GetBalance()
        {
            return Balance;
        }
    }
}