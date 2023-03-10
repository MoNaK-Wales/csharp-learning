using System;

namespace classes
{
    class Program
    {
        static void Main(string[] args)
        {
            //объект - это экземпляр класса, принимающий типа "шаблона" в виде класса

            Person firstPerson = new Person(); //синтаксис для объявления: имя класса имя объекта = new конструктор_класса(параметры_конструктора);
                                        //если в классе конструктор не определён, автоматически создаётся пустой


            Console.WriteLine(firstPerson.publicVar);   //получить поля класса можно через имя_объекта.имя_поля
            Console.WriteLine(firstPerson.internalVar); //вне класса можно получить только public, internal и protect internal
            Console.WriteLine(firstPerson.protIntVar);

            //доступным именам также можно присваивать значение, а доступные методы вызывать
            firstPerson.age = 17;
            firstPerson.Print();
            firstPerson.name = "David";
            firstPerson.age = 18;
            firstPerson.Print();


            ForConstructors firstcar = new ForConstructors("Ferrari");      //выбирается перегрузка конструктора для присаивания марки, год изначально будет 2008
            ForConstructors secondcar = new ForConstructors(2006);          //выбирается перегрузка конструктора для присваивания года, марка изначально будет "BMW"
            ForConstructors myCar = new ForConstructors("Toyota", 2016);    //выбирается перегрузка конструктора для присваивания марки и года
            ForConstructors newcar = new ForConstructors();                 //выбирается перегрузка, которая просто выводит строку о создании новой машины

            myCar.Print();
            firstcar.Print();
            secondcar.Print();
            newcar.Print();

            //также, любым полям и свойствам класса можно присваивать значения через инициализаторы в фигурных скобках
            Person secondPerson = new Person { age = 23, name = "Finn", publicVar = "just var" };
            secondPerson.Print();



            //свойства
            ForProperties ukraine = new ForProperties();
            ukraine.Name = "Украина";
            Console.WriteLine(ukraine.Name);    //свойство Name доступно и для чтения, и для записи, как обычное поле

            //ukraine.Planet = "Mars"; - нельзя, так как свойство Planet досступно только для чтения
            Console.WriteLine(ukraine.Planet);

            Console.WriteLine(ukraine.Population); //у Population ещё нет значения и срабатывает условие в геттере, которое выводи строку 
            ukraine.Population = 39000000;
            Console.WriteLine(ukraine.Population); //значение с помощью сеттера устанавливается на 39000000
            ukraine.Population = -230;             //новое значение слишком маленькое, поэтому остаётся 39000000
            Console.WriteLine(ukraine.Population);
            ukraine.Population = 9893028384;       //новое значение слишком большое, поэтому остаётся 39000000
            Console.WriteLine(ukraine.Population+"\n");


            ForReadonly readOnlyClass = new ForReadonly();
            Console.WriteLine(readOnlyClass.text);              //readonly поле можно получить
            //forReadonly.text = "no"                           //readonly поле нельзя поменять нигде, кроме конструктора
            Console.WriteLine(ForReadonly.name);                //readonly поле может быть статическим
            ForReadonly readOnlyClass2 = new ForReadonly(true); //при указании true, в наш конструктор, readonly поле меняется
            Console.WriteLine(readOnlyClass2.text);             //readonly поле поменялось в конструкторе

            //структуры
            ForStructureBook book = new ForStructureBook(); //конструктор по умолчаланию присваивает числовым переменным 0, а строковым null
            Console.WriteLine("\n"+book.bookInfo()); //null (пустая строка) и 0

            ForStructureBook book2 = new ForStructureBook("Гарри Поттер", 99);  //инициализация полей структуры через конструктор
            Console.WriteLine(book2.bookInfo());
            book2.title = "Гарри Поттер и философский камень";                  //доступные члены структуры можно изменять через экземпляр
            Console.WriteLine(book2.bookInfo());

            //также можно использовать инициализатор. так сначала вызывается конструктор по умолчанию, а потом уже полям присваиваются значения
            ForStructureBook book3 = new ForStructureBook { title = "Незнайка на луне", /*price = 219*/ };  //так можно определять только доступные поля
            //так как сначала вызвался пустой конструктор, а потом значение не присвоилось из-за модификатора доступа private, значение price - 0;
            Console.WriteLine(book3.bookInfo());


            Console.ReadLine();
            /*string postText = Console.ReadLine();
             Post post = new Post();
             post.Text = postText;
             post.ShowPost(); */
        }

        class Person   //класс объявляется с помощью ключевого слова class и названия (с большой буквы)
        {
            public string name = "Mike"; //переменные класса называются "поля"
            public int age;               

            //у компонентов класса есть 6 модификаторов доступа: private, public, private protected, protected, internal и protected internal
            //в основном используют private и public. если ничего не написать, модификатор будет private.
                                                                                                             //сборка - это компилируемый код
            //private string privateVar = "private";                //доступно только в этом классе
            protected string protectedVar = "protected";            //доступно только в этом классе и производных в любых сборках 
            protected private string protPrivVar = "prot. private"; //доступно только в этом классе и производных этой сборки
            internal string internalVar = "internal";               //доступно в любом месте внутри этого проекта
            protected internal string protIntVar = "prot. inter.";  //доступно в любом месте внутри этого проекта и производных в других сборках
            public string publicVar = "public";                     //доступно в любом месте программы, а также для других программ и сборок
        

            //в классе можно объявлять обычные методы, для них также используются модификаторы доступа
            public void Print()
            {
                Console.WriteLine($"Hello, my name is {name} and i'm {age}");
            }

            
        }
        class ForConstructors
        {
            public int year = 2008;
            public string brand = "BMW";

                            //конструктор - это метод, который вызывается единожды при создании нового объекта класса
            public ForConstructors()    //конструктор не имеет возвращаемого типа, модификатор public и называется также как и класс
            {
                Console.WriteLine("\nНовая машина!\n");
            }
            //конструкторы также можно перегружать и использовать их для присваивания полей при создании объекта
            public ForConstructors(string brand) => this.brand = brand;     //ключевое слово this представляет ссылку на текущий объект класса.
            public ForConstructors(int year) => this.year = year;           //его обычно используют, чтобы разграничить параметры и поле класса
                                                                //this.name означает, что name - это поле текущего класса, а не название параметра name
            public ForConstructors(string brand, int year) { this.brand = brand; this.year = year; } 

            public void Print()                     
            {
                Console.WriteLine($"My car is a {year} {brand}");
            }
        }
        class ForProperties
        {
            private string name;
            private string planet = "Earth";
            private long population = 0;

            //кроме полей, существуют свойства. они нужны для более простого доступа к приватным полям и установлению логистики
            public string Name        //для объявления к приватному полю свойства: модификатор доступа, и название поля с большой буквы (стиль)
            {
                get { return name; }  //далее объявляются акссесоры или методы доступа (геттеры и сеттеры) 
                set { name = value; } //get нужен для получения значения, set нужен для присвоения значений. value это передаваемое значение
            }                         //это свойство почти ничем не отличается от поля, его также можно изменить и получить

            //такую простую запись можно заменить автоматическим свойством:
            public string AutoName { get; set; }     //так автоматически создаётся поле autoName, через свойство можно его получить и присваивать
            public string Planet
            {
                get { return planet; }     //можно указывать только один акссесор, в этом случае значение можно только получить или только изменить
            }                              //можно также указать только сеттер, тогда значение можно установить, но получить нельзя
            public string Continent { get; private set; } = "Europe";   //акссесорам можно придавать модификаторы доступа. тут изменить это свойство
                                                           //можно только из этого класса. также автосвойствам можно присвоить значения по умолчанию
            //например, его значение можно изменить через метод
            public void changeContinent() => Continent = "Asia";


            //Самое главное применение свойства - это добавление логики к нему. например, можно сделать так, чтобы значение свойства изменялось
            //только тогда, если оно положительное и не больше всего населения Земли, и возвращать значение только при его наличии (если не равно 0)
            public long Population
            {
                get
                {
                    if (population != 0)
                        return population;
                    else
                    {
                        Console.Write("население не указано ");
                        return -1;
                    }
                }
                set
                {
                    if (value > 0 && value < 8000000000)
                        population = value;
                }
            }
        }
        class ForReadonly
        {
            /*ключевое слово readonly делает поля доступными только для чтения. таким полям можно присвоить значений либо при их объявлении, 
            либо в конструкторе. ключевое слово readonly ставится после модификатора доступа, перед типом переменной*/
            public readonly string text = "readonly поле, обозначенное в объявлении";
            //public readonly int i;                        //в отличии от констант, readonly можно объявить по ходу программы , а не только при объявлении
            public readonly static string name = "Maksim";  //также, readonly поле может иметь ключевое слово static, когда константы всегда статические

            /*public const int x;                                    эти строки выведут ошибку
            public static  const string country = "Ukraine";*/
            public ForReadonly(bool i=false)
            {
                if (i) this.text = "readonly поле, обозначенное в конструкторе"; //в конструкторе, мы можем поменять поле с readonly, константы нельзя 
            }                                                                    //тут, если указано true в качестве аргумента

        }

        struct ForStructureBook
        {   
            //структура это тип значений, похожий на класс. он обычно используется для инкапсуляции (объединения) небольших групп перемнных
            //в отличии от класса, переменная екземпляра структуры содержит сам объект, а не ссылку на него. поэтому он и используется для небольших типов

            //1 отличие - нельзя инициализировать поля внутри структуры
            //public int y = 9;     этот код выдаст ошибку, так как тут поле пытаются иницализировать в самой структуре, а так нельзя
            public string title;
            private double price;
            //однако статические элементы (соответственно и константы) можно определить в структуре
            public static int y = 10;

            //2 отличие - нельзя создать конструктор по умолчанию (без параметров), так как он всегда генерируется автоматически для инициализации всех переменных
            //конструктор по умолчанию задаёт всем полям значения 0/false/null (в зависимости от их типа), так как все поля структуры должны быть инициализированы
            public ForStructureBook(string title, double price)
            {
                //3 отличие - конструктор структуры должен инициализировать все поля
                this.title = title;
                this.price = price;
            }
            public string bookInfo()
            {
                return $"Название: {title}\nЦена: {price} гривен";
            }
            

            //4 отличие - структура не может наследовать другие классы/структуры и не может быть наследована другими классами/структурами
            //(=> поля не могут быть виртуальными), но структура может реализовывывать интерфейсы
        }


        /*class Post
        {
            public string Text { get; set; }
            public Post()
            {
                Console.WriteLine("New post");
            }

            public void ShowPost()
            {
                Console.WriteLine(Text);
            }
        }*/
    }
}
