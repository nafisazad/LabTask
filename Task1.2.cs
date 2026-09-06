using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of students: ");
        int students = Convert.ToInt32(Console.ReadLine());

        int[][] marks = new int[students][];

        for (int i = 0; i < students; i++)
        {
            Console.Write("Enter number of courses for Student " + (i + 1) + ": ");
            int courses = Convert.ToInt32(Console.ReadLine());

            marks[i] = new int[courses];

            for (int j = 0; j < courses; j++)
            {
                Console.Write("Enter mark for Course " + (j + 1) + ": ");
                marks[i][j] = Convert.ToInt32(Console.ReadLine());
            }
        }

        Console.WriteLine("\nStudent Marks and Total Marks:");

        for (int i = 0; i < students; i++)
        {
            int sum = 0;

            Console.Write("Student " + (i + 1) + " Marks: ");

            for (int j = 0; j < marks[i].Length; j++)
            {
                Console.Write(marks[i][j] + " ");
                sum += marks[i][j];
            }

            Console.WriteLine("\nTotal Marks = " + sum);
            Console.WriteLine();
        }
    }
}
