using System;

namespace exceptions__try_catch
{
    class Program
    {
        static void Main(string[] args)
        {
            //для обработки возможныъ ошибок (исключений) используется конструкция try catch finally^
            try     //сначала выполняется этот блок
            {
                Console.Write("первый ввод: ");
                int x = 10 / Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(x);
            }
            catch   //в случае какой-то ошибки, выполнение 1 блока прерывается и вызывается блок catch
            {
                Console.WriteLine("какая-то ошибка");
            }
            finally //этот блок вызывается в любом случае, была ошибка или нет. также этот блок будет вызван даже в случае исключения внутри catch
            {
                Console.WriteLine("блок finally");
            }

            //также можно использовать только с catch или только с finally:
            try
            {
                Console.Write("второй ввод: ");
                int length = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Некорректный ввод");
            }

            //в блоке catch можно присвоить ошибку некой переменной;  оператор throw
            try
            {
                //с помощью оператора throw можно вызвать уже существующего типа исключние, или создать новое
                //throw new NotImplementedException();  вызывает исключение, которое обычно происходит при нереализованном методе

                throw new Exception("ошибка третьего ввода"); //вызывает новосоданное исключение класса Exception. класс Exception ялвяется базовым классом для всех
                                                              //обычных исключений. одна из перегрузок конструктора Exception принимает строку, которая сообщает об ошибке
            }
            catch (Exception e) //nеременная e принимает исключение, которое вызвало блок catch
            {
                Console.WriteLine("\n"+e.Message);  //свойство Message у класса Exception возвращает сообщение, описывающее ошибку
                Console.WriteLine(e.Source);        //свойство Source у класса Exception возвращает/задаёт место возникновения ошибки (по умолчанию — имя сборки)
            }

            //можно использовать несколько разных блоков catch, для разных ошибок:
            try
            {
                throw new ArgumentNullException();
                throw new NullReferenceException(); //так как перед этим уже была вызвана ошибка, блок try уже обрывается, и это исключение уже не вызвется
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("деление на 0");
            }
            catch (ArgumentNullException e)     //при указании определённой ошибки, её также можно присвоить временной переменной
            {
                Console.WriteLine("\nпередача пустого аргумента в метод: " + e.Message);
            }
            catch(Exception e)                  //для того, чтобы обработать возможные другие исключения, можно использовать общий Exception
            {
                Console.WriteLine("другая ошибка: " + e.Message);
            }

            Console.ReadLine();
        }
    }
}
