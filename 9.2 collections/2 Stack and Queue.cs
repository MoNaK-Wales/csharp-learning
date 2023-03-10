using System;
using System.Collections.Generic;

namespace collections
{
    public class StackQueue
    {
        public static void StackQueueMain()
        {
            Header("Stack<T>");
            /*𝐒𝐭𝐚𝐜𝐤<𝐓> — это коллекция, которая работает по такому принципу: кто последний зашёл - тот первый выйдет. то есть новые элементы кладутся в конец,
            и когда их получают, элементы идут с конца.пример с одеждой: одеваемся мы начиная с нижнего белья и футболки до верхней одежды, а раздеваемся
            наоборот, начиная с верхней одежды. так и работает Stack (стопка)*/
            //конструкторы, как у списка (но нельзя использовать инициализатор: пустой конструктор; указать ёмкость; добавить другой массив/список/IEnumerable
            var booksArr = new string[] { "1 book", "2 book", "3 book", "4 book" };
            Stack<string> books = new Stack<string>(booksArr);
            foreach (string book in books)      //на этом примере можно разобрать принцип стека: сначала положили друг на друга 4 книги, начиная с 1, но когда
                Print(book);                    //их нужно взять по очереди, сначала берётся последняя (4), потом следующая (3) и так далее до первой
                                                //фактически, перебор стека происходит просто наоборот

            Print(books);                       //моя функция для стека, декорирует как стопку

            //как и многие объекты IEnumerable, стек имеет свойство count
            Print(books.Count + "\n");          //4

            var list = new string[4];

            //методы Stack<T>:
            Print(books.Peek());                //возвращает последний (верхний) элемент стека: 4 book
            Print(books);                       //4 book, 3 book, 2 book, 1 book
            Print(books.Pop());                 //возвращает последний (верхний) элемент стека и удаляет его: 4 book
            Print(books);                       //3 book, 2 book, 1 book
            books.Push("5 book");               //добавляет в конец (верхним) элемент
            Print(books);                       //5 book, 3 book, 2 book, 1 book
            Print(books.TryPeek(out var res));  //если стек не пуст, записывает в указанную переменную верхний элемент и возвращает true, а если
            Print(res);                         //пуст, то возвращает false: true, 5 book
            Print(books.TryPop(out res));       //если стек не пуст, записывает в указанную переменную верхний элемент, удаляет его и возвращает true, а если
            Print(res);                         //пуст, то возвращает false: true, 5 book
            Print(books);                       //3 book, 2 book, 1 book
            Print(books.Contains("4 book"));    //возвращает true, если указанный объект есть в Stack: false
            books.CopyTo(list, 1);              //копирует элементы в указанный массив начиная с указанного иднекса массива
            Print(list);                        //"", "3 book", "2 book", "1 book" (пустая строка из-за большего количества элементов в массиве)
            list = books.ToArray();             //возвращает массив из элементов стека
            Print(list);                        //3 book, 2 book, 1 book (в виде массива)
            books.Clear();                      //очищает стек
            Print(books);                       //(пустая коробочка)


            Header("Queue<T>");
            /*𝐐𝐮𝐞𝐮𝐞<𝐓> — это коллекция, похожая на стек. работает по такому принципу: кто первый зашёл - тот первый выходит. Queue работает как очередь в
            в реальной жизни. Например, люди в очереди: кто первый пришёл, того первым и примут(в живой очереди)*/
            //конструкторы такие же, как у стека: пустой; можно указать ёмкость; можно указать массив/список/IEnumerable, элементы которого будут добавлены
            Queue<string> patients = new Queue<string>(new string[] { "1 patient", "2 patient", "3 patient", "4 patient" });
            foreach (string i in patients)
                Print(i);                       //итерация по очереди проходит в обычном порядке

            Print(patients);            //моя функция для очереди, указывает что это очередь 

            //Queue<T> тоже имеет свойство Count
            Print(patients.Count);      //4

            var list2 = new string[4];

            //методы Queue<T> похожи на методы стека, но Pop и Push заменены Dequeue и Enqueue соответственно:
            Print(patients.Peek());                 //возвращает первый элемент очереди: 1 patient
            Print(patients);                        //1 patient, 2 patient, 3 patient, 4 patient 
            Print(patients.Dequeue());              //возвращает первый элемент и удаляет его из очереди: 1 patient
            Print(patients);                        //2 patient, 3 patient, 4 patient
            patients.Enqueue("5 patient");          //добавляет указанный элемент в конец очереди
            Print(patients);                        //2 patient, 3 patient, 4 patient, 5 patient
            Print(patients.TryPeek(out res));       //пробует получить первый элемент очереди. если получилось, возвращает true и записывает его в указанную
            Print(res);                             //указанную переменную. если очередь пуста - возвращает false: true, 2 patient
            Print(patients.TryDequeue(out res));    //пробует получить и удалить первый элемент очереди. если получилось, возвращает true и записывает его в 
            Print(res);                             //указанную переменную и удаляет его. если очередь пуста - возвращает false: true, 2 patient
            Print(patients);                        //3 patient, 4 patient, 5 patient
            Print(patients.Contains("patient"));    //возвращает true, если очередь содержит указанный элемент: falsse
            patients.CopyTo(list2, 1);              //копирует элементы очереди в массива, начиная с укзанного индекс массив
            Print(list2);                           //"", 3 patient, 4 patient, 5 patient (массив)
            list2 = patients.ToArray();             //возвращает массив из элементов очереди
            Print(list2);                           //3 patient, 4 patient, 5 patient (массив)
            patients.Clear();                       //очищает очередь
            Print(patients);                        //(пустая очередь)
        }
    }
}
