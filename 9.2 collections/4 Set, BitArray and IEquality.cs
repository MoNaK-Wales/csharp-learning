using System;
using System.Collections;
using System.Collections.Generic;

namespace collections
{
    public class SetBit
    {
        public static void SetBitMain()
        {
            Header("HashSet<T>, IEqualityComparer<T> and SortedSet<T>");
            /*𝐇𝐚𝐬𝐡𝐒𝐞𝐭<𝐓> — это коллекция, которая содержит только уникальные значения. он удобен тем, что позволяет быстро находить, добавлять и удалять элементы.
            HashSet является математическим множеством и имеет такие же функции: объединение, пересечение, вычитание и т.д.*/
            //в конструктор можно передать IEnumerable, ёмкость или функцию определения равенства IEqualityComparer (об этом позже)
            HashSet<int> intSet = new HashSet<int>(8) { 1, 7, 3, 15, 7, 13, 9 };      //создание множества с ёмкостью 8
            //в инициализатор передано 7 чисел, но там есть 2 одинаковых числа. так как это множество, останется только одно число из дубликатов
            foreach(int i in intSet) Print(i);      //1, 7, 3, 15, 13, 9

            Print(intSet);              //перегрузка Print на множества

            //свойства HashSet:
            Print(intSet.Count);            //количество элементов HashSet: 4
            Print(intSet.Comparer+"\n");    //функция определения равенства: компоратор по умолчанию

            //HashSet имеет статический метод CreateSetComparer:
            HashSet<int>.CreateSetComparer();     //возвращает объект IEqualityComparer (HashSetEqualityComparer) который позволяет сравнивать множества между собой

            HashSet<int> intSet2 = new HashSet<int>() { 6, 15, 25 };
            HashSet<int> intSet3 = new HashSet<int>() { 6, 3, 17, 5, 7, 9, 15 };
            HashSet<int> intSet4 = new HashSet<int>() { 7, 1, 19 };
            HashSet<int> intSet5 = new HashSet<int>() { 20, 6, 12, 9, 7};
            HashSet<int> sSet = new HashSet<int>() { 3, 7 };
            Predicate<int> natural = i => i < 0;            //условие для только натуральных чисел
            var res = new int[5];

            //методы HashSet:
            intSet.Add(0);                          //добавляет указанный элемент к множеству (если такой уже есть, то ничего не происходит)
            Print(intSet);                          //1, 7, 3, 15, 13, 9, 0
            intSet.Remove(1);                       //удаляет указанный элемент из множества: 7, 3, 15, 13, 9, 0
            intSet.RemoveWhere(natural);            //удаляет все элементы, соответсвующие указанному делегату (больше нуля)
            Print(intSet);                          //7, 3, 15, 13, 9
            intSet.UnionWith(intSet2);              //операция объединения множеств (A ⋃ B)
            Print(intSet);                          //6, 7, 3, 15, 13, 9, 25 (порядок не определён)
            intSet.IntersectWith(intSet3);          //операция пересечения множеств (A ⋂ B)
            Print(intSet);                          //6, 7, 3, 15, 9
            intSet.ExceptWith(intSet4);             //операция разности множеств (A \ B)
            Print(intSet);                          //6, 3, 15, 9
            intSet.SymmetricExceptWith(intSet5);    //операция симметрической разности множеств (A △ B) (объединение без пересечения: (A ⋃ B) \ (A ⋂ B))
            Print(intSet);                          //12, 20, 3, 15, 7
            Print(intSet.IsSubsetOf(sSet));         //true, если множество является подмножеством указанной коллекции (A ⊆ B): false
            Print(intSet.IsSupersetOf(sSet));       //true, если множество является надмножеством указанной коллекции (A ⊇ B): true
            Print(intSet.IsProperSubsetOf(sSet));   //true, если множество является строгим подмножеством (только собственные) указанной коллекции (A ⊂ B): false
            Print(intSet.IsProperSupersetOf(sSet)); //true, если множество является строгим надмножеством (только собственные) указанной коллекции (A ⊃ B): true
            Print(intSet.Overlaps(intSet2));        //true, если множество и указанная коллекция имеют общие элементы (A ⋂ B != ∅): true
            Print(intSet.SetEquals(intSet2));       //true, если множество и указанная коллекция имеют одни и те же элементы (A = B): false
            Print(intSet.Contains(1));              //true, если множество содержит указанный элемент: false
            intSet.CopyTo(res);                     //копирует все элементы множества в указанный массив
            Print(res);                             //12, 20, 3, 15, 7 (массив)
            intSet.Clear();                         //очищает множество
            Print(intSet);                          //(пустое множество)

            Print("");


            /*𝐈𝐄𝐪𝐮𝐚𝐥𝐢𝐭𝐲𝐂𝐨𝐦𝐩𝐚𝐫𝐞𝐫 это итерфейс, который позволяет объекту этого типа содержать данные о том, равны ли элементы указанного типа. объекты этого
            интерфейса используются в множествах, которые не могут содержать одинаковые элементы; словарях, которые не могут содержать одинаковые ключи, и т.д.*/
            HashSet<int> set = new HashSet<int>(new TenthEquallity()) { 1, 4, 12, 11, 15, 25, 21, 20, 29, 122, 121, 126 };
            /*тут, в качестве функции определения равенства, используется объект класса tenthequality, который считает одинаковыми числа, десятки которых равны,
            поэтому числа, чьи десятки равны, удаляются (кроме первого вхождения)*/
            Print(set);     //1, 12, 25, 122

            Print("");


            //𝐒𝐨𝐫𝐭𝐞𝐝𝐒𝐞𝐭<𝐓> - это коллекция, почти такая же, как и HashSet<T>, но отсортированный
            //в конструктор можно передать IEnumerable и компоратор для сортировки
            SortedSet<char> chars = new SortedSet<char>() { 'O', 'C', 'Q', 'C', 'B', 'O', 'A' };
            //SortedSet сортирует все элементы и, так как это список, удаляет дубликаты
            Print(chars);   //A, B, C, O, Q

            Print("");

            //свойства SortedSet<T>:
            Print(chars.Count);     //возвращает количество элементов SortedList: 5
            Print(chars.Comparer);  //возвращает функцию сравнения: функция по уравнению
            Print(chars.Min);       //возвращает минимальное значение в отсортированном множестве: A
            Print(chars.Max);       //возвращает максимальное значение в отсортированном множестве: Q

            //SortedSet<T> имеет статический метод CreateSetComparer с двумя перегрузками:
            //*возвращает объект IEqualityComparer (SortedSetEqualityComparer) который позволяет сравнивать множества между собой
            SortedSet<char>.CreateSetComparer();
            //*возвращает объект IEqualityComparer (SortedSetEqualityComparer) который позволяет сравнивать множества между собой, с учетом указанного компоратора
            SortedSet<int>.CreateSetComparer(new TenthEquallity());

            //SortedSet<T> имеет такие же методы, как и HashSet<T>, но имеет пару уникальных методов:
            Print(chars.GetViewBetween('B', 'P'));          //возвращает подмножество из элементов, входящих в указанный диапазон: B, C, O
            Print(chars.GetViewBetween('C', 'a'));          //буквы нижнего регистра стоят после заглавных, поэтому если результат: C, O, Q
            var rChars = new List<char>(chars.Reverse());   //возвращает IEnumerable из SortedList в обратном порядке
            Print(rChars);                                  //Q, O, C, B, A


            Header("BitArray");
            /*𝐁𝐢𝐭𝐀𝐫𝐫𝐚𝐲 — это коллекция, которая содержит биты. каждый элемент имеет значениe true (1) или false (0). он используется для управления
            последовательностью логических значений*/
            /*в конструктор нужно передать другой BitArray/массив байтов/массив 32-битных чисел/массив логических значений/к-во элементов (все будут false)/к-во
            элементов и bool, значение которого задаётся всем элементам*/
            BitArray bits = new BitArray(5);        //создаёт массив из 5 битов, имеющих значение false (0)
            BitArray bits2 = new BitArray(5, true); //создаёт массив из 5 битов, имеющих значение true (1)
            foreach (var i in bits) Print(i);   //False, False, False, False, False
            foreach (var i in bits2) Print(i);  //True, True, True, True, True

            Print(bits);            //перегрузка Print на BitArray, заменяющая true на 1 и false на 0: 0 0 0 0 0
            Print(bits2);           //1 1 1 1 1

            //получение и изменение элементов BitArray:
            bits[1] = true;     //элементы BitArray можно изменять по индексу
            Print(bits[0]);     //элементы BitArray можно получать по индексу: false
            bits2[3] = false;
            Print(bits);        //0 1 0 0 0
            Print(bits2);       //1 1 1 0 1

            //свойства BitArray:
            Print("\n" + bits.Count);   //Count возвращает количество элементов в BitArray: 5
            Print(bits.Length);         //Length задаёт или возвращает количество элементов в BitArray: 5
            bits2.Length = 6;           //если задать количество элементов больше, чем было, новые элементы будут false
            Print(bits2);               //1 1 1 0 1 0
            Print(bits.Length);         //6
            bits2.Length = 5;           //возврат к-ва элементов
            Print(bits.IsReadOnly);     //возвращает true, если массив битов доступен только для чтения: false

            bool[] boolRes = new bool[5];

            //методы BitArray:
            Print(bits.Get(1));         //возвращает элемент по указанному индексу (то же самое, что и bits[1]): 1
            bits.SetAll(false);         //устанавливает указанное значение всем элементам
            Print(bits);                //0 0 0 0 0
            bits.Set(2, true);          //устанавливает указанное значение указанному элементу 
            bits.Set(0, true);          //то же самое, что и bits[0] = true
            Print(bits);                //1 0 1 0 0
            bits.LeftShift(2);          //сдвигает все значения слева направо на указанное количество единиц (новые значения слева будут 0)
            Print(bits);                //0 0 1 0 1
            bits.RightShift(2);         //сдвигает все значения справа налево на указанное количество единиц (новые значения справа будут 0)
            Print(bits);                //1 0 1 0 0
            Print(bits.Clone());        //неглубокое копирование BitArray, возвращает object: объект BitArray
            bits.CopyTo(boolRes, 0);    //копирует элементы BitArray в указанный массив начиная с его указанного индекса
            Print(boolRes);             //True, False, True, False, False

            Print("");
            
            /*логические операции BitArray проводятся над каждым элементов первого массива битов и соответсующим элементов второго, например:
            первый_элемент_1_BitArray AND первый_элемент_2_BitArray. оба BitArray должны иметь одинаковую длину*/
            Print(bits);            //1 0 1 0 0
            Print(bits2);           //1 1 1 0 1
            Print(bits.And(bits2)); //возвращает BitArray с результатом операции И над каждым элементом двух массивов битов и присваивает его: 1 0 1 0 0
            Print(bits.Or(bits2));  //возвращает BitArray с результатом операции ИЛИ над каждым элементом двух массивов битов и присваивает его: 1 1 1 0 1
            Print(bits.Not());      //возвращает BitArray с результатом операции НЕ над каждым элементом массива битов (реверс) и присваивает его: 0 0 0 1 0
            Print(bits.Xor(bits2)); //возвращает BitArray с результатом операции исключающее ИЛИ (0^0=0; 0^1=1; 1^0=1; 1^1=0) и присваивает его: 1 1 1 1 1
        }

        //для создания функции определения равенства нужно реализовать интерфейс IEqualityComparer и его две функции: Equals и GetHashCode
        class TenthEquallity: IEqualityComparer<int>
        {
            //пускай с этим компоратором, если десятки чисел равны, то сами числа тоже равны (20=21=22=23=24=25=26=27=28=29)
            public bool Equals(int a, int b)
            {
                if (a / 10 == b / 10)   
                    return true;        //если элементы равны, возвращается true
                else
                    return false;
            }
            /*GetHashCode (хеширование) — это функция для ускоренного сравнения. она придаёт разным объектам разные значения, а для одинаковых объектов - равные
            при определении равенства двух элементов, сначала проверяют хеш-коды и если они разные, то и сами элементы разные, но если коды одинаковые, то тогда
            уже элементы уже проверяются по основной функции. хеш-код должен быстро вычисляться, так как он для этого и задуман*/
            public int GetHashCode(int i)
            {
                return i / 10;
            }
        }
    }
}
