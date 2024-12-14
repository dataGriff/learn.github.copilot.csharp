using System;
using Xunit;
using BankAccount;

namespace BankAccount.UnitTests
{
    public class BankAccountTest
    {
        [Fact]
        public void Credit_ShouldIncreaseBalance()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Credit(500);

            // Assert
            Assert.Equal(1500, account.Balance);
        }

        [Fact]
        public void Debit_ShouldDecreaseBalance_WhenSufficientFunds()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            account.Debit(500);

            // Assert
            Assert.Equal(500, account.Balance);
        }

        [Fact]
        public void Debit_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act & Assert
            Assert.Throws<Exception>(() => account.Debit(1500));
        }

        [Fact]
        public void Transfer_ShouldTransferFunds_WhenSufficientFunds()
        {
            // Arrange
            var account1 = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Doe", "Savings", DateTime.Now);

            // Act
            account1.Transfer(account2, 500);

            // Assert
            Assert.Equal(500, account1.Balance);
            Assert.Equal(1000, account2.Balance);
        }

        [Fact]
        public void Transfer_ShouldThrowException_WhenInsufficientFunds()
        {
            // Arrange
            var account1 = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Doe", "Savings", DateTime.Now);

            // Act & Assert
            Assert.Throws<Exception>(() => account1.Transfer(account2, 1500));
        }

        [Fact]
        public void Transfer_ShouldThrowException_WhenExceedsLimitForDifferentOwners()
        {
            // Arrange
            var account1 = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);
            var account2 = new BankAccount("456", 500, "Jane Doe", "Savings", DateTime.Now);

            // Act & Assert
            Assert.Throws<Exception>(() => account1.Transfer(account2, 600));
        }

        [Fact]
        public void CalculateInterest_ShouldReturnCorrectInterest()
        {
            // Arrange
            var account = new BankAccount("123", 1000, "John Doe", "Savings", DateTime.Now);

            // Act
            var interest = account.CalculateInterest(0.05);

            // Assert
            Assert.Equal(50, interest);
        }
    }
}
