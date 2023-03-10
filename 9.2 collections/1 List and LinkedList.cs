using System;
using System.Collections.Generic;

namespace collections
{
    public class List
    {
        public static void ListMain()
        {
            Header("List<T>", false);

            int[] arr = new int[10];
            Converter<int, string> conv = i => Convert.ToString(i); //выражение (след. урок), которое конвертирует числа в строки

            //𝐋𝐢𝐬𝐭<𝐓> — это простейший список. он похож на массив, но в нём легче добавлять и удалять элементы
            List<string> list1 = new List<string>();        //список это класс (обобщенный) и его нужно создавать через конструктор
            List<int> list2 = new List<int>(3);             /*в конструкторе можно указать число - ёмкость списка. это делается для большей
                                                            производительности. если к-во элементов станет больше, то ёмкость увеличится*/
            List<int> list3 = new List<int>() { 1, 2, 3 };  //элементы списка можно указать сразу в фигурных скобках, как в массиве
            List<int> list4 = new List<int>(list3);         //также можно добавить в конструктор другой список/массив и он будет добавлен
            List<int> list = new List<int>(list3) { 10 };   //можно использовать оба способа одновременно. элементы списка: 1, 2, 3, 10

            //обращение к элементам списка работает также, как и с массивами — через квадратные скобки
            Print(list[1]);     //элемент под первым индексом в списке - 2
            list[1] = 15;       //теперь 1 индекс в списке - 15

            //список, как и массив, можно перебрать через for/foreach, что у меня сделано в обобщенной функции Print
            Print<int>(list);   //{ 1, 15, 3, 10 }
            Print(list.Count + "\n");  //свойство Count возвращает длину списка

            //методы списков:
            list.Add(20);                           //добавляет в конец списка элемент
            Print(list);                            //1, 15, 3, 10, 20
            list.AddRange(new int[] { 5, 93, 5 });  //добавляет в конец списка весь указанный спискок/массив
            Print(list);                            //1, 15, 3, 10, 20, 5, 93, 5
            list.Remove(93);                        //удаляет первое вхождение указанного элемента
            Print(list);                            //1, 15, 3, 10, 20, 5, 5
            list.RemoveAt(0);                       //удаляет элемент под указанным индексом
            Print(list);                            //15, 3, 10, 20, 5, 5
            list.RemoveRange(3, 2);                 //удаляет (2_аргумент) элементов, начиная с (1_аргумент) индекса. Здесь удаляет
            Print(list);                            //2 элемента начиная с 3 индекса: 15, 3, 10, 5
            list.RemoveAll(i => i < 10);            //удаляет все элементы, соответсвующие делегате (следующий урок). Здесь удаляет
            Print(list);                            //все элементы, которые меньше 10: 15, 10
            list.Insert(1, 17);                     //вставляет указанный элемент (2 аргумент) под указанный индекс (1 аргумент)
            Print(list);                            //15, 17, 10
            list.InsertRange(2, list3);             //вставляет указанный список/массив под указанный индекс
            Print(list);                            //15, 17, 1, 2, 3, 10
            Print(list.Contains(20));               //true если список содержит указанный элемент: false (нет 20)
            Print(list.Exists(i => i % 5 == 0));    //true если список содержит элемент, соответсвующий делегату: true (есть 15/10)
            Print(list.IndexOf(10));                //индекс первого вхождения указанного элемента:    5
            Print(list.LastIndexOf(10));            //индекс последнего вхождения указанного элемента: 5
            Print(list.Find(i => i < 10));          //первое вхождение, соответсвующее делегате: 1 (первый элемент в списке меньше 10)
            Print(list.FindLast(i => i < 10));      //последнее вхождение, соответсвующее делегате: 3 (последний элемент в списке меньше 10)
            Print(list.FindAll(i => i < 10));       //список всех элементов, соответсвующих делегате: 1, 2, 3
            Print(list.GetRange(1, 2));             //список (2_аргумент) (тут двух) элементов, начиная с (1_аргумент) индекса: 17, 1
            list.CopyTo(arr);                       //List<T>.CopyTo() копирует все элементы из списка в указанный массив
            Print(arr);                             //15, 17, 1, 2, 3, 10, 0, 0, 0, 0 (в массиве на 4 элемента больше, поэтому ещё 4 нуля)
            list.CopyTo(1, arr, 7, 3);              //копирует 3_аргумент элементов из списка, начиная с 1_аргумент индекса и вставляет
            Print(arr);                             //в 2_аргумент массив, начиная с 4_аргумент индекса: 15, 17, 1, 2, 3, 10, 0, 17, 1, 2
            list.Reverse();                         //меняет порядок элементов в списке наоборот
            Print(list);                            //10, 3, 2, 1, 17, 15
            list.Reverse(1, 3);                     //меняет порядок указ. к-во элементов в списке наоборот, начиная с указанного индекса
            Print(list);                            //10, 1, 2, 3, 17, 15
            list.Sort();                            //сортирует список
            Print(list);                            //1, 2, 3ш 10, 15, 17
            Print(list.BinarySearch(3));            //ищет индекс указанного элемента бинарным поиском. ͟с͟п͟и͟с͟о͟к͟ ͟д͟о͟л͟ж͟е͟н͟ ͟б͟ы͟т͟ь͟ ͟о͟т͟с͟о͟р͟т͟и͟р͟о͟в͟а͟н: 2
            Print(list.ToArray());                  //возвращает новый массив с элементами списка: массив
            Print(list.ConvertAll(conv).GetType()); //конвертирует все элементы в другой тип и добавляет в новый список: List<String>
            list.Clear();                           //очищает список
            Print(list);                            //{}


            Header("LinkedList<T>");
            string[] strings = new string[4]; 
            //𝐋𝐢𝐧𝐤𝐞𝐝𝐋𝐢𝐬𝐭<𝐓> похож на обычный список, но каждый элемент в нём хранит ссылку на предыдущий и следующий элемент в списке
            LinkedList<string> linked1 = new LinkedList<string>();
            LinkedList<int> linked2 = new LinkedList<int>(list3);       //можно передать в конструктор другой список/массив/IEnumerable
            //значения при создании нужно передавать в конструктор c помощью нового списка, а не инициализатора
            LinkedList<string> linked = new LinkedList<string>(new [] {"first", "second", "third"});

            //Каждый элемент LinkedList<T>, в отличии от списка, имеет тип не T, а LinkedListNode<T>. то есть добавляемые элементы T обвертываются
            //в LinkedListNode<T>: Value, Next, Previous
            //Свойства LinkedList<T>:
            Print(linked.Count);       //возвращает количество элементов: 3
            Print(linked.First?.Value);//возвращает первый элемент типа LinkedListNode<T> (поэтому нужен Value). может вернуть null (поэтому "?"): first
            Print(linked.Last?.Value); //возвращает последний элемент типа LinkedListNode<T> (поэтому нужен Value). может вернуть null (поэтому "?"): third

            Print("\n");

            var second = linked.Find("second"); //может вернуть null
            if (second != null)
            {
                Print(second.GetType());        //LinkedListNode
                //свойства LinkedListNode<T>:
                Print(second.Value);            //возвращает значение этого элемента указанного типа T
                Print(second.Previous?.Value);  //Previous возвращает предыдущий элемент типа LinkedListNode<T> 
                Print(second.Next?.Value);      //Next возвращает следующий элемент типа LinkedListNode<T> 
                //Next у последнего элемента, как и Previous у первого элемента возращают null (в консоли это пропуск)
                Print(second.Next?.Next);
                Print(second.Previous?.Previous);

                //методы LinkedList<T>:
                linked.AddAfter(second, "afterSecond");     //добавляет элемент после указанного узла: first, second, afterSecond, third
                linked.AddBefore(second, "preSecond");      //добавляет элемент перед указанным узлом: first, preSecond, second, afterSecond, third
                linked.AddFirst("the first");               //добавляет элемент первым: the first, first, preSecond, second, afterSecond, third
                linked.AddLast("last");                     //добавляет элемент последним: the first, first, preSecond, second, afterSecond, third, last
                Print(linked);                              //моя функция для итерации по LinkedList
                linked.Remove("third");                     //удаляет первое вхождение указанного элемента: удалён third
                linked.RemoveFirst();                       //удаляет первый элемент: first, preSecond, second, afterSecond, last
                linked.RemoveLast();                        //удаляет последний элемент: first, preSecond, second, afterSecond
                Print(linked);                              //first, preSecond, second, afterSecond
                linked.CopyTo(strings, 0);                  //копирует элементы в указанный массив начиная с указанного иднекса массива
                Print(strings);                             //first, preSecond, second, afterSecond в массиве
                Print(linked.Contains("2"));                //возвращает true, если указанный объект есть в LinkedList: false 
                Print(linked.Find("first")?.Value);         //возвращает LinkedListNode первого вхождения указанного элемента: first
                Print(linked.FindLast("first")?.Value);     //возвращает LinkedListNode последнего вхождения указанного элемента: first

                //LinkedList<T> можно перебрать с помощью while и свойства Next
                var element = linked.First;
                while(element!= null)
                {
                    Console.WriteLine("—"+element.Value);
                    element = element.Next; 
                }

                linked.Clear();             //очищает список
                Print(linked);              //{}
            }
        }
    }
}
