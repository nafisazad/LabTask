using System;
class Employee
{
    public int ID;
    public string Name;

      public Employee  (int id, string name)
    {
        ID= id;
        Name = name;
        Console.WriteLine("Employee ID   = " + ID);
        Console.WriteLine("Employee Name = " + Name);
        }
}

class PermanentEmployee : Employee  {
    public double BasicSalary;
    public double Bonus   ;
    public PermanentEmployee(int id, string name , double basicSalary, double bonus)  : base(id, name)
    {
        BasicSalary = basicSalary;
        Bonus = bonus;

        double TotalSalary = BasicSalary + Bonus;

        Console.WriteLine("Basic Salary = " + BasicSalary);
        Console.WriteLine("Bonus = " + Bonus);
        Console.WriteLine("Total Salary = " + TotalSalary);
    }
}

class Program
{
    static void Main()
    {

        PermanentEmployee e = new PermanentEmployee(25-61338-1, "Nafis", 50000, 10000);
        PermanentEmployee s = new   PermanentEmployee(25-61338-1, "Nafis2", 50000, 10000);

    }
}
