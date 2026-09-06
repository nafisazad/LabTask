using System;

enum AccountType
{
    Savings,
    Current,
    PremiumSavings
}

struct Address
{
    public string City;
    public string Country;

    public Address(string city, string country)
    {
        City = city;
        Country = country;
    }
}

class Account
{
    private int accountNumber;
    private string accountHolder;
    private double balance;

    public static int TotalAccounts;

    public const string BankName = "Smart Bank";
    public readonly DateTime CreatedDate;

    public double? LoanAmount;

    public Address Address;

    public int AccountNumber
    {
        get { return accountNumber; }
        set { accountNumber = value; }
    }

    public string AccountHolder
    {
        get { return accountHolder; }
        set { accountHolder = value; }
    }

    public double Balance
    {
        get { return balance; }
        set { balance = value; }
    }

    static Account()
    {
        Console.WriteLine("Static Constructor Called");
        TotalAccounts = 0;
    }

    public Account() : this(0, "Unknown", 0)
    {
        Console.WriteLine("Default Constructor");
    }

    public Account(int accNo, string holder, double bal)
    {
        accountNumber = accNo;
        accountHolder = holder;
        balance = bal;
        CreatedDate = DateTime.Now;
        TotalAccounts++;
        Console.WriteLine("Parameterized Constructor");
    }

    public Account(Account a)
    {
        accountNumber = a.accountNumber;
        accountHolder = a.accountHolder;
        balance = a.balance;
        CreatedDate = a.CreatedDate;
        Console.WriteLine("Copy Constructor");
    }

    ~Account()
    {
        Console.WriteLine("Destructor Called");
    }

    public virtual double CalculateInterest()
    {
        return balance * 0.02;
    }

    public void Deposit(double amount)
    {
        balance += amount;
    }

    public void Deposit(ref double amount)
    {
        balance += amount;
        amount = 0;
    }

    public void Deposit(out double amount)
    {
        amount = 1000;
        balance += amount;
    }

    public void Deposit(params double[] amounts)
    {
        foreach (double amt in amounts)
            balance += amt;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Acc No: {accountNumber}");
        Console.WriteLine($"Holder: {accountHolder}");
        Console.WriteLine($"Balance: {balance}");
    }
}

class SavingsAccount : Account
{
    public SavingsAccount(int id, string name, double bal)
        : base(id, name, bal)
    {
    }

    public override double CalculateInterest()
    {
        return Balance * 0.05;
    }
}

class CurrentAccount : Account
{
    public CurrentAccount(int id, string name, double bal)
        : base(id, name, bal)
    {
    }

    public new double CalculateInterest()
    {
        return 0;
    }
}

class PremiumSavings : SavingsAccount
{
    public PremiumSavings(int id, string name, double bal)
        : base(id, name, bal)
    {
    }

    public sealed override double CalculateInterest()
    {
        return Balance * 0.10;
    }
}

class Program
{
    static void Main()
    {
        int x = 10;
        string str = "Banking";

        double d = 50.75;
        int n = (int)d;

        Console.WriteLine($"Casting: {n}");

        SavingsAccount s1 =
            new SavingsAccount(101, "Nafis", 10000);

        CurrentAccount c1 =
            new CurrentAccount(102, "Azad", 20000);

        PremiumSavings p1 =
            new PremiumSavings(103, "Rahim", 30000);

        s1.Address = new Address("Dhaka", "Bangladesh");

        Account acc;

        acc = s1;
        Console.WriteLine("Savings Interest: " + acc.CalculateInterest());

        acc = p1;
        Console.WriteLine("Premium Interest: " + acc.CalculateInterest());

        Console.WriteLine("Current Interest: " + c1.CalculateInterest());

        double amount = 500;
        s1.Deposit(ref amount);

        double outAmount;
        s1.Deposit(out outAmount);

        s1.Deposit(100, 200, 300);

        s1.ShowInfo();

        int[] arr1 = { 1, 2, 3, 4, 5 };

        foreach (int i in arr1)
            Console.Write(i + " ");

        Console.WriteLine();

        int[,] arr2 =
        {
            {1,2,3},
            {4,5,6}
        };

        Console.WriteLine("Rows: " + arr2.GetLength(0));
        Console.WriteLine("Cols: " + arr2.GetLength(1));

        int[][] jagged =
        {
            new int[]{1,2},
            new int[]{3,4,5},
            new int[]{6,7,8,9}
        };

        foreach (int[] row in jagged)
        {
            foreach (int item in row)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Total Accounts: " + Account.TotalAccounts);
    }
}
