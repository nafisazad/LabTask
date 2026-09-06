using System;
 
 class Vehicle  
{
    public virtual void Start()
    {
        Console.WriteLine("starting the vehicle ");
    }

}

 class Car: Vehicle  
{
    public sealed override void Start()
    {
        Console.WriteLine("starting the car by overriding the Start method ");
    }
}

class Bike : Vehicle 

    public  override void Start()
    {
        Console.WriteLine("starting the bike by overriding the Start method ");
    }
}
class Truck : Vehicle  
{
    public new void Start()
    {
        Console.WriteLine("starting the Truck by overriding the Start method ");
    }
}

/*class Sportscar : Car
{
    public override void Start()
    {
        Console.WriteLine("starting the sportscar by overriding the Start method ");
    }
}
*/

class Program
{
    static void Main()
    {
        Console.WriteLine("Demonstrating runtime polymorphism: ");
        Vehicle v1;  
        Console.WriteLine();

        v1 = new Car();
        v1.Start();
        Console.WriteLine();

        v1 = new Bike();
        v1.Start();
        Console.WriteLine();


        Console.WriteLine("method hiding ");

        Vehicle  v2 = new Truck(); 
        v2.Start();

        Truck t1 = new Truck();  
        t1.Start();

        Console.WriteLine();
        Console.WriteLine("demostrating runtime polymorphism");

        Vehicle v = new Car();
        v.Start();  


        Car c = new Car();      
        c.Start();   

    }

}
