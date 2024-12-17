namespace BankAccountApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccountManager accountManager = new BankAccountManager();
            List<BankAccount> accounts = accountManager.CreateBankAccounts(20);

            accountManager.SimulateTransactions(accounts, 100);

            accountManager.SimulateTransfers(accounts);
        }
    }

    class BankAccountManager
    {
        public List<BankAccount> CreateBankAccounts(int numberOfAccounts)
        {
            List<BankAccount> accounts = new List<BankAccount>();
            int createdAccounts = 0;

            while (createdAccounts < numberOfAccounts)
            {
                try
                {
                    double initialBalance = RandomGenerator.GenerateRandomBalance(10, 50000);
                    string accountHolderName = RandomGenerator.GenerateRandomAccountHolder();
                    string accountType = RandomGenerator.GenerateRandomAccountType();
                    DateTime dateOpened = RandomGenerator.GenerateRandomDateOpened();
                    BankAccount account = new BankAccount($"Account {createdAccounts + 1}", initialBalance, accountHolderName, accountType, dateOpened);
                    accounts.Add(account);
                    createdAccounts++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Account creation failed: {ex.Message}");
                }
            }

            return accounts;
        }

        public void SimulateTransactions(List<BankAccount> accounts, int numberOfTransactions)
        {
            foreach (BankAccount account in accounts)
            {
                for (int i = 0; i < numberOfTransactions; i++)
                {
                    double transactionAmount = RandomGenerator.GenerateRandomBalance(-500, 500);
                    try
                    {
                        if (transactionAmount >= 0)
                        {
                            account.Credit(transactionAmount);
                            Console.WriteLine($"Credit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                        else
                        {
                            account.Debit(-transactionAmount);
                            Console.WriteLine($"Debit: {transactionAmount}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Transaction failed: {ex.Message}");
                    }
                }

                Console.WriteLine($"Account: {account.AccountNumber}, Balance: {account.Balance.ToString("C")}, Account Holder: {account.AccountHolderName}, Account Type: {account.AccountType}");
            }
        }

        public void SimulateTransfers(List<BankAccount> accounts)
        {
            foreach (BankAccount fromAccount in accounts)
            {
                foreach (BankAccount toAccount in accounts)
                {
                    if (fromAccount != toAccount)
                    {
                        try
                        {
                            double transferAmount = RandomGenerator.GenerateRandomBalance(0, fromAccount.Balance);
                            fromAccount.Transfer(toAccount, transferAmount);
                            Console.WriteLine($"Transfer: {transferAmount.ToString("C")} from {fromAccount.AccountNumber} ({fromAccount.AccountHolderName}, {fromAccount.AccountType}) to {toAccount.AccountNumber} ({toAccount.AccountHolderName}, {toAccount.AccountType})");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Transfer failed: {ex.Message}"); 
                        }
                    }
                }
            }
        }
    }

    static class RandomGenerator
    {
        public static double GenerateRandomBalance(double min, double max)
        {
            return GenerateRandomDouble(min, max);
        }

        public static string GenerateRandomAccountHolder()
        {
            string[] accountHolderNames = 
            { 
                "John Smith", "Maria Garcia", "Mohammed Khan", "Sophie Dubois", 
                "Liam Johnson", "Emma Martinez", "Noah Lee", "Olivia Kim", 
                "William Chen", "Ava Wang", "James Brown", "Isabella Nguyen", 
                "Benjamin Wilson", "Mia Li", "Lucas Anderson", "Charlotte Liu", 
                "Alexander Taylor", "Amelia Patel", "Daniel Garcia", "Sophia Kim" 
            };

            return accountHolderNames[GenerateRandomInt(0, accountHolderNames.Length)];
        }

        public static string GenerateRandomAccountType()
        {
            string[] accountTypes = { "Savings", "Checking", "Money Market", "Certificate of Deposit", "Retirement" };
            return accountTypes[GenerateRandomInt(0, accountTypes.Length)];
        }

        public static DateTime GenerateRandomDateOpened()
        {
            DateTime startDate = new DateTime(DateTime.Today.Year - 10, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            DateTime randomDate = startDate.AddDays(GenerateRandomInt(0, range));

            // Ensure the date is not in the future
            if (randomDate >= DateTime.Today)
            {
                randomDate = randomDate.AddDays(-1);
            }

            return randomDate;
        }

        private static double GenerateRandomDouble(double min, double max)
        {
            Random random = new Random();
            double value = random.NextDouble() * (max - min) + min;
            return Math.Round(value, 2);
        }

        private static int GenerateRandomInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}