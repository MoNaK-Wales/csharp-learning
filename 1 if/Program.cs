using System;

namespace @if
{
    class Program
    {
        static void Main(string[] args)
        {
            int age;

            while (true)
            {
                Console.Write("возраст: ");
                age = Convert.ToInt32(Console.ReadLine());

                if (age > 125 || age <= 0)
                {
                    Console.WriteLine("некорректный возраст");
                }
                else if (age > 11)
                {
                    Console.Write("имя: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("привет, " + name);
                    break;
                }
                else
                {
                    Console.WriteLine("подрасти чуть-чуть");
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
