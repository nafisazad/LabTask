using System;

class Program
{
    static void Main()
    {
        int[,] matrix = new int[3, 3];
        int sum = 0;

        Console.WriteLine("Enter elements of the 3x3 matrix:");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write("Element [" + i + "," + j + "]: ");
                matrix[i, j] = Convert.ToInt32(Console.ReadLine());

                sum += matrix[i, j];
            }
        }

        Console.WriteLine("\nMatrix:");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("\nSum of all elements = " + sum);
    }
}
