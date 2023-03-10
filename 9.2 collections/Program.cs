global using static collections.Program;
using System;                       //когда я перешёл на c# 10.0 эти библиотеки стали не нужны
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace collections
{
    class Program
    {
        static void Main(string[] args)
        {
            //в C# есть массивы, но они не всегда удобны в плане динамичности. поэтому в языке также есть коллекции
            //коллекии есть разных видов: список, словарь, очередь и т.д. для добавления коллекций используется
            //пространство имен System.Collections.Generic

            //ещё есть необобщенные коллекции: их элементы могут быть разных типов, как в пайтоне, однако они медленнее и существует риск
            //ошибки. они находятся в пространстве имен System.Collections

            List.ListMain();
            StackQueue.StackQueueMain();
            DictSorted.DictMain();
            SetBit.SetBitMain();
            Observable.ObservableMain();
        }

        public static void Print(object? a) => Console.WriteLine(a);
        public static void Print(int[] list)
        {
            Console.Write("{ ");
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list[i]);
                if (i + 1 != list.Length)        //если это не последний i
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" } (массив)");
        }
        public static void Print(string[] list)
        {
            Console.Write("{ ");
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list[i]);
                if (i + 1 != list.Length)        //если это не последний i
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" } (массив)");
        }
        public static void Print(BitArray list)
        {
            foreach(bool i in list)
            {
                Console.Write(i ? 1+" " : 0+" ");
            }
            Console.WriteLine();
        }        
        public static void Print<T>(List<T> list)
        {
            Console.Write("{ ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
                if (i + 1 != list.Count)        //если это не последний i
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(" }");
        }
        public static void Print<T>(LinkedList<T> list)
        {
            Console.Write("{ ");
            foreach (T i in list)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine("}");
        }
        public static void Print<T>(Stack<T> list)
        {
            Console.WriteLine("┌──────┐");
            foreach(T i in list)
            {
                Print($"│{i}│");
            }
            Console.WriteLine("└──────┘");
        }
        public static void Print<T>(Queue<T> list)
        {
            var arr = list.ToArray();
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(arr[i]);
                if(i + 1 != list.Count)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("  в очереди");
        }
        public static void Print<K, V>(Dictionary<K, V> list) where K: notnull where V: notnull
        {
            foreach (var i in list)
                Console.WriteLine(i.Key + ": " + i.Value);
            Console.WriteLine("");
        }
        public static void Print<T>(ISet<T> list)
        {
            T[] copy = new T[list.Count];
            list.CopyTo(copy, 0);
            Console.Write("множество: { ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(copy[i]);
                if (i + 1 != list.Count)        //если это не последний i
                {
                    Console.Write("; ");
                }
            }
            Console.WriteLine(" }");
        }
        public static void Print<T>(ObservableCollection<T> list)
        {
            Console.Write("{ ");
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
                if (i + 1 != list.Count)        //если это не последний i
                {
                    Console.Write("; ");
                }
            }
            Console.WriteLine(" } (observable)");
        }
        public static void Header(string title, bool space = true)
        {
            if (space)
                Print("\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Print("\t\t\t\t"+title);
            Console.ResetColor();
        }
    }
}
