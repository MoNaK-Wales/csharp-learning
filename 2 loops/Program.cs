using System;

namespace loops
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            while (++x < 5)
            {
                Console.WriteLine("x=" + x);   //1, 2, 3, 4        
            }
            Console.WriteLine("");

            int z = 0;
            do
            {
                Console.WriteLine("z=" + z);  //0, 1, 2, 3, 4, 5
            } while (++z < 5);
            Console.WriteLine("");    /*do-while проверяет условие и инкрементирует после выполнения, 
                                     1 раз задача точно будет выполнена*/

            /*for принимает 3 аргумента: 
            первый - это действие, которое делается один раз перед началом функции, обычно объявляет переменную
            второй - это условие, как в while, при котором выполняется цикл
            третий - это действие, которое делается каждый раз при окончании итерации, обычно увеличение или 
                                                                                       уменьшение переменной*/
            for (int forx = 1; forx < 6; forx++)
            {
                Console.WriteLine("for " + forx);
            }
            Console.WriteLine("");

            /*необязательно именно в первой части цикла объявлять переменную, 
              а в третий части изменять ее значение - это могут быть любые действия*/

            int forz = 0;
            for (Console.WriteLine("начало цикла"); forz < 6; Console.WriteLine(forz + " итерация цикла"))
            {
                forz++;
            }  //так как оно пишет значение после выполнения цикла, без условий, значение 6 также записывается
            Console.WriteLine("");

            /*можно записать вовсе без аргументов (for (; ;)), но тогда цикл будет бесконечным,
             но можно опустить только некоторые блоки*/

            int fory = 1;
            for (; fory < 6;)
            {
                Console.WriteLine(fory++);
            }

            string[] names = { "Mike", "John", "Paul", "Kim", "Jack" };
            foreach(string n in names)          //foreach похож на for из пайтона, он итерируется по массиву, каждый элемент
            {                                   //которого приравнивается к переменной. тут при итерациях n=names[0], n=names[1]...
                Console.WriteLine("Hi, " + n);
            }


            Console.ReadLine();
        }
    }
}
