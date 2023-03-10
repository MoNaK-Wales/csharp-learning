using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{
    public class Events
    {
        class Account
        {
            public delegate void NotifyHandler(string text);
            //определение события состоит из ключевого слова event, типа делегата который представляет событие и имя события
            /*теперь это событие по сути представляет делегат NotifyHandler, который принимает строку в качестве параметра и его также можно 
             вызвать (и с помощью Invoke)*/
            public event NotifyHandler? Notify;        //пока у события нет функций, он может иметь значение null
            int money;
            public Account(int money) => this.money = money;
            public void Take(int money)
            {
                if (this.money < money)
                    Notify?.Invoke("Недостаточно средств на балансе. Баланс: " + this.money + " гривен");
                else
                {
                    this.money -= money;
                    Notify?.Invoke($"Успешно списано {money} гривен. Баланс: {this.money} гривен");
                }
            }
            public void Put(int money)
            {
                this.money += money;
                Notify?.Invoke($"Поступило на счёт {money} гривен. Баланс: {this.money} гривен");
            }
        }
        class Account2
        {
            /*добавление и удаление обработчиков в событие может быть управляемым с помощью акссесоров add и remove. add срабатывает при
             использовании оператора +=, а remove при -=. так же, как и set, эти акссесоры имеют свойство value*/
            NotifyHandler? notify;
            public event NotifyHandler? Notify
            {
                add
                {
                    notify += value;
                    Console.WriteLine($"Обработчик {value?.Method.Name} был добавлен"); //через value.Method можно получить информацию о методе
                }
                remove
                {
                    notify -= value;
                    Console.WriteLine($"Обработчик {value?.Method.Name} был удалён");
                }
            }
            /*обычно, при возникновении события, нужно передать некоторую информацию о нём. для этого создают отдельный класс, а делегат
             события имеет 2 параметра: объект отправитель (издатель) и объект класса с информацией о событии:*/
            public delegate void NotifyHandler(Account2 sender, Account2EventArgs e);   //тут, Account2EventArgs это класс, содержащий информацию
                                                                                        //о событии
            int money;
            public Account2(int money) => this.money = money;
            public void Take(int money)
            {
                if (this.money < money)
                    /*тут мы передаём в метод this, в качестве sender, то есть текущий объект и новый объект Account2EventArgs с 4 свойствами:
                     сообщение, баланс счёта, количество денег, на которое изменился  баланс и успешно ли снялись/поступили деньги*/
                    notify?.Invoke(this, new Account2EventArgs("Недостаточно средств на балансе", this.money, money, false));
                else
                {
                    this.money -= money;
                    notify?.Invoke(this, new Account2EventArgs($"Успешно списано {money} гривен", this.money, money, true));
                }
            }
            public void Put(int money)
            {
                this.money += money;
                notify?.Invoke(this, new Account2EventArgs($"Поступило на счёт {money} гривен", this.money, money, true));
            }
        }
        //классы с информацией о событии обычно называют так: класс, к которому относится + EventArgs
        //класс содержит информацию о передаваемом сообщении, текущий баланс, число, на которое изменилось количество денег и успешна ли операция
        class Account2EventArgs
        {
            public string Message { get; }  //текст уведомления
            public int Balance { get; }     //количество денег после изменения
            public int Change { get; }      //число, на которое изменилось количество денег
            public bool isSuccess { get; }  //успешна ли операция
            public Account2EventArgs(string mes, int balance, int change, bool success)
            {
                Message = mes;
                Balance = balance;
                Change = change;
                isSuccess = success;
            }
        }
        public static void EventsMain()
        {
            /*͇с͇о͇б͇ы͇т͇и͇я позволяют классу сигнализировать о каком-либо действии. класс, который отправляет событие называется издателем, а тот, 
             который обрабатывает его, называется подписчиком */
            //возьмем ситуацию из примера делегатов с банковским счётом Account, но уведомления буду происходить за счёт событий
            Account myAcc = new(300);
            myAcc.Take(20);    //если сейчас применть функцию, то действия сработают, но уведомления не будет, так как событие не имеет функций

            //функции события называются обработчиками событий. чтобы добавить или удалить обработчик у события используются операторы += или -=
            myAcc.Notify += ColorWrite;
            //также можно добавлять другие делегаты, анонимные функции и лямбды
            myAcc.Notify += text => Console.WriteLine("▒" + text + "▒");
            //теперь при любом действии (во всех действиях вызывается событие) будет дважды выводится текст: цветной и с определённым символом:
            myAcc.Take(40);
            myAcc.Take(280);
            myAcc.Put(70);
            Console.WriteLine();
            //можно удалить функцию из списка вызовов события:
            myAcc.Notify -= ColorWrite;
            myAcc.Take(500);
            myAcc.Take(100);
            //c событиями, в отличии от делегатов, нельзя использовать оператор присваивания =, можно только += и -=

            //другой класс счёта, где рассматриваются акссесоры события и класс информации о событии
            Account2 myAcc2 = new(500);

            //теперь нужно создать обработчик, соответсвующий Account2.NotifyHandler и добавить его к событию
            myAcc2.Notify += NotifyFunc;    //сработал аксессор add
            //теперь все операции над этим счётом будут выводить три строчки о состоянии счёта и транзакции
            myAcc2.Take(274);
            myAcc2.Take(230);
            myAcc2.Put(15);
            myAcc2.Take(230);

            //можно добавить этот же обработчик ещё раз, тогда при каждой операции, строчки буду дважды выводиться
            myAcc2.Notify += NotifyFunc;        //сработал аксессор add
            myAcc2.Put(58);                     //дважды вывелось сообщение
            myAcc2.Notify -= NotifyFunc;        //сработал аксессор remove
            myAcc2.Put(30);                     //сообщение вывелось один раз
        }
        static void ColorWrite(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        static void NotifyFunc(Account2 sender, Account2EventArgs e)
        {
            Console.WriteLine("\nРезультат операции: " + e.Message);
            if (e.isSuccess)
                Console.WriteLine($"Счёт изменился на {e.Change} гривен");
            else
                Console.WriteLine($"Счёт не смог измениться на {e.Change} гривен");
            Console.WriteLine($"Текущий баланс: {e.Balance} гривен\n");
        }
    }
}
