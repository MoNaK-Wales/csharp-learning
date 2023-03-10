using System;
using System.Collections.Generic;

namespace collections
{
    public class DictSorted
    {
        public static void DictMain()
        {
            string[] keysArr = new string[4];
            int[] valuesArr = new int[4];
            Header("Dictionary<K, V>");
            //𝐃𝐢𝐜𝐭𝐢𝐨𝐧𝐚𝐫𝐲<𝐊, 𝐕> (словарь) — это коллекция, каждый элемент которой это пара ключ-значение. словарь принимает 2 типа, один для ключа, второй для
            //значений. в конструктор можно передать ёмкость или список из пар значений (KeyValuePair<K, V>). элементы можно передавать через инициализатор
            Dictionary<char, bool> dict = new Dictionary<char, bool>();     //пустой словарь у которого ключи типа char, а значения типа bool
            Dictionary<string, int> ages = new Dictionary<string, int>(5)   //словарь с ёмкостью 5, в который элементы переданы через инициализатор
            {
                ["Tom"] = 20,           //в этом инициализаторе добавлены 3 пары <string, int>
                ["Bob"] = 17,
                ["Sam"] = 25
            };
            /*есть ещё один способ передавать элемент в инициализатор, равноценный прошлому:
            Dictionary<string, int> ages = new Dictionary<string, int>(4)
            {
                { "Tom", 20},
                { "Bob", 17},
                { "Sam", 25}
            };*/

            //по сути, словарь является списком из элементов KeyValuePair<TKey, TValue>. каждая такая пара имеет два свойства: key и value
            //при переборе словаря через foreach, каждый раз возвращается KeyValuePair<TKey, TValue>, через который уже можно получить ключ и значение
            foreach (var i in ages)
                Console.WriteLine($"Name: {i.Key}  Age: {i.Value}");

            //получение и изменение значений элементов словаря похоже на получение элементов списка, но вместо индексов используются ключи
            Print(ages["Bob"]);   //17
            ages["Bob"] = 23;     //изменение существующего элемента
            Print(ages["Bob"]);   //23
            ages["Tim"] = 30;     //если указанного ключа нет, создаётся новая пара
            Print(ages);          //все пары словаря (есть Tim)

            //свойства Dictionary<K, V>:
            Print(ages.Count);                  //количесво элементов словаря: 4
            ages.Keys.CopyTo(keysArr, 0);       //Keys возвращает коллекцию (поэтому используется CopyTo) всех ключей словаря
            ages.Values.CopyTo(valuesArr, 0);   //Values возвращает коллекцию (поэтому используется CopyTo) всех значений словаря
            Print(keysArr);                     //Tom, Bob, Sam, Tim
            Print(valuesArr);                   //20, 23, 25, 30

            Print("");

            //методы Dictionary<K, V>:
            ages.Add("Matt", 27);                     //добавляет указанную пару в словарь: 5 пар в словаре
            Print(ages);                              //Tom: 20, Bob: 23, Sam:25, Tim: 30, Matt: 27
            Print(ages.ContainsKey("Mike"));          //возвращает true, если словарь содержит указанный ключ: false
            Print(ages.ContainsValue(23));            //возвращает true, если словарь содержит указанное значение: true
            ages.Remove("Sam");                       //удаляет пару по ключу в словаре: пара Sam и 25 удалены
            ages.Remove("Tim", out var res);          //удаляет пару по ключу в словаре и записывает значение элемента в указанную переменную
            Print(res);                               //30
            Print(ages);                              //Tom: 20, Bob: 23, Matt: 27
            Print(ages.TryAdd("Ben", 12));            //пробует добавить пару в словарь. если получилось, вовзращает true: true (и добавлена пара Ben-12)
            Print(ages.TryGetValue("Ben", out res));  //пробует получить значение по ключу. если получилось, вовзращает true и записывает значение в res: true
            Print(res);                               //12
            ages.Clear();                             //очищает словарь
            Print(ages);                              //(пустой словарь)


            Header("IComparer и IComparable");
            /*массивы и некоторые коллекции используют метод Sort.однако этот метод работает только с примитивными типами, такими как строки и числа.
            чтобы сортировка работала со сложными типами(классами) применяются два схожих интерфейса: 𝐈𝐂𝐨𝐦𝐩𝐚𝐫𝐞𝐫 и 𝐈𝐂𝐨𝐦𝐩𝐚𝐫𝐚𝐛𝐥𝐞*/
            /*IComparable это интерфейс который позволяет объектам класса, который реализует его, сортироваться между собой. он имеет метод CompareTo, который
            сравнивает текущий объект с другим. Businessman*/
            /*IComparer это интерфейс, который позволяет объекту (компоратору) такого типа содержать данные о том, как сортировать объекты определённого типа. также
            можно такие объекты-сортировщики создавать для уже существующих типов, которые не поддерживают сортировку по умолчанию. будет на примере Person*/
            /*у обоих интерфейсов подобный принцип. у каждого функция Compare/CompareTo возвращает число, которое имеет одно из трех значений:
             * значение больше нуля — первый/текущий элемент должен быть после сравниваемого элемента
             * значение равно нулю  —  оба элемента равны
             * значение меньше нуля — первый/текущий элемент должен быть раньше сравнивваемого элемента
            */
            //𝐈𝐂𝐨𝐦𝐩𝐚𝐫𝐞𝐫
            List<Person> people = new List<Person>()    //обычный класс, с именем и возрастом
            {
                new Person("Sally", 21),
                new Person("Michael", 17),
                new Person("Bill", 28),
                new Person("Ben", 15)
            };
            Print("Список людей до сортировки:");
            foreach (var person in people) Print($"  Name: {person.Name}  Age: {person.Age}");
            //people.Sort();    эту функцию без параметров нельзя применить к списку объектов Person, так как он не поддерживать сортировку (интерфейс IComparable)
            people.Sort(new PersonComparer());  //аргументом указывается объект класса-компоратора. сортировка по длине имени
            Print("Список людей после сортировки:");
            foreach (var person in people) Print($"  Name: {person.Name}  Age: {person.Age}");

            //𝐈𝐂𝐨𝐦𝐩𝐚𝐫𝐚𝐛𝐥𝐞
            List<Businessman> businessmen = new List<Businessman>()
            {
                new Businessman("Samuel", 27, 1400000),
                new Businessman("John", 35, 1800500),
                new Businessman("Tom", 34, 2500000),
                new Businessman("Sam", 30, 1406600),
            };
            Print("Список бизнесменов до сортировки:");
            foreach (var person in businessmen) Print($"  Name: {person.Name}  Age: {person.Age}  Money: {person.Money}$");
            businessmen.Sort();  //объекты интерфеса IComparable можно сортировать без параметров. сортировка по количеству денег (в порядке убывания)
            Print("Список бизнесменов после сортировки:");
            foreach (var person in businessmen) Print($"  Name: {person.Name}  Age: {person.Age}  Money: {person.Money}$");


            Header("SortedList<K, V> и  SortedDictionary<K, V>");
            /*𝐒𝐨𝐫𝐭𝐞𝐝𝐋𝐢𝐬𝐭<𝐊, 𝐕> — это коллекция пар ключ-значение, которые отсортированы по значению ключа. эта коллекция схожа со словарём, кроме того, что она ещё
             сразу сортирует элементы. SortedList, как и словарь, содержат элементы типа KeyValuePair<K, V>, в каждом из которых есть ключ и значение*/
            /*помимо SortedList<K, V>, есть и 𝐒𝐨𝐫𝐭𝐞𝐝𝐃𝐢𝐜𝐭𝐢𝐨𝐧𝐚𝐫𝐲<𝐊, 𝐕>. они почти идентичны, но SortedList лучше в плане использования памяти, быстрее и имеет такие
             удобные функции, как IndexOfKey, IndexOfValue и RemoveAt, что позволяет косвенно работать с индексами. SortedDictionary лучше же подходит для операций
             вставки и удаления для несортированных данных. в коде будет описан только SortedList*/

            //конструкторы SortedList могут принимать такие аргументы: ёмкость, словарь из которого будут скопированы элементы или функция сравнения (компоратор)
            //создается SortedList, ключи которого - объекты Person, значения - строки, ёмкость - 6, а компоратор - ранее созданная функция сравнения Person объектов
            SortedList<Person, string> sorted = new SortedList<Person, string>(6, new PersonComparer())
            {
                { new Person("Sally", 21), "freelancer" },
                { new Person("Michael", 17), "waiter" },
                { new Person("Bill", 28), "lawyer" },
                { new Person("Ben", 15), "student" }
            };
            //порядок указания элементов не важен, они всё равно буду отсортированны по функции сравнения

            //при итерации по отсортированному списку, как и со словарём, цикл проходится по элементам KeyValuePair, из которых можно получить Key и Value
            foreach (var p in sorted)
                Print($"{p.Key.Name} {p.Key.Age}: {p.Value}");  //элементы идут в порядке, как они были отсортированы компоратором PersonComparer, по длине имени
            SortedListPrint(sorted);        //функция для того, чтобы написать в консоль элементы списка с Person, делает то же самое, что и цикл сверху

            //свойства SortedList<K, V>
            Print("\n"+sorted.Count);                   //количесво элементов отсортированного списка: 4
            foreach(var i in sorted.Keys) Print(i);     //Keys возвращает коллекцию всех ключей отсортированного списка: объекты Person
            foreach (var i in sorted.Values) Print(i);  //Values возвращает коллекцию всех значений отсортированного списка: student, lawyer, freelancer, waiter
            Print(sorted.Comparer+"\n");                //возвращает компоратор IComparer этого SortedList

            //получение и изменение элементов SortedList такое же, как у словаря
            Person lastItem = sorted.Keys[1];          //так как у меня ключи списка это объекты, не привязанные к переменным, я получу второй ключ через Keys
            Print(sorted[lastItem]);                    //получение значения через ключ: lawyer
            sorted[lastItem] = "builder";               //изменение значения через ключ
            Print(sorted[lastItem]);                    //builder
            sorted[new Person("Samson", 34)] = "vet";   //создание новой пары через несуществующий ключ
            SortedListPrint(sorted);                    //Ben 15: student; Bill 28: builder; Sally 21: freelancer; Samson 34: vet; Michael 17: waiter

            string? res2;

            //методы SortedList<K, V>:
            sorted.Add(new Person("Ed", 25), "actor");  //добавляет новую пару в список с учетом сортировки: Ed 25: actor
            SortedListPrint(sorted);                    //Ed 25: actor; Ben 15, student; Bill 28: builder; Sally 21: freelancer; Samson 34: vet; Michael 17: waiter
            sorted.Remove(lastItem);                    //удаляет пару по ключу: удалён Bill 28: builder
            sorted.RemoveAt(2);                         //удаляет пару по индексу: удалён Sally 21: freelancer
            SortedListPrint(sorted);                    //Ed 25: actor; Ben 15, student; Samson 34: vet; Michael 17: waiter
            Print(sorted.IndexOfKey(sorted.Keys[1]));   //возвращает индекс пары с данным ключем: 1
            Print(sorted.IndexOfValue("waiter"));       //возвращает индекс первого вхождения пары с данным значением: 3
            Print(sorted.ContainsKey(lastItem));        //возвращает true, если указанный ключ есть в SortedList: false (пара с этим объектом уже удалена)
            Print(sorted.ContainsValue("vet"));         //возвращает true, если указанное значение есть в SortedList: true
            var item = sorted.Keys[1];                  //второй элемент отсортированного списка
            Print(sorted.TryGetValue(item, out res2));  //пробует получить значение по ключу. если получилось - возвращает true и записывает результат в res2
            Print(res2);                                //student
            sorted.Clear();                             //очищает отсортированный список
            SortedListPrint(sorted);                    //пустой SortedList
        }
        public class Person
        {
            public string? Name { get; }
            public int? Age { get; }
            public Person(string name, int age)
            {
                this.Name = name;
                this.Age = age;
            }
        }
        //чтобы создать компоратор, нужно реализовать интерфейс IComparer с типом, который должен быть отсортирован, и единственной функцией Compare
        public class PersonComparer : IComparer<Person> 
        {
            public int Compare(Person? a, Person? b)
            {
                if(a?.Name != null && b?.Name != null)
                {
                    if (a.Name.Length > b.Name.Length)          //если одно имя длиннее второго, то он стоит после него (значение больше нуля)
                        return 1;
                    else if (a.Name.Length < b.Name.Length)     //если одно имя короче второго, то оно стоит перед ним (значение меньше нуля)
                        return -1;
                    else                                        //равные значения
                        return 0;

                    //эти строки можно заменить на эту
                    //return a.Name.Length - b.Name.Length;
                }
                else
                    throw new Exception("Некорректные данные");
            }
        }
        //чтобы объекты класса можно было сортировать, нужно релизовать IComparable (с этим типом), и его функцию CompareTo
        class Businessman : Person, IComparable<Businessman>
        {
            public int Money { get; }
            public Businessman(string name, int age, int money) : base(name, age)
            {
                Money = money;
            }
            public int CompareTo(Businessman? b)            //CompareTo сравнивает данный объект с другим таким же
            {
                if (b != null)
                    return b.Money - this.Money;            //если у данного объекта денег больше, чем у другого, то вернется отрицательное число - он будет спереди
                else
                    throw new Exception("Некорректные данные");
            }
        }
        static void SortedListPrint(SortedList<Person, string> list)
        {
            foreach (var i in list)
            {
                Print($"  {i.Key.Name} {i.Key.Age}: {i.Value}");
            }
            Print("");
        }
    }
}
