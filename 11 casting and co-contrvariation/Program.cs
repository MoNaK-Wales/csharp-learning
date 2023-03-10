using System.Globalization;

namespace casting
{
    class Program
    {
        static void Main(string[] args)
        {
            Header("Преобразование типов");
            Casting.CastingMain();
            Header("Ко/контрвариантность");
            Variance.VarianceMain();
            Console.WriteLine(people(new List<int[]>() { new[] { 10, 0 }, new[] { 3, 5 }, new[] { 5, 8 } }));
            int[] i = new int[2];
            //i.su
        }
        static void Header(string t)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t" + t);
            Console.ResetColor();
        }
        static int people(List<int[]> peopleListInOut)
        {
            Func<int[], int> func = arr => arr[0] - arr[1];
            return peopleListInOut.Sum<int[]>(func);
        }
    }
}