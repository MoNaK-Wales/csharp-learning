using System;

namespace methods
{
    class Program
    {
        static string SayHello(string name)
        {
            return "Hello, " + name;
        }
        static void Hello(string name)
        {
            Console.WriteLine("Hello, " + name);
        }
        static void ShortHello(string name) => Console.WriteLine("Hello, " + name);
        static void DefaultHello(string name = "имя по умолчанию")
        {
            Console.WriteLine("Hello, " + name);
        }
        static void PrintPerson(string name, int age, string country, string company)
        {
            Console.WriteLine($"Name: {name}  Age: {age}  Country: {country}  Company: {company}");
        }
        static void sqr(int a)
        {
            a *= a;
            Console.WriteLine("a в обычном методе: " + a);
        }
        static void refsqr(ref int a)
        {
            a *= a;
            Console.WriteLine("a в методе c ref: " + a);
        }
        static void GetRectangleData(int width, int height, out int rectArea, out int rectPerimetr)
        {
            rectArea = width * height;
            rectPerimetr = (width + height) * 2;
        }
        static void InGetRectangleData(in int width, in int height, out int rectArea, out int rectPerimetr)
        {
            rectArea = width * height;
            rectPerimetr = (width + height) * 2;
        }
        static void Print(int a) => Console.WriteLine("Int:" + a);
        static void Print(float a) => Console.WriteLine("Float:" + a);
        static void Print(double a) => Console.WriteLine("double:" + a);
        static void Print(string a) => Console.WriteLine("string:" + a);
        static void Print(bool a) => Console.WriteLine("bool:" + a);
        static void Print(char a) => Console.WriteLine("char:" + a);
        /*static void DrawPyramid(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                for(int j = i; j <= n; j++)
                {
                    Console.Write("  ");
                }
                for(int k=1; k <= 2*i-1; k++)
                {
                    Console.Write("*"+" ");
                }
                Console.WriteLine("");
            }
        }
        static int Points(int levels)
        {
            if (levels == 1)
            {
                return 1;
            }
            return levels + Points(levels-1);
        }*/

        static void Main(string[] args)
        {
            /*
            static string SayHello(string name)        в объявлении метода (функции) указывается модификатор (тут static),
            {                                        тип возвращаемого значения (тут string), то есть тип, который
                return "Hello, "+name;               возвращает метод, название и параметры (нужно указывать тип)
            }
            */
            Console.WriteLine(SayHello("метод"));

            /*
            static void Hello(string name)           если функция ничего не возвращает, используется тип void
            {
                Console.WriteLine("Hello, "+name);
            }
            */
            Hello("void");

            //static void ShortHello(string name) => Console.WriteLine("Hello, "+name)
            //если метод содержит одну инструкцию, можно её укоротить с =>
            ShortHello("короткий метод");

            /*
            static void DefaultHello(string name = "имя по умолчанию")   можно указать параметру значение по умолчанию,
            {                                                            которое используется, если не был введён аргумент,
                Console.WriteLine("Hello, "+name);                       но их нужно указывать после основых параметров
            }
            */
            DefaultHello();

            /*
            static void PrintPerson(string name, int age, string country, string company)
            {
                Console.WriteLine($"Name: {name}  Age: {age}  Country: {country}  Company: {company}");
            }
            */
            PrintPerson("Jake", 24, "Ukraine", "Microsoft");
            //обычно, аргументы передаются в том порядке, как записаны параметры
            PrintPerson(age: 24, company: "Microsoft", name: "Jake", country: "Ukraine");
            //чтобы не запутаться, можно указывать, какой параметр вы указываете

            /*
            static void sqr(int a)
            {                             когда в метод, в качестве аргумента, передаётся переменная, по сути, передаётся
                a *= a;                   только её значение, и после вполнения метода переменная останется прежней
                Console.WriteLine("a в обычном методе: "+a)
            }
            */
            int a = 4;
            sqr(a);
            Console.WriteLine("а после обычного метода: " + a);

            /*
            static void refsqr(ref int a)
            {                                можно передавать непосредственно переменную, написав ref перед параметром и
                a *= a;                      аргументом. таким образом действия в методе происходят с самой переменной
                Console.WriteLine("a в методе c ref: "+a)
            }
            */
            a = 4;
            refsqr(ref a);
            Console.WriteLine("a после метода c ref: " + a);

            /*
            static void GetRectangleData(int width, int height, out int rectArea, out int rectPerimetr)
            {                                     модификатор out делает параметры входными. то есть значения будет
                rectArea = width * height;        возвращаться не через return, а через выходные параметры 
                rectPerimetr = (width + height) * 2;           out нужно писать и в параметрах и в вызове метода
            }
            */

            GetRectangleData(10, 20, out var area, out var perimetr); //можно объявлять переменные сразу в вызове метода,
            Console.WriteLine($"Out  Area: {area}, perimetr: {perimetr}");//и использовать var, если не известен тип значений

            /*
            static void InGetRectangleData(in int width, in int height, out int rectArea, out int rectPerimetr)
            {
                rectArea = width * height;               также можно передавать входные параметры с модификатором in.
                rectPerimetr = (width + height) * 2;     он указывает, что передаётся сам параметр по ссылке, однако в
            }                                            методе он изменятся не будет.
                                                         in нужно писать в параметрах, в вызове метода он не обязателен
            */
            int width = 20;
            int height = 10;
            InGetRectangleData(width, height, out var inarea, out var inperimetr);
            Console.WriteLine($"Outin  Area: {inarea}, perimetr: {inperimetr}");

            /*
            static void Print(int a) => Console.WriteLine("Int:"+a);         так как параметр должен иметь определённый
            static void Print(float a) => Console.WriteLine("Float:"+a);     тип, можно перегружать методы, чтобы 
            static void Print(double a) => Console.WriteLine("double:"+a);   создать метод с тем же названием, но
            static void Print(string a) => Console.WriteLine("string:"+a);   разными параметрами.
            static void Print(bool a) => Console.WriteLine("bool:"+a);       тут мы перегрузили метод Print(укороченная
            static void Print(char a) => Console.WriteLine("char:"+a);       запись) для 6 разных типов значений
            */
            Print(32.36721938332);  //в зависимости от типа значения аргумента, выбирается перегрузка метода под него
            Print(false);
            Print(7.38f);
            Print("hi");
            Print('n');
            Print(75);



            /* DrawPyramid(Convert.ToInt32(Console.ReadLine()));
             Console.WriteLine(Points(Convert.ToInt32(Console.ReadLine())));*/
            Console.ReadLine();
        }
    }
}
