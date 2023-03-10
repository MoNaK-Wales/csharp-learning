using System;

namespace generics
{
    class Program
    {
        //иногда нужно создать метод, который поддерживает разные типы данных (например, функция, которая меняет местами значения
        //для этого существуют ͟о͟б͟о͟б͟щ͟е͟н͟н͟ы͟е͟ ͟т͟и͟п͟ы, указываемые в <>. функция из примера:
        static void Swap<T>(ref T a, ref T b)   //для обобщенного типа обычно используется буква T. теперь в методе это определённый тип,
        {                                       //указываемый потом, при вызове 
            T temp = a;
            a = b;
            b = temp;
        }

        //также можно создавать обобщенные классы. например, класс человека, у которого айди может быть как цифровым, так и в виде строки
        class Person<T>
        {
            string name;
            public T id;
            public static string text;     //статические переменные в обобщенных классах работают немного иначе: для каждого вида
                                           //обобщенного класса создается свой набор статических переменных                
            public Person(string name, T id)
            {
                this.name = name;
                this.id = id;
            }
            public void Hello()
            {
                Console.WriteLine($"Привет, я {name}, мой id — {id}. Тип id: {typeof(T)}");
            }
        }

        //в обобщениях можно использовать несколько обобщенных параметров и использовать другой обобщенный тип
        class Job<T, P>
        {
            public T jobId;
            public P person;
            public Job(T id, P human)
            {
                jobId = id;
                person = human;
            }
        }

        //иногда не все типы данных подходят в обобщенный метод. тогда можно использовать ограничение с помоиощью ключевого слова where
        //where T: Person<int> значит, что теперь в этом методе можно использовать только объекты класса Person<int> и производные от него
        static void SwapID<T>(ref T a, ref T b) where T: Person<int>
        {
            int temp = a.id;
            a.id = b.id;
            b.id = temp;
        }

        //ограничения также работает и для классов
        //если универсальных типов несколько, для каждого указывается (или не указывается) ограничение
        class SomeClass<T, P>
            where T: class
            where P: Person<T>      //тут для определения P, вместо универсального параметра Person поставлен ун. параметр из этого класса
        {
            public T structData;
            public SomeClass(P data)
            {
                structData = data.id;
            }
        }

        /*ограничений может быть несколько (максимум 3) и они должны быть в таком порядке:
         * 1. класс/class/struct (что-то одно)
         * 2. интерфейс (может быть несколько, то есть указываемый тип должен реализовывать их всех, либо быть интерфейсом, наследующим их всех)
         * 3. new() — представвляемый тип должен быть с публичными и пустым конструктором
        */


        //наследовать обобщенные классы можно разными способами
        //можно создать унаследованный класс с таким же универсальным типом:
        class DerivedGeneric<T> : Person<T>
        {
            public DerivedGeneric(string name, T id) : base(name, id) { }   //эта строчка копирует конструктор базового класса
        }

        //можно создать обычный класс, унаследованный от обобщенного с уже определённым типом
        class DerivedGeneric2 : Person<int>     //этот класс наследует только класс Person<int>
        {
            public DerivedGeneric2(string name, int id) : base(name, id) { }
        }

        //можно создать наследника со своими обобщенными параметрами от базового класса с уже определенным типом
        class DerivedGeneric3<T> : Person<int>
            where T: struct
        {
            public T otherData;
            public DerivedGeneric3(string name, int id, T data) : base(name, id)
            {
                otherData = data;
            }
        }

        class DerivedGeneric4<T> : DerivedGeneric3<T>   //если класс наследует другой обобщенный класс с ограничением, такое же ограничение
            where T: struct                             //должно быть определено и в наследнике
        {
            public DerivedGeneric4(string name, int id, T data) : base(name, id, data) { }
        }

        //обобщёнными могут быть и структуры, для них правила такие же
        struct Data<T>
        {
            public T data;
            public Data(T data)
            {
                this.data = data;
            }
        }

        //интерфейс тоже может быть обобщённым
        interface IGeneric<T>
        {
            T Property { get; set; }
        }
        //классы, которые реализуют его могут сразу указать тип, либо тоже иметь такой же обобщённый тип
        class GenericRealization<T> : IGeneric<T>   //класс может иметь и дополнительные обобщённые типы, но T передаётся в интерфейс
        {
            public T Property { get; set; }
            public GenericRealization(T val) => Property = val;
        }
        class CharRealization : IGeneric<char>      //тут класс уже должен реализовать char property
        {
            public char Property { get; set; }
            public CharRealization(char val) => Property = val;
        }

        static void Main(string[] args)
        {
            int firstInt = 6, secondInt = 1;
            Print($"числа до swap: {firstInt}, {secondInt}");
            Swap<int>(ref firstInt, ref secondInt);         //тип указывается также, как при создании функции: в знакак <> после названия
            Print($"числа после swap: {firstInt}, {secondInt}");

            string firstStr = "abc", secondStr = "xyz";
            Print($"строки до swap: {firstStr}, {secondStr}");
            Swap<string>(ref firstStr, ref secondStr);
            Print($"строки после swap: {firstStr}, {secondStr} \n");


            Person<int> person1 = new Person<int>("Tom", 1234);             //у этого экземпляра тип у айди является int
            person1.Hello();
            Person<string> person2 = new Person<string>("Bob", "a23b");     //у этого экземпляра тип у айди является string
            person2.Hello();

            Person<int>.text = "числа";                
            Person<string>.text = "строки";                                 //изменяется статическое поле у одного класса разных типов
            Print(Person<int>.text + ", " + Person<string>.text + "\n");    //однако у Person<int> и Person<string> собственное
                                                                            //статическое поле text

            Job<string, Person<int>> ceo = new Job<string, Person<int>>("acbd", person1);
            //объект ceo имеет поле jobId указанного первым типа string, и поле person, указанного вторым типа Person<int>
            ceo.person.Hello();


            Person<int> person3 = new Person<int>("Bim", 5644);
            Print($"\nid до SwapID: {person1.id}, {person3.id}");
            SwapID<Person<int>>(ref person1, ref person3);
            Print($"id после SwapID: {person1.id}, {person3.id}");


            SomeClass<string, Person<string>> someClass = new SomeClass<string, Person<string>>(person2);  //string ялвяется классом
            Print("\n" + someClass.structData + "\n");

            
            //таким образом этот класс наследует Person<int>
            DerivedGeneric<int> derivedGeneric = new DerivedGeneric<int>("first", 555);
            //таким образом этот класс наследует Person<string>
            DerivedGeneric<string> derivedGenericStr = new DerivedGeneric<string>("first", "555");
            //этот класс наследует только Person<int>
            DerivedGeneric2 derivedGeneric2 = new DerivedGeneric2("second", 222);
            //этот класс наследует только Person<int>, однако имеет ещё и свою универсальную переменную
            DerivedGeneric3<bool> derivedGeneric3 = new DerivedGeneric3<bool>("third", 333, true);

            derivedGeneric.Hello();
            derivedGenericStr.Hello();
            derivedGeneric2.Hello();
            derivedGeneric3.Hello();
            Print(derivedGeneric3.otherData+"\n");

            Data<decimal> personalData = new Data<decimal>(15m);
            Print(personalData.data.GetType()+"\n");        //decimal

            //тут тип string передаётся и в класс GenericRealization<T>, и в интерфейс IGeneric<T>
            GenericRealization<string> genRealization = new GenericRealization<string>("string property");  
            Print(genRealization.Property);             //string property
            Print(genRealization.Property.GetType());   //string
            //тут ещё на уровне класса в IGeneric<T> передан тип char
            CharRealization charRealization = new CharRealization('g');
            Print(charRealization.Property);            //g
            Print(charRealization.Property.GetType());  //char
            

            Console.ReadLine();
        }

        static void Print(object str) => Console.WriteLine(str);
    }
}
