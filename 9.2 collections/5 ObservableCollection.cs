using System;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace collections
{
    public class Observable
    {
        public static void ObservableMain()
        {
            Header("ObservableCollection<T>");
            /*𝐎𝐛𝐬𝐞𝐫𝐯𝐚𝐛𝐥𝐞𝐂𝐨𝐥𝐥𝐞𝐜𝐭𝐢𝐨𝐧<𝐓> — это коллекция, похожа на List, позволяющая отслеживать любые изменения. в отличии от остальных описанных
            коллекций, ObservableCollection<T> находится в пространстве имен System.Collections.ObjectModel*/
            //в конструктор можно передать список или IEnumerable. значения можно передать через инициализатор
            ObservableCollection<double> obs = new ObservableCollection<double>() { 6.1, 4.03, 5, 3.9, 0.4 };

            //обращение к элементам ObservableCollection такое же как у List
            Print(obs[1]);          //получение: 4,03
            obs[1] = 12.32;         //изменение
            Print(obs[1]+"\n");     //12,32

            Print(obs);             //перегрузка Print для ObservableCollection<T>

            double[] res = new double[4]; 

            //методы ObservableCollection<T>:
            obs.Add(8.91);              //добавляет указанный элемент в коллекцию
            obs.Remove(12.32);          //удаляет первое вхождение указанного элемент из коллекции
            obs.RemoveAt(1);            //удаляет элемент из коллекции по указанному индексу
            Print(obs);                 //6.1, 3.9, 0.4, 8.91
            Print(obs.IndexOf(6.1));    //возвращает индекс первого вхождения указанного элемента: 0
            obs.Move(2, 0);             //перемещает элемент первого указанного индекса на второй укзанный индекс
            Print(obs);                 //0.4, 6.1, 3.9, 8.91
            Print(obs.Contains(5));     //возврщает true, если укзанный элемент есть в коллекции: false
            obs.CopyTo(res, 0);         //копирует все элементы коллекции в указанный массив начиная с указанного индекса
            Print(obs);                 //0.4, 6.1, 3.9, 8.91 (массив)

            Print("");

            /*для отслеживания изменений ObservableCollection нужно к его событию CollectionChanged добавить метод, который и получает информацию
            об изменениях:*/
            obs.CollectionChanged += ChangeAction;

            obs.Add(49.029);    //добавление
            obs.Remove(6.1);    //удаление
            obs.RemoveAt(2);    //также удаление
            obs[0] = 32;        //замена
            obs.Move(0, 2);     //перемещение
            obs.Clear();        //очищение
        }
        /*в метод на событие CollectionChanged нужно указывать в параметрах объект (отправитель) и объект типа NotifyCollectionChangedEventArgs,
        который имеет информацию об изменении. он имеет такие свойства: 
        * Action — возвращает действие из перечисления NotifyCollectionChangedAction, которое вызвало событие
        * NewItems — возвращает список новых элементов после совершенного действия
        * OldItems — возвращает список элементов, над которым совершили действия Replace, Remove или Move
        * NewStartingIndex — возвращает индекс изменённого элемента
        * OldStartingIndex — возвращает индекс элемена, в котором произошло действие Replace, Remove или Move
        через действия удобно проходить через switch*/
        static void ChangeAction(object? sender, NotifyCollectionChangedEventArgs args)
        {
            switch (args.Action)
            {
                case NotifyCollectionChangedAction.Add:         //если добавили элемент
                    Print($"ДЕЙСТВИЕ: добавлен элемент {args.NewItems?[0]} по индексу {args.NewStartingIndex}");
                    break;
                case NotifyCollectionChangedAction.Remove:      //если удалили элемент
                    Print($"ДЕЙСТВИЕ: удалён элемент {args.OldItems?[0]} по индексу {args.OldStartingIndex}");
                    break;
                case NotifyCollectionChangedAction.Replace:     //если заменили элемент (коллекция[индекс] = новое_значение)
                    Print($"ДЕЙСТВИЕ: заменён элемент {args.OldItems?[0]} по индексу {args.OldStartingIndex} на новый элемент {args.NewItems}");
                    break;
                case NotifyCollectionChangedAction.Move:        //если изменили индекс элемента
                    Print($"ДЕЙСТВИЕ: элемент {args.OldItems?[0]} перемещён с {args.OldStartingIndex} индекса на {args.NewStartingIndex}");
                    break;
                case NotifyCollectionChangedAction.Reset:       //если отчистить список
                    Print("ДЕЙСТВИЕ: список полностью удалён");
                    break;
            }
        }
    }
}
