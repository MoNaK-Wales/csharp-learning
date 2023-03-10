using System;

namespace indexers_and_operators
{
    class Program
    {
        class Person        //класс людей для списка
        {
            public string name;
            public Person(string name) => this.name = name;
        }
        class ForIndexers
        {
            //индексаторы позволяют объектам идексироваться, как объект. по синтаксису напоминает массив:
            /*возвращаемый_тип this[Тип параметр1, ...]
            {
                get { ... }
                set { ... }
            }*/
            private Person[] people;        //создаём массив объектов класса Person
            public ForIndexers(Person[] people) => this.people = people;    //конструктор для задавания списка

            public Person this[int index]   //допустим, в качестве параметра будем принимать целое число
            {
                get { return people[index]; }   //обращаемся к списку people, возвращаем значение под индексом index
                set { people[index] = value; }  //задаём значение value элементу массива people под индексом index
            }

            //можно перегружать индексаторы, принимать несколько параметров и задавать логику в акссесорах
            public string[] this[int index1, int index2]  //эта перегрузка будет для 2-х целочисельных параметров, возвращает список строк
            {                                           //зададим логику, что он будет возвращать подмассив с именами Person с идексами от 1 параметра до 2 включительно
                get         //как и в свойствах, можно ограничивать акссесоры или опускать один из них. Опустим сеттер
                {
                    string[] peopleNames = new string[index2-index1+1];
                    for (int i = 0; index1 <= index2; index1++) { peopleNames[i++] = people[index1].name; }
                    return peopleNames;
                }
            }

            //тип параметра может быть любым
            string phone;
            string email;
            public string this[string arg]
            {                               //создадим перегрузку индексатора, которая принимает строку в качетсве параметра. если аргумент равен "phone" или "email",
                get                         //то одноимённым полям присваивается значение или они возвращают своё значение, иначе пишется, что индекс недоступен
                {
                    switch (arg)
                    {
                        case "phone": return phone;
                        case "email": return email;
                        default: return "индекс недоступен";
                    }
                }
                set
                {
                    switch (arg)
                    {
                        case "phone": phone = value; break;
                        case "email": email = value; break;
                        default: Console.WriteLine("индекс недоступен"); break;
                    }
                }
            }

        }

        class ForOperatorsRect  //представим, что объекты этого класса - прямоугольники
        {
            public int height; //1 сторона
            public int width;  //2 сторона

            //Наряду с методами в классах и структурах мы можем также определять операторы. Их определение имеет такой синтаксис:
            /*public static возвращаемый_тип operator оператор(параметры) { ...}*/

            //допустим, если мы хотим складывать объекты, мы будем складывать их 1 стороны вместе и 2 стороны вместе и возвращать новый объект с этими сторонами
            public static ForOperatorsRect operator +(ForOperatorsRect rect1, ForOperatorsRect rect2)
            {
                ForOperatorsRect res = new ForOperatorsRect() { height = rect1.height + rect2.height, width = rect1.width + rect2.width };
                return res;
                //тут мы создаём новый объект ForOperatorsRect, задаём его свойстам сумму свойств обхъектов, которые указанные в аргументах. И возвращаем этот объект
            }
            //можно перегружать операторы, например, сделаем так, чтобы вторым параметром у оператора + было число, которое добавляется к обоим сторонами
            public static ForOperatorsRect operator +(ForOperatorsRect rect1, int val)
            {
                ForOperatorsRect res = new ForOperatorsRect() { height = rect1.height + val, width = rect1.width + val };
                return res;
            }

            //также можно определить операторы сравнивания. Их нужно указывать парами, нельзя указать только одно. Пары: == !=; < >; <=  >=
            //сравнения прямоугольников будем делать через площадь
            public static bool operator <(ForOperatorsRect rect1, ForOperatorsRect rect2)   //операторы сравниванияя возвращает true или false - bool
            {
                return rect1.width * rect1.height <= rect2.width * rect2.height;
            }
            public static bool operator >(ForOperatorsRect rect1, ForOperatorsRect rect2)   //пара для меньше
            {
                return rect1.width * rect1.height >= rect2.width * rect2.height;
            }

            //можно определять операторы инкремента и декремента:
            public static ForOperatorsRect operator ++ (ForOperatorsRect rect)              //этот оператор унарный, то есть принимает только 1 параметр
            {                                                                               //в данном случае инкремент будет увеличивать обе стороны на 1 
                ForOperatorsRect res = new ForOperatorsRect()                              //и возвращать новый объект
                {
                    height = rect.height + 1,
                    width = rect.width + 1
                };
                return res;
            }                                                                               //при определении инкремента, необязательно определять декремент

            //можно определить true и false. оно применяется, когда мы используем объект в качестве условия.
            public static bool operator true(ForOperatorsRect rect)
            {
                return rect.width > 0 && rect.height>0;             //когда мы будем передавать объект в условие, оно вернёт true, если обе стороны больше 0 
            }
            public static bool operator false(ForOperatorsRect rect)
            {
                return rect.width < 0 || rect.height < 0;           //когда мы будем передавать объект в условие, оно вернёт false, если хотя бы одна сторон меньше 0 
            }
            public static bool operator !(ForOperatorsRect rect)    //если в условии использовать if (!counter), то нам также необходимо определить для типа операцию !
            {
                return rect.width < 0 || rect.height < 0;           //запись для ! синонимична определению false
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("\t\tИндексаторы");
            ForIndexers forIndexers = new ForIndexers(new Person[]{                                 //создаём объект forIndexers класса ForIndexers и
                new Person("Tom"), new Person("Bob"), new Person("Sam"), new Person("Alice")        //передаём в конструктор список объектов класса Person
            });

            Console.WriteLine(forIndexers[0].name); //через идексатор forIndexers получаем 0-ой элемент приватного списка people - объект Person с полем name = "Tom"
            forIndexers[1].name = "Mike";           //через идексатор forIndexers изменяем name 1-го объекта Person в people  с "Bob" на "Mike"
            Console.WriteLine(forIndexers[1].name); //Mike

            printArr(forIndexers[1, 2]);    //printArr самосозданный метод для вывода массива. Тут мы написали перегрузку индексатора, которая принимает 2 целых числа
                                            //в метод, с помощью идексатора, мы вывели массив имён объектов Person из массива people с 2 по 3 индекс: "Bob" и "Mike"
            printArr(forIndexers[0, 2]);    //Tom, Bob, Mike

            forIndexers["email"] = "some email";
            forIndexers["phone"] = "some phone";
            forIndexers["any"] = "invalid index";           //такой индекс не предусмотрен, вызывается default в switch
            Console.WriteLine(forIndexers["email"]);        //some email
            Console.WriteLine(forIndexers["phone"]);        //some phone
            Console.WriteLine(forIndexers["invalid"]+"\n"); //такой индекс не предусмотрен, вызывается default в switch



            Console.WriteLine("\t\tПерегрузка операторов");

            ForOperatorsRect rect1 = new ForOperatorsRect() { height = 3, width = 5, };
            ForOperatorsRect rect2 = new ForOperatorsRect() { height = 4, width = 6, };
            ForOperatorsRect rectSum = rect1 + rect2;               //создаём новый прямоугольник, ширина и высота  - суммы ширины и высоты 1 и 2 прямоугольникиков
            bool isRect1Bigger = rect1 > rect2;                     //сравниваем площади прямоугольников. 15>24==False
            bool isRect1Smaller = rect1 < rect2;                    //сравниваем площади прямоугольников. 15<24==True
            Console.WriteLine($"высота полученного прямоугольника: {rectSum.height}, ширина: {rectSum.width}");     //7 и 11
            Console.WriteLine($"1 прямоугольник больше 2?: {isRect1Bigger}  Меньше?: {isRect1Smaller}");            //false и true

            //определение инкремента или декремента будет работать и на префиксную, и на постфиксную форму. При этом
            ForOperatorsRect rect3 = rect1++;                                   //у rect1 height будет 4, width будет 6; у rect3 height будет 3, width будет 5
            Console.WriteLine($"1: {rect1.height}, {rect1.width}, 3: {rect3.height} , {rect3.width}"); 
            rect3 = ++rect1;                                                    //у rect1 height будет 5, width будет 7; у rect3 height будет 5, width будет 7
            Console.WriteLine($"1: {rect1.height}, {rect1.width}, 3: {rect3.height} , {rect3.width}");

            ForOperatorsRect rect4 = rect2 + 6; //добавляем к полям rect по 6
            Console.WriteLine($"высота полученного прямоугольника: {rect4.height}, ширина: {rect4.width}"); //10 и 12
            rect4 += 8;                         //добавляем сокращённой записью ещё по 8
            Console.WriteLine($"высота полученного прямоугольника: {rect4.height}, ширина: {rect4.width}"); //18 и 20

            ForOperatorsRect rectNegative = new ForOperatorsRect() { height = -4, width = 5 };
            if (rectNegative) { Console.WriteLine("у rectNegative правильные стороны"); } else { Console.WriteLine("у rectNegative неправильные стороны"); }
            //так как у rectNegative одна сторона отрицательная, он возвращает false и выполняется блок else
            if (rect1) { Console.WriteLine("у rect1 правильные стороны"); } else { Console.WriteLine("у rect1 неправильные стороны"); }
            //так как у rect1 обе стороны положительные, он возвращает true
            if (!rect2) { Console.WriteLine("у rect2 неправильные стороны"); } else { Console.WriteLine("у rect2 правильные стороны"); }
            //можно использовать оператор !, который мы определили


            Console.ReadLine();
        }

        static void printArr(string[] arr)
        {
            int count = 1;
            Console.Write("{ ");
            foreach (var i in arr)
            {
                Console.Write(i);
                if (count != arr.Length)
                    Console.Write(", ");
                count++;
            }
            Console.WriteLine(" }");
        }
    }
}
