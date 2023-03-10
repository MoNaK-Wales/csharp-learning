using System;

namespace enums
{
    //перечисление - это определяемый тип, в котором мы указываем допустимые значения в виде набора констант
    enum DayTime        //по сути создался тип DayTime, с помощью которого можно создавать переменные
    {
        Morning,
        Afternoon,
        Evening,
        Night
    }

    //каждая константа перечисления имеет своё значение и тип. по умолчанию они типа int и нумеруются от 0. перечислению можно указать тип, а константам явно указать значения
    //тип перечисления может быть только целочисельным: byte, sbyte, short, ushort, int, uint, long, ulong
    enum PersonData : ushort     //все константы этого перечисления будут иметь тип ushort
    {
        Name,       //0     первая константа перечисления по умолчанию равен 0. каждый следующий элемент имеет значение на 1 больше чем предыдущий
        Surname,    //1
        Age,        //2
        Sex = 9,    //9     явно указали значение. дальше отсчёт идет от этого значения
        Country,    //10
        Job = Age,  //2     можно присваивать константе значение другой константы
        City = 9    //9     можно указывать одинаковые значения
    }
    class Program
    {
        static void Main(string[] args)
        {
            DayTime dayTime = DayTime.Evening;  //тут создалась переменная dayTime класса DayTime, значение которой - Evening перечисления DayTime
            Console.WriteLine(dayTime);         //Evening

            //зачастую перечисления используются как хранилище состояния, поэтому нередко они используются с конструкцией switch (тут в методе GoodDay)
            GoodDay(DayTime.Evening);   //Добрый вечер!
            GoodDay(DayTime.Morning);   //Доброе утро!
            GoodDay(DayTime.Night);     //Доброй ночи!

            PersonData data = PersonData.Surname;
            //чтобы получить значение элемента перечисления: (тип)элемент. может происходить приведения типов: PersonData имеет тип ushort, но мы получили intЫ
            Console.WriteLine("\n"+(int)data);

            //элементы перечисления могут ин/декрементироваться. в таком случае им присваивается следующая/предыдущая по значению константа. также можно использовать другие операторы
            Console.WriteLine($"Элемент: {++data}; Значение: {(short)data}");  //если несколько элементов имеют одинаковое значение, присваивается последняя созданная с этим значением константа: Job, 2
            Console.WriteLine($"Элемент: {++data}; Значение: {(short)data}");  //если константы со значением после де/инкремента нет, то элемент будет равен значению: 3, 3
            data += 6;  //3+9 = 9
            Console.WriteLine($"Элемент: {data}; Значение: {(short)data}");    //теперь значение 9. элементу City присвоилось значение позже, чем Sex, поэтому вывод: City, 9
            Console.WriteLine($"Элемент: {++data}; Значение: {(short)data}");  //Country, 10

            Console.Read();
        }

        static void GoodDay(DayTime a)  //метод принимает параметр типа DayTime
        {
            switch (a)
            {
                case DayTime.Morning:
                    Console.WriteLine("Доброе утро!");
                    break;

                case DayTime.Afternoon:
                    Console.WriteLine("Добрый день!");
                    break;

                case DayTime.Evening:
                    Console.WriteLine("Добрый вечер!");
                    break;

                case DayTime.Night:
                    Console.WriteLine("Доброй ночи!");
                    break;                
            }
        }
    }
}
