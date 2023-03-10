using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{
    public class Delegates
    {
        delegate void FirstDelegate();
        delegate int SecondDelegate(int a, int b);
        delegate void ThirdDelegate(ref double a, double b);
        delegate T GenDelegate<T>(T t);
        enum VoidMethods { Hello, HowAreYou, Answer }
        delegate void NotifyHandler(string text);
        delegate bool Equality(int a);
        class Account
        {
            protected int money;
            NotifyHandler notify;
            public Account(int money, NotifyHandler func)
            {
                this.money = money;
                notify = func;
            }
            public void Take(int money)
            {
                if (this.money < money)
                    notify("Недостаточно средств на балансе. Баланс: " + this.money + " гривен");
                else
                {
                    this.money -= money;
                    notify($"Успешно списано {money} гривен. Баланс: {this.money} гривен");
                }
            }
        }
        public static void DelegatesMain()
        {
            /*͇Д͇е͇л͇е͇г͇а͇т — это тип, который содержит ссылку на методы, с помощью которого может их вызывать. делегат определяется там же, где и 
            методы, с помощью ключевого слова delegate, возвращаемого типа, имени делегата и набора параметров*/
            /*далее, чтобы создать переменную делегата, нужно указать имя метода (без скобочек) параметры и возвращаемый тип которого соответсвует делегату. также можно указать метод с помощью конструктора делегата*/
            FirstDelegate welcome = Hello;
            FirstDelegate ask = new FirstDelegate(Question);    //равноценный способ
            //FirstDelegate age = Add;                          //этот метод нельзя указать, так как метод возвращает int, а делегат void

            /*теперь эти переменные можно вызвать как обычные методы. есть способ с помощью функции Invoke, в которую передаются параметры (при
            наличии). способы равноценны, но Invoke можно использовать для избежания значения null*/
            welcome();
            ask.Invoke();

            //после определения переменной делегата, метод переменной можно менять с помощью присваивания
            SecondDelegate op;

            op = Add;
            Console.WriteLine("\n3+5=" + op(3, 5));

            op = Multiply;
            Console.WriteLine(op(3, 5) + "=3*5\n");

            //метод должен соответствовать делегату, в том числе и модификаторы in, ref и out и порядок параметров
            ThirdDelegate power = Pow;
            //ThirdDelegate power = NoRef;      //первый параметр NoRef не имеет модификатора ref, в отличии от делегата
            double num = 5;
            power(ref num, 3);
            Console.WriteLine(num + "\n");        //125

            /*делегат может указывать на несколько методов (MulticastDelegate) с одинаковой сигнатурой (возвращаемый тип+набор параметров).
             при вызове такого делегата все методы последовательно вызываются. для добавления используется +=*/
            FirstDelegate? greeting = Hello;        //делегат может иметь значение null
            greeting += Answer;
            greeting += Question;
            greeting += Question;   //можно добавлять копии, они будут так же добавлены
            greeting += Hello;
            greeting();             //hello, i've been better, how are you, how are you, hello
            Console.WriteLine();
            //точно так же можно использовать -= для удаления последнего вхождения метода из списка вызовов (если есть)
            greeting -= Hello;                     //удаляется только последний Hello, а первый остаётся
            greeting?.Invoke();                    //?, так как допускается null: hello, i've been better, how are you, how are you
            Console.WriteLine();
            /*и к делегатам можно применять + и - для добавления/удаления методов в список методов. при удалении от 1 делегата удаляются только те методы из 2, которые есть в 1*/
            FirstDelegate askGreeting = ask + greeting;
            FirstDelegate? onlyGreeting = greeting - ask - ask; //удаляются два последних how are you
            FirstDelegate? onlyWelcome = welcome - greeting;    //если 2 делегат не является подсписком вызово первого - результат это 1 делегат
            askGreeting.Invoke();       //how are you, hello, i've been better, how are you, how are you
            Console.WriteLine();
            onlyGreeting?.Invoke();     //hello, i've been better
            Console.WriteLine();
            onlyWelcome?.Invoke();      //hello
            Console.WriteLine();

            //если делегат, указывающий на несколько методов, возвращает значение, то срабатывает только последний метод
            SecondDelegate sumOrMultiply = new SecondDelegate(Add) + new SecondDelegate(Multiply);  //последним добавлен Multiply
            Console.WriteLine(sumOrMultiply(4, 5)); //20
            Console.WriteLine();

            //делегат может быть и обобщённым. так, можно уточнять тип и тогда больше методов будут доступны для этого делегата
            GenDelegate<double> squareOp = Square;  //указывя double, функция делегата должна принимать один double аргумент и возвращать double
            GenDelegate<float> doubleOp = Double;   //указывя float, функция делегата должна принимать один float аргумент и возвращать float
            Console.WriteLine(squareOp(6));         //36
            Console.WriteLine(doubleOp(6) + "\n");         //12

            /*одно из важнейших использований делегатов — это использование их, как методов обратного вызова, то есть передача их в качестве
            аргумента в другой метод. таким образом в метод пожно передать другой метод*/
            /*static void OpResult(SecondDelegate op, int a, int b) — тут первый параметр принимает делегат типа SecondDelegate, то есть 
            принимающий два числа. этот метод просто выполняет почто то же, что и делегат, но методы могут быть более интересными*/
            SecondDelegate minus = Subtract;
            SecondDelegate plus = Add;
            OpResult(minus, 3, 9);      //-6
            OpResult(plus, 5, 8);       //13
            OpResult(Multiply, 21, 0);  //можно указать просто имя функции, соответствующей делегату: 0
            Console.WriteLine();

            //также делегаты можно возвращать из методов. для этого возвращаемым типом нужно указать тип делегата
            //метод GetReplica возвращает метод типа FirstDelegate, в соотетствии с указанной константой перечисления voidMethods
            FirstDelegate replica = GetReplica(VoidMethods.Hello);
            replica();
            replica = GetReplica(VoidMethods.HowAreYou);
            replica();
            replica = GetReplica(VoidMethods.Answer);
            replica();
            //так как этот метод возвращает метод, можно добавить ещё одну пару скобок и тогда возвращённый метод будет вызван
            GetReplica(VoidMethods.HowAreYou)();

            Console.WriteLine();


            /*пример использования делегата на практике. есть объект банковского счёта, в котором есть метод снятия денег. этот метод должен
             как-то уведомлять о снятии. можно добавить просто Console.WriteLine(...), но вне консольного приложения это не имеет смысла.
             с помощью делегатов можно сделегировать, то есть направить задачу что делать при снятии в другую часть кода*/
            Account myAcc = new Account(1000, Console.WriteLine);   //в конструктор передаётся изначальное к-во денег и метод, который выводит
                                                                    //уведомление. тут это обычный Console.WriteLine
            myAcc.Take(650);        //успешно списалось
            myAcc.Take(500);        //не хватает

            Account myAcc2 = new Account(230, ColorWrite);          //тут передаётся метод, который будет выводить уведомление фиолетовым цветом
            myAcc2.Take(34);
            myAcc2.Take(350);

            Console.WriteLine();


            /*при определении экземплярова делегата, использовалась именованная (обычная) функция. однако, если нет необходимости для создания
            отдельного метода и его действие нигде больше не используется, можно изспользовать анонимные функции*/
            //анонимные функции определяются с помощью оператора delegate, параметрами и телом функции. возвращаемый тип указывать не нужно
            GenDelegate<double> triple = delegate (double a)     //сигнатура метода должна соответствовать делегату
            {
                return a * 3;
            };
            GenDelegate<char> upper = delegate (char a) { return a.ToString().ToUpper().ToCharArray()[0]; };
            Console.WriteLine(triple(23));     //69
            Console.WriteLine(upper('y'));     //Y

            /*если анонимному методу не нужны параметры (даже если они есть в делегате), скобки с параметрами можно опустить. но если делегат
            имеет параметр с модификатором out, то параметры должны быть указаны*/
            ThirdDelegate printPony = delegate
            {
                Console.WriteLine("Pony");
            };
            printPony(ref num, 1);        //но аргументы всё равно нужно указать

            //ещё один способ применения анонимных методов это использование их в качесте аргумента для функции:
            OpResult(delegate (int a, int b) { return a + a + b; }, 3, 4);  //10
            OpResult(delegate (int a, int b)
            {
                return a * 2 + b * 3;
            }, 7, 4);       //26

            Console.WriteLine();


            //удобной записью анонимных методов являются ͇л͇я͇м͇б͇д͇ы. так как они являются анонимными методами, то по типу лямбды являются делегатами
            //лямда-выражения записываются с помощью лямбда-оператора =>. синтаксис лямбд:
            //(набор_параметров) => выражение
            //лямбды могут иметь несколько инструкций, в таком случае используются скобки:
            //(набор_параметров) => { инструкции }
            SecondDelegate squareSum = (x, y) => x * x + y * y;     //если лямбда возвращает значение и состоит из 1 выражения, return опускается
            SecondDelegate squareSubtract = (x, y) =>               //лямбда с несколькими инструкциями
            {
                int xSquare = x * x;
                int ySquare = y * y;
                return xSquare > ySquare ? xSquare - ySquare : ySquare - xSquare;   //если лямбда имеет больше 1 выражения, return нужно писать
            };
            Console.WriteLine(squareSum(5, 4));         //41
            Console.WriteLine(squareSubtract(2, 4));    //12

            /*в с# 10 появилась возможность неявно типизировать лямбды, то есть использовать var. но так может возникнуть проблема с типизацией
             параметров, поэтому стоит указывать их тип*/
            //var not = (logic) => !logic;      //это выводит ошибку "не удалось вывести тип делегата"
            var not = (bool logic) => !logic;   //var определил тип этого делегата как встроеннй Func<bool, bool>
            Console.WriteLine(not.GetType());   //Func<bool, bool>
            Console.WriteLine(not(false));      //false

            //если лямбда имеет один параметр и тип указывать не надо, скобки можно опустить
            GenDelegate<string> trim = text => text.Trim();
            Console.WriteLine(trim("      text   ") + "\n");    //text

            //как и обычные делегаты, лямбды можно добавлять и удалять
            FirstDelegate? helloLambda = () => Console.WriteLine("Hi");
            helloLambda += () => Console.WriteLine("How r u doing?");
            helloLambda += () => Console.WriteLine("What's up?");
            helloLambda();

            Console.WriteLine();

            /*как и делегаты, лямбды можно использовать как аргумент в методе. метод SumByCond суммирует все числа из массива, соответсвующие
             указанному методу проверки делегата Equality*/
            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int res = SumByCond(nums, i => i < 5);      //тут суммирются числа из массива, которые меньше 5
            Console.WriteLine(res);                     //10
            res = SumByCond(nums, i => i % 2 == 1);     //тут суммируются только нечётные числа массива
            Console.WriteLine(res + "\n");                //25

            //лямбды также могут быть возвращены методами. GetLambdaReplica по сути делает то же самое, что и GetReplica, но первый метод возвращает лямбда-выражение
            var lambdaReplica = GetLambdaReplica(VoidMethods.Hello);
            lambdaReplica();
            lambdaReplica = GetLambdaReplica(VoidMethods.HowAreYou);
            lambdaReplica();
            lambdaReplica = GetLambdaReplica(VoidMethods.Answer);
            lambdaReplica();
            //и точно так же можно добавить пару скобок и сразу вызвать метод
            GetLambdaReplica(VoidMethods.HowAreYou)();

            Console.WriteLine();


            //c# имеет встроенные обобщенные делегаты Func, Action и Predicate. Func принимает до 16 параметров и возвращает последний указанный тип. Action не возвращает значение и может принимать до 16 параметров. Predicate принимает 1 параметр и возвращает bool
            Func<int, int, int> func = Add;                                 //этот делегат принимает два int значения и возвращает int
            Action<string> action = ColorWrite;                             //этот делегат принимает string значение и не возвращает значения
            Predicate<char> isLower = character => char.IsLower(character); //этот делегат принимает char и возвращает bool


            /*͇з͇а͇м͇ы͇к͇а͇н͇и͇е — это определение функции, которая запоминает свои переменные и параметры (лексическое окружение) даже тогда, когда 
             выполняется вне своей области видимости. замыкание состоит из 3 компонетнов:
             * внешняя функция, которая является областью видимости замыкания и в которой содержится лексическое окружение
             * лексическое окружение - некоторые переменные, определённые во внешней функции
             * вложенная (замкнутая) функция, которая использует переменные внешней функции*/
            FirstDelegate closure = Outer();        //closure это метод Inner, так как его возвращает метод Outer
            closure();      //теперь для этого метода, переменная x метода Outer равняется 9 и при следующем вызове х останется 9
            closure();      //11
            closure();      //13
            Console.WriteLine();
            //если создать другую переменную этого метода, для него х будет иметь своё значение
            FirstDelegate closure2 = Outer();
            closure2();     //9
            Console.WriteLine();

            //"замкнуть" также можно и лямбду/анонимную функцию
            var closure3 = OuterLambda();
            closure3();     //11
            closure3();     //12
            closure3();     //13
            Console.WriteLine();

            //при создании делегата с замкнутым методом, параметр, передаваемый к внешнему методу также входит в лексическое окружение
            Action<int> closure4 = OuterParams(11);     //теперь, для метода Inner у closure4 есть значение n (11)
            closure4(20);   //31
            closure4(53);   //64
            closure4(49);   //60
            Console.WriteLine();
            //ещё одна переменная локальной функции, только с другим значением для n
            Action<int> closure5 = OuterParams(100);
            closure5(42);   //142
            closure5(2);    //102
            closure5(81);   //181
        }
        static void Hello() => Console.WriteLine("Hello!");
        static void Question() => Console.WriteLine("How are you?");
        static void Answer() => Console.WriteLine("I've been better");
        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;
        static int Subtract(int a, int b) => a - b;
        static void Pow(ref double num, double n) => num = Math.Pow(num, n);
        static void NoRef(double num, double n) { }
        static double Square(double n) => n * n;
        static float Double(float n) => n + n;
        static void OpResult(SecondDelegate op, int a, int b) => Console.WriteLine("result: " + op(a, b));
        static FirstDelegate GetReplica(VoidMethods a)
        {
            switch (a)
            {
                case VoidMethods.Hello: return Hello;
                case VoidMethods.HowAreYou: return Question;
                case VoidMethods.Answer: return Answer;
                default: return Hello;
            }
        }
        static void ColorWrite(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static int SumByCond(int[] arr, Equality func)
        {
            int sum = 0;
            foreach (int i in arr)
            {
                if (func(i))
                    sum += i;
            }
            return sum;
        }
        static FirstDelegate GetLambdaReplica(VoidMethods a)
        {
            switch (a)
            {
                case VoidMethods.Hello: return () => Console.WriteLine("Salute");
                case VoidMethods.HowAreYou: return () => Console.WriteLine("How are you feeling?");
                case VoidMethods.Answer: return () => Console.WriteLine("All is well");
                default: return () => Console.WriteLine("extra return");
            }
        }
        static FirstDelegate Outer()        //внешняя функция, возвращающая метод типа FirstDelegate
        {
            int x = 7;      //лексическое окружение, переменная определённая во внешней функции
            void Inner()    //замкнутая функция, использующая лексическое окружение
            {
                x += 2;
                Console.WriteLine(x);
            }
            return Inner;   //возвращаение замкнутой функции
        }
        static FirstDelegate OuterLambda()
        {
            int x = 10;
            FirstDelegate Inner = () => Console.WriteLine(++x);
            return Inner;
        }
        static Action<int> OuterParams(int n)
        {
            var Inner = (int m) => Console.WriteLine($"Сумма с {n}: {m + n}");
            return Inner;
        }
    }
}
