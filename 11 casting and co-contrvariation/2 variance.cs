using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using variance.invariance;
using variance.covariance;
using variance.contravariance;

namespace casting
{
    public class Variance
    {
        class Animal { }
        class Fox : Animal { }

        delegate Animal AnimalReturn();
        delegate T GenAnimalReturn<out T>();
        delegate void AnimalPrint(Fox a);
        delegate void GenAnimalPrint<in T>(T a);
        public static void VarianceMain()
        {
            /*͇к͇о͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь͇ ͇и͇ ͇к͇о͇н͇т͇р͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь в с# позволяют использовать неявное переопределение некоторого типа в другой выше или ниже в 
             иерархии. ковариантность позволяет использовать более конкретный тип, контрвариантность позволяет использовать более общий тип, а
             инвариантность не позволяет использовать какой-либо другой тип. вариантность в с# используется только в интерфейсах и делегатах*/

            /*͇к͇о͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь͇ ͇и͇ ͇к͇о͇н͇т͇р͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь͇ ͇в͇ ͇и͇н͇т͇е͇р͇ф͇е͇й͇с͇а͇х используется в обобщённых интерфейсах. по умолчанию все интерфейсы инварианты, 
             но это можно изменить*/
            /*для примера используется базовый класс животных, производный класс лисы и пространства имён в variance в каждом из которых есть
             обобщённый интерфейс и, реализующий его, класс стаи*/

            //класс обычного обобщённого интерфейса можно присвоить либо такому же классу, либо сделать апкаст до его интерфейса:
            Flock<Fox> inFlock = new Flock<Fox>(new Fox());
            IFlock<Fox> inFlock2 = new Flock<Fox>(new Fox());
            //его свойство можно как получить, так и изменить
            inFlock.Animal = new Fox();
            Console.WriteLine(inFlock.Animal+"\n");
            //но если понадобится более общий тип, например IFlock<Animal>, то так сделать не получиться, так как IFlock<T> инвариантный
            //IFlock<Animal> inFlock3 = inFlock;        //так сделать нельзя

            /*ковариантный интерфейс позволяет возвращать тип, производный от указанного. ковариантный тип указывается с помощью out:
             IFlockCo<out T>, что значит, что тип T является ковариантным и T FlockFunc() значит что этот метод может вернуть как тип T, так и 
             производный от него. однако ковариантный тип можно использовать только для возвращаемого типа метода интерфейса и для свойства
             только для записи. ковариантность это апкаст параметра типа с некоторыми ограничениями*/
            IFlockCo<Animal> coFlock = new FlockCo<Fox>(new Fox());
            /*это значит, что переменной coFlock типа IFlockCo<Animal> присвоен объект более конкретного типа IFlockCo<Fox>, то есть
             в качесте Т используется тип Fox, однако он не может использоваться в сеттере свойства и как параметр метода интерфейса.
             произошел апкаст параметра типа к Animal. это можно использовать, например, в функции которая принимает IFlockCo<Animal> параметр*/
            Console.WriteLine(coFlock.FlockFunc());     //функция, возвращаемая указанный тип Fox
            Console.WriteLine(coFlock.Animal+"\n");     //свойство, указанного типа Fox, которое доступно только для чтения
            //coFlock.Animal = new Fox();               //изменять ковариантное свойство нельзя
            
            /*контрвариантный интерфейс позволяет принимать тип, более общий для указанного (родительский). ковариантный тип указывается с 
             помощью in: IFlockContra<in T>, что значит, что тип Т является контрвариантным и void FlockFunc(T animal) может принимать как 
             указанный тип, так и более общий. однако контрвариантный тип можно использовать как тип параметра метода интерфейса, тип свойства
             только для записи и для ограничения обобщения (where). контрвариантность это даункаст параметра типа с некоторыми ограничениями*/
            //для Fox более общими классами являются Animal и Object, поэтому контрвариантность можно использовать с ними обоими
            object obj = new Fox();                                         //для даункаста нужен предварительный апкаст
            IFlockContra<Fox> contraFlock = new FlockContra<object>(obj);
            /*тут переменной contraFlock типа IFlockContra<Fox> присвоен объект более общего типа IFlockContra<object>, то есть в качестве Т
             используется тип object, но он не может использоваться как возвращаемый тип и как свойство для чтения*/
            /*хоть T и является типом object, из-за типа IFlockContra<Fox> функция FlockFunc принимает только тип Fox (и производные) и свойство 
             тоже типа Fox*/
            contraFlock.FlockFunc(new Fox());           //функция с параметром указанного типа Object(Fox)
            contraFlock.Animal = new Fox();             //свойство только для записи
            //Console.WriteLine(contraFlock.Animal);    //свойство нельзя получить
            //contraFlock.Animal = new object();        //объект класса нельзя указать в свойство
            //contraFlock.FlockFunc(new object());      //объект класса нельзя указать в метод

            //можно указывать и ковариантные типы, и контрвариантные типы в одном интерфейсе: interface IFlockCoContra<in T, out K>


            /*͇к͇о͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь͇ ͇и͇ ͇к͇о͇н͇т͇р͇в͇а͇р͇и͇а͇н͇т͇н͇о͇с͇т͇ь͇ ͇в͇ ͇д͇е͇л͇е͇г͇а͇т͇а͇х используется как в обычных делегатах, так и обобщённых*/

            //ковариантность в делегате заключается в том, что возвращаемый тип делегата может быть более конкретным (производным) от указанного
            AnimalReturn animalReturn = GetFox;     //AnimalReturn предпологает возвращение объекта Animal, но благодаря ковариантности, указан
                                                    //метод, возвращающий более конкретный тип Fox
            Fox foxy = (Fox)animalReturn();         //делегат всё равно возвращает Animal, поэтому чтобы получить нужно использовать явное
                                                    //преобразование (а так как указанный метод возвращает Fox, можно использовать даункастинг)
            /*для ковариантности в обобщённом делегате используется ключевое слово out и этот тип должен использоваться только для возвращаемого
             типа, нельзя использовать для типа параметра: delegate T GenAnimalReturn<out T>()*/
            GenAnimalReturn<Fox> foxReturn = GetFox;        //возвращает Fox
            GenAnimalReturn<Animal> anReturn = foxReturn;   //тут, благодаря ковариантному типу T, можно указать метод, возвращающий производный
                                                            //класс от указанного. возвращает Fox, но можно неявно преобразовать в Animal
            Console.WriteLine(foxReturn.GetType() + " : " + anReturn.GetType());
            Console.WriteLine(foxReturn().GetType() + " : " + anReturn().GetType());

            //контрвариантность в делегате позволяет указать параметр более общего типа от указанного
            AnimalPrint animalPrint = PrintAnimal;  //хоть AnimalPrint должен принимать параметр типа Fox, благодаря контрвариантности можно
                                                    //указать метод с параметром более общего (родительского) типа
            animalPrint(foxy);                      //делегат всё равно должен принимать параметр типа Fox
            /*для контрвариантности в обобщенном делегате используется ключевое слово in. этот тип должен использоваться только как тип 
             параметра, нельзя использовать для возвращаемого типа: delegate void GenAnimalPrint<in T>(T a)*/
            GenAnimalPrint<object> objPrint = PrintObject;  //принимает object 
            GenAnimalPrint<Fox> foxPrint = objPrint;        //даункаст от object к Fox, благодаря контрвариантности, теперь принимает только Fox

            objPrint(12);
            foxPrint(new Fox());

            //также можно совмещать оба способа вариантности: delegate T SomeDelegate<out T, in R>(R val);
        }
        static Fox GetFox() => new Fox();
        static void PrintAnimal(Animal animal) => Console.WriteLine("Зверушка: "+animal.GetType().ToString());
        static void PrintObject(object obj) => Console.WriteLine("Объект: "+obj.GetType().ToString());
    }
}
namespace variance
{
    namespace invariance
    {
        interface IFlock<T>
        {
            T Animal { get; set; }
            T FlockFunc();
        }
        class Flock<T> : IFlock<T> where T : new()
        {
            public T Animal { get; set; }
            public T FlockFunc()
            {
                return new T();
            }
            public Flock(T animal)
            {
                Animal = animal;
            }
        }
    }
    namespace covariance
    {
        interface IFlockCo<out T>
        {
            T Animal { get; }
            T FlockFunc();
        }
        class FlockCo<T> : IFlockCo<T> where T : new()
        {
            public T Animal { get; }
            public T FlockFunc()
            {
                return new T();
            }
            public FlockCo(T animal)
            {
                Animal = animal;
            }
        }
    }
    namespace contravariance
    {
        interface IFlockContra<in T>
        {
            T Animal { set; }
            void FlockFunc(T animal);
        }
        class FlockContra<T> : IFlockContra<T>
        {
            private T animal;
            public T Animal { set { animal = value; } }
            public void FlockFunc(T animal)
            {
                Console.WriteLine(animal?.GetType().ToString());
                Console.WriteLine(this.animal?.GetType().ToString()+"\n");
            }
            public FlockContra(T animal)
            {
                this.animal = animal;
            }
        }
    }
}
