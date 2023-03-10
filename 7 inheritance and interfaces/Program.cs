using System;

namespace inheritance_and_interfaces
{
    //наследование получает создать класс, основанный на другом классе. класс унаследует все особенности первого, кроме конструктора
    class Base  //класс, от которого наследуют свойства, называется базовым (base)
    {
        protected string job = ""; //модификатор protected позволяет использовать элементы только внутри цепочки класса
        public string Job { private get { return job; } set { job = value; } }
        public void printJob() => Console.WriteLine(job);
        protected int thrice(int a)
        {
            return a * 3;
        }
        public Base() => Console.WriteLine("создался объект от базового класса");


    }
    class Derived : Base    //класс, который наследует свойства, называется производным (derived). Наследуемый класс пишется через знак :
    {
        int age = 24;
        public void printAll(int a)
        {
            Console.WriteLine($"job: {job}, age: {age}");               //производный класс может использовать protected поле у себя
            Console.WriteLine(a + " трижды: " + base.thrice(a)+"\n");   //base позволяет обращаться к базовому классу. тут мы используем метод базового классу
        }
        public Derived() => Console.WriteLine("создался объект производного класса");
    }

    //модификатор sealed запрещает наследование от этого класса. также нельзя наследовать статический класс
    sealed class notForInherit { }
    static class notForInherit2 { }
    /*class error : notForInherit { }       эти строчки выведут ошибку
    class error : notForInherit2 { }*/


    class ForVirtual
    {
        //иногда появляется потребность в переписывании метода для производного класса
        public virtual void print()         //ключевое слово virtual позволяет переопределять этот метод в классах-наследниках (его необязательно переопределять)
        {
            Console.WriteLine("Обычный метод print");
        }

        public readonly string information = "какая-то readonly информация";
        public void info()
        {
            Console.WriteLine("инфо");
        }


        int number = 0;
        public virtual int Number           //свойства также могут быть виртуальными и переписаны в производных классах
        {
            get { return number; }
            set { number = value; }
        }

    }
    class ForOverride1 : ForVirtual
    {
        //для назначения новых задач виртуальному методу/свойству используется ключевое слово override. переопределённый метод должен иметь такие же параметры
        public override void print()
        {
            Console.WriteLine("Переопределённый метод print");
        }
    }
    class ForOverride2 : ForVirtual
    {
        //тут не будем переписывать метод print, тогда он будет таким-же, как в базовом классе
        public override sealed int Number           //переопределённый методы/свойства могут быть заблокированы для последующего переопределения
        {
            get { return base.Number; }             //в переопределении свойства используем base и обращаемся к свойству базового класса
            set { base.Number = Math.Abs(value); }  //в отличии от изначального свойства, переопределённое свойство назначает только модуль числа
        }

        //также можно заменить для производного класса методы, свойства и поля (даже статические, константы и поля для чтения)
        public new readonly string information = "теперь какая-то новая readonly информация";
        public new void info()
        {                                               //ключевым словом new скрывются члены базового класса и заменяются новыми
            Console.WriteLine("теперь новое инфо\n");
        }
    }


    abstract class ForAbstract   //abstract класс не может иметь ключевое слово sealed, так как они пртивоположные
    {
        //абстрактные классы используются для тех случаев, когда нужно создать какую-то общую форму для производных классов, которые уже будут наполняться
        //нельзя создать экземпляр абстрактного класса
        //класс, содержащий хотя бы один абстрактный член, сам должен быть абстрактным. абстрактные члены не могут быть приватными

        //абстрактные методы, в отличии от виртуальных, не должны иметь содержание, однако в производных классах их обязательно нужно переопределить
        public abstract void printText(string text);  //abstract как-бы указывает на то, что элемент неполноценый и незавершённый. 
        public abstract int X { get; set; }         //свойства также могут быть абстрактными. из этой записи в производных классах нужно определить оба акссесора
        public int i = 1;                           //абстрактный класс может содержать и неабстрактные члены класса
    }
    class AbstractInherit : ForAbstract
    {
        int x = 0;
        public override void printText(string text) //все неабстрактные производные классы должны иметь реальизацию абстрактных методов/свойств
        {
            Console.WriteLine("Текст в первом производнном классе от абстрактного: " + text);
        }
        public override int X
        {
            get { return x - 1; }
            set { x = value; }
        }
    }
    class AbstractInherit2 : ForAbstract
    {
        int x = 0;
        public override void printText(string text)
        {
            Console.WriteLine("Текст во втором производнном классе от абстрактного: " + text);
        }
        public override int X
        {
            get { return x * 2; }
            set { x = value; }
        }
    }


    interface IForInterface      //имя интерфейса принято писать с заглавной I в начале
    {
        //интерфейс это по сути набор, который содержит только абстрактные члены
        //интерфейс может содержать такие элементы, как методы, свойства, события и индексатор (ещё константы и статические поля, но с C# 8)
        //int a;        интерфейс не может содержать неабстрактные элементы

        //abstract указывать не нужно - все члены интерфейса и так абстрактные. также нельзя указывать модификатор доступа (до c# 8), он по умолчанию public
        void move();
        void interfacePrint();
    }
    interface IForInterface2
    {
        void draw();
    }
    //интерфейсы так же, как классы, могут наследовать друг друга. при реализации производного интерфейса нужно реализвоать члены как производного, так и базового класса
    interface IForInterface3 : IForInterface2 
    {
        void somethingElse();
    }
    class InterfaceRealisation : IForInterface //в реализации интерфейса класс должен реализовать все методы/свойства интерфейса
    {
        public void move()            //ключевое слово override не нужно при реализации интерфейса. в самой реализации модификаторы доступа должны быть тоже public
        {
            Console.WriteLine("Двигаюсь");
        }
        public void interfacePrint()
        {
            Console.WriteLine("1 реализация интерфейса\n");
        }
    }
    //отличительная возможность интерфейсов - это их множественное наследование. можно наследовать сразу несколько интерфейсов, которые указываются через запятую
    class InterfaceRealisation2 : IForInterface, IForInterface2
    {
        //при наследовании нескольких интерфейсов, необходимо реализовать все члены всех интерфейсов
        public void move()
        {
            Console.WriteLine("Двигаюсь с 2 интерфейсами");
        }
        public void interfacePrint()
        {
            Console.WriteLine("2 реализация интерфейса");
        }
        public void draw()
        {
            Console.WriteLine("функция второго наследуемого интерфейса\n");
        }
    }
    //также можно реализовать сразу базовый класс и один или более интерфейсов:
    class InterfaceRealisation3 : Base, IForInterface, IForInterface2
    {
        public void move() => Console.WriteLine("Двигаюсь с 2 интерфейсами и базовым классом");
        public void interfacePrint() =>  Console.WriteLine("работа: "+job);     //так мы реализуем методы двух интерфейсов и наследуем класс Base, получая доступ к job
        public void draw() => Console.WriteLine("рисовать\n");
    }
    
    /*существует явная реализация интерфейса. это могут быть методы, свойства или события. явная реализация указывается так - интерфейс.элемент. явно реализуемые элементы являются
     членами не класса этого интерфейса, а самого интерфейса, то есть для этого нужен апкаст. это может понадобиться, если два интерфейса имеют одинаковые члены*/
    interface IExplicit
    {
        void draw();   //такой же метод, как у IForIterface2
    }
    class Explicit: IExplicit, IForInterface2       //реализация интерфейсов с одинаковым методом
    {
        //если реализовать метод, который есть у обоих интерфейсов, то он реализация произойдёт для обоих интерфейсов
        public void draw() => Console.WriteLine("общая реализация draw для обоих интерфейсов");
        //явно реализуемые члены не могут иметь модификатор доступа, но объектам типа их интерфейса, всё равно они доступны
        void IExplicit.draw() => Console.WriteLine("явная реализация draw для IExplicit");
        void IForInterface2.draw() => Console.WriteLine("явная реализация draw для IForInterface2");
    }
    class Explicit2 : IExplicit
    {
        //этот класс будет иметь только явную реализацию
        void IExplicit.draw() => Console.WriteLine("draw, доступный только для объекта IExplicit");
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            Derived inherited = new Derived();  //при создании объекта производного класса, вызываются конструкторы обоих классов (сначала производного)
            inherited.Job = "layer";            //можно обращаться к членам базового класса, через объект производного класса
            inherited.printJob();
            inherited.printAll(4);              //также к членам своего класса

            ForOverride1 override1 = new ForOverride1();
            ForOverride2 override2 = new ForOverride2();
            override1.print();
            override2.print();                          //тут не переопределен метод, пооэтому выполняется блок из базового класса
            override1.Number = -130;
            Console.WriteLine(override1.Number);
            override2.Number = -130;
            Console.WriteLine(override2.Number);        //во втором классе, свойство Number переопределено так, что ему задаётся модуль указанного числа
            Console.WriteLine(override1.information);   
            Console.WriteLine(override2.information);   //так как мы скрыли базовое значение поля и заменили его новым, вывод будет другой
            override1.info();
            override2.info();                           //так как мы скрыли базовый методы и заменили его новым, вывод будет другой

            //ForAbstract error = new ForAbstract();                    нельзя создать экземпляр абстрактного класса    
            AbstractInherit abstractInherit = new AbstractInherit();
            AbstractInherit2 abstractInherit2 = new AbstractInherit2();
            abstractInherit.printText("какой-то текст");
            abstractInherit2.printText("какой-то текст");
            abstractInherit.X = 10;
            abstractInherit2.X = 10;
            Console.WriteLine(abstractInherit.X);       //9
            Console.WriteLine(abstractInherit2.X+"\n"); //20

            //IForInterface error = new IForInterface();                нельзя создать экземпляр интерфейса
            InterfaceRealisation realisation = new InterfaceRealisation();
            InterfaceRealisation2 realisation2 = new InterfaceRealisation2();
            realisation.move();
            realisation.interfacePrint();
            realisation2.move();
            realisation2.interfacePrint();
            realisation2.draw();            //2 реализация также имеет метод draw второго интерфейса 
            InterfaceRealisation3 realisation3 = new InterfaceRealisation3();   //объект класса, наследуемого класс Base и реализуемого интерфейсы 1 и 2
            realisation3.Job = "doctor";    //поле наследуемое от класса Base
            realisation3.move();            //реализации методов интерфейсов
            realisation3.interfacePrint();
            realisation3.draw();

            //явная реализация
            Explicit expl = new Explicit();         
            expl.draw();                            //для объекта Explicit просто есть свой метод draw
            IExplicit expl2 = new Explicit();       //апкаст к IExplicit
            expl2.draw();                           //явная реализация draw для IExplicit, доступная только объекту типа IExplicit
            IForInterface2 expl3 = new Explicit();  //апкаст к IForInterface2
            expl3.draw();                           //явная реализация draw для IForInterface2, доступная только объекту типа IForInterface2
            Explicit2 expl4 = new Explicit2();      //этот объект не имеет собственной реализации, только явная
            //expl4.draw();                         //это выводит ошибку, так как тип Explicit2 не имеет метода draw
            ((IExplicit)expl4).draw();              //если сделать апкаст к IExplicit, он будет иметь draw, т.к. он явно реализован в Explicit2
            
            
            Console.ReadLine();
        }
    }
}
