using System;

namespace static_and_inbuilt_classes
{
    class Program
    {   
        class ForStatic
        {
            /*члены класса, могут быть статическими, если использовать static
            статические члены класса, принадлежат не к какому-то экземпляру класса, а к самому классу. при любых количествах
            объектов класса, существует только одна копия статического члена*/

            public static int some_static_int = 14;  //статические поля хранят значение для всего класса, то есть принадлежат всем объектам

            //таким же образом можно создавать статические свойства:
            static int averageAge;                  //для статического свойства, поле должно быть тоже статическим
            public static int AverageAge
            {
                get { return averageAge; }
                set { if (value > 1 && value < 100) averageAge = value; }
            }

            //статические методы определяют общее для всех объектов поведение.
            //в таких методах обращаться можно только к стат. членам, так как нестатические переменные у каждого объекта разные
            public int age;
            public static bool isOlder(ForStatic a)
            {                                   //метод, проверяющий, старше ли среднего возраста, принимает в качестве параметра класс
                return a.age > averageAge;      //так как поле age у каждого объекта разное, поэтому мы передаём его через объект
            }                                   //а averageAge статическое, поэтому его можно передавать без ссылки

            //существуют статические конструкторы. они не должны иметь модификатор доступа и не принимают параметров, нельзя использовать "this",
            //и можно обращаться только к статическим членам класса. Главное отличие - он вызывается автоматически при самом первом
            //создании объекта данного класса или при первом обращении к его статическим членам
            static ForStatic()  //без модификатора доступа
            {
                Console.WriteLine("Первый вызова класса ForStatic\n");    
            }

        }
        static class Some_static_class      //можно объявить статический класс. он может иметь только статические члены класса.
        {                                   //Экземпляр статического класса создать нельзя
            //private static string name = "My name is Maksim";
            public const float x = 0.0f;    //константные члены являются статическими по определению (необязательно только в статическом классе)
            public static void some_static_method() => Console.WriteLine("метод статического класса");

        }
        static void Main(string[] args)
        {
            //чтобы получить или изменить статическую переменную, нужно обращаться через класс. если обратиться через объект, будет ошибка
            Console.WriteLine(ForStatic.some_static_int);       //статические переменные можно использовать, даже когда нет ни одного объекта: 14
                                                                //мы впервые обратились к классу, вызывается статический конструктор
            ForStatic forStatic = new ForStatic();
            ForStatic.some_static_int = 35;                     //статические переменные можно изменять, так же обращаясь через имя класса
            Console.WriteLine(ForStatic.some_static_int+"\n");  //35

            ForStatic.AverageAge = 31;                      //статическое свойство указывается так же, как статическое поле
            ForStatic.AverageAge = 312;                     //значение не подходит под условие, не изменится
            Console.WriteLine(ForStatic.AverageAge+"\n");   //31

            forStatic.age = 27;
            Console.WriteLine("возраст больше среднего? " + ForStatic.isOlder(forStatic));      //27>31==False
            //isOlder статический, обращаемся через класс и передаём объект в качестве аргумента, в методе уже извлекается поле age
            forStatic.age = 45;
            Console.WriteLine("возраст больше среднего? " + ForStatic.isOlder(forStatic)+"\n"); //45>31==True

            //Some_static_class n = new Some_static_class();    //нельзя создавать экземпляр статического класса
            Console.WriteLine($"константа статического класса {Some_static_class.x}");
            Some_static_class.some_static_method();
            Console.WriteLine();

            //существует множество полезных и часто используемых статических классов: Math, Array, String и DateTim
            //статический класс Math имеет 2 константы и большое количество методов:
            Console.WriteLine($"число пи: {Math.PI}, число e {Math.E}");    //константы пи и е с 14 числами после запятой
            Console.WriteLine(Math.Max(150, 245));      //возвращает самое большое число из двух:   245
            Console.WriteLine(Math.Min(150, 245));      //возвращает самое маленькое число из двух: 150
            Console.WriteLine(Math.Abs(-23));           //возвращает модуль числа: 23
            Console.WriteLine(Math.Pow(5, 3));          //возвращает первое число, возведённое в второе число степень: 125
            Console.WriteLine(Math.Round(23.328, 1));   //возвращает первое число, округленное до второе число знаков после запятой.
                                                        //b по умолчанию равно 0. Вывод: 23,3
            Console.WriteLine(Math.Sqrt(196));          //возвращает квадратный корень числа: 14       
            Console.WriteLine(Math.Truncate(24.53));    //возвращает указанное число, убирая всю дробную часть: 24
            Console.WriteLine(Math.Sin(90*Math.PI/180));//возвращает синус угла (угол указывается в радианах):   1
            Console.WriteLine(Math.Cos(Math.PI));       //возвращает косинус угла (угол указывается в радианах): -1

            Console.WriteLine();


            //некоторые методы статического класса Array
            int[] some_array = { 2, 4, -9, 10 };    
            string[] some_array2 = { "Alice", "Tom", "Sam", "Bob", "Kate", "Alice" };
            printArr("до методов", some_array);                     //printArr - самосозданный перегруженный массив
            printArr("до методов", some_array2);
            
            Array.Reverse(some_array);                              //Reverse меняет расположение элементов в обратном порядке
            Array.Reverse(some_array2);                                         
            printArr("после Reverse", some_array);
            printArr("после Reverse", some_array2);

            Array.Sort(some_array);                                 //Sort размещает числа от меньшего к большему
            Array.Sort(some_array2, 0, some_array2.Length-1);       //строки размещает по алфавиту. Можно указать часть массива для сортировки
            printArr("после Sort", some_array);
            printArr("после Sort, кроме последнего", some_array2);  //так как мы указали сортировать все-1 элементы от 0, поэтому "Alice" остаётся

            Console.WriteLine();


            //статический класс String имеет такие методы:
            string s1 = "first text";
            string s2 = "beautiful text";

            Console.WriteLine(String.Concat(s1, s2));   //Concat объединяет 2 или несколько строк
            Console.WriteLine(String.Equals(s1, s2));   //проверяет на равенство строки

            Console.WriteLine();


            //DateTime позволяет работать с датами:                                     
            Console.WriteLine("Сейчас: "+DateTime.Now);                                 //возвращает дату и время сейчас
            Console.WriteLine("Сегодня: "+DateTime.Today);                              //возвращает сегодняшнюю дату
            Console.WriteLine("Дней в феврале 2020: "+DateTime.DaysInMonth(2020, 2));   //возвращает к-во дней в указанном месяце указанного года




            Console.ReadLine();
        }

        static void printArr(string text, int[] arr)
        {
            Console.Write("Массив чисел " + text+": { ");
            foreach(var i in arr)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine("}");
        }
        static void printArr(string text, string[] arr)
        {
            Console.Write("Массив строк " + text+": { ");
            foreach(var i in arr)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine("}");
        }
    }
}
