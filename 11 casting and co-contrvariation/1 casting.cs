using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casting
{
    public class Casting
    {
        public static void CastingMain()
        {
            //некоторые типы можно переобразовывать в другие, например int в float:
            int integer = 10;
            float floating = integer;       //тут происходит неявное преобразование int к float, так как все числа int входят в диапазон чисел float
            Console.WriteLine(floating);    //10
            //иногда нужно явное преобразование, так как данные могут быть потеряны
            //byte c = 10 + integer;        //неявное преобразование тут не может быть совершено, так как диапазон byte меньше, чем диапазон значений int
            byte c = (byte)(10 + integer);  //тут происходит явное преобразование, то есть даётся подтерждение, что мы готовы к тому, что возможна потеря данных
            Console.WriteLine(c);           //20
            //если с явным преобразованием дать некорректные данные, ожидаемый результат может быть не таким
            int integer2 = 260;
            byte c2 = (byte)(integer2);     //так как байт может содержать число от 0 до 256, при указании значения вне диапазона, число обрезается
            Console.WriteLine(c2);          //старший бит усёкся, поэтому от 260 осталось 4 (integer2 % 256)
            //для проверки правильности преобразования, используется кюлчевое слово checked, который вызывает исключение, если преобразование некорректное
            try { c2 = checked((byte)integer2); }
            catch(Exception e) { Console.WriteLine(e.Message+"\n"); }    //Arithmetic operation resulted in an overflow.


            //преобразование объектов классов
            //для примера есть базовый класс Person, реализующий интерфейс ICreature, и два производных от него Employee и Client
            //переменная типа базового класса может хранить ссылку на объект производного класса:
            Person person = new Employee("Tom", "Google");
            /*тут происходит неявное преобразование типа Employee в Person, по анологии в реальной жизни, что каждый работник является человеком. это
             преобразование называется восходящим или upcasting, так как в иерархии тип Employee снизу переходит в тип Person сверху*/
            /*так, переменная person хранит ссылку на объект Employee, который имеет своё свойство Company, однако, так как эта переменная типа Person, ей 
             доступны только члены класса Person, хоть значение Company всё ещё остаётся в памяти*/
            Console.WriteLine(person.Name);     //Tom
            //Console.WriteLine(person.Company);  //сам тип Person не имеет определения Company, поэтому так писать нельзя

            //так же и с апкастингом для Client, свойство Name, наследуемое от Person, получить можно, но своё свойство Bank нельзя
            Person client = new Client("Tim", "NatBank");
            Console.WriteLine(client.Name);     //Tim
            //Console.WriteLine(client.Bank);   //так писать нельзя

            //апкастинг можно производить и с типом object, так как все классы по умолчанию наследуют его. однако будут доступны только члены класса object
            object empObject = new Employee("Bob", "Ubisoft");
            Console.WriteLine(empObject.ToString());
            //Console.WriteLine(empObject.Name);          //object не имеет определения для Name

            //также можно преобразовывать в тип интерфейса. тогда будут доступны только те члены, которые реализуют этот интерфейс
            ICreature interfacePerson = new Person("Sam");
            interfacePerson.Print();                        //Person.Print реализует ICreature.Print, поэтому он доступен через этот тип
            //Console.WriteLine(interfacePerson.Name)       //Name это свойство из Person, а не ICreature


            /*есть и обратное действие: нисходящее преобразование или downcasting. то есть преобразование от базового класса к производному.
            главное: ͟н͟и͟с͟х͟о͟д͟я͟щ͟е͟е͟ ͟п͟р͟е͟о͟б͟р͟а͟з͟о͟в͟а͟н͟и͟е͟ ͟н͟е͟в͟о͟з͟м͟о͟ж͟н͟о͟ ͟б͟е͟з͟ ͟п͟р͟е͟д͟в͟а͟р͟и͟т͟е͟л͟ь͟н͟о͟г͟о͟ ͟в͟о͟с͟х͟о͟д͟я͟щ͟е͟г͟о. то есть базовый тип не может стать дочерним, если не содержить ссылку
            на дочерний*/
            /*например, person типа Person содержит ссылку на объект Employee. хоть через него нельзя получить Company, он всё равно остаётся в памяти. 
            получить его можно, преобразовав person в производный тип Employee, то есть произвести даункастинг*/
            //преобразование должно быть явным, так как не все объекты базового класса являются объектами производного (не все люди являются работниками) 
            Console.WriteLine(((Employee)person).Company);  //преобразованием даётся подтверждение, что person может являться Employee, иначе будет исключение
                                                            //тут, с помощью ((Employee)person) получается объект Employee, который уже имеет значение Company
                                                            //Employee emp = person;       //явным преобразованием невозможно сделать нисходящее преобразование

            //однако это не безопасно, так как исключение при преобразовании вызывается уже при компиляции
            try { Client client1 = (Client)person; }        //среда разроботки не говорит об ошибке, но Person person хранит ссылку на объект Employee, 
            catch(Exception e)                              //поэтому попытка преобразовать person в тип Client вызовет исключение при компиляции
            { 
                Console.WriteLine($"{e.GetType()}: {e.Message}\n");  //InvalidCastException
            }


            //для обезопашивания от InvalidCastException при преобразовании, существует 3 основных способа: try-catch (использованный сверху), as и is
            //ключевое слово as производит преобразование, но если оно не удалось, вместо исключения она просто присваивает значение null
            Employee? employee1 = empObject as Employee;            //после as указывается тип для преобразования. так как as может вернуть null, используется ?
            Employee? employee2 = interfacePerson as Employee;      //interfacePerson содержит ссылку только на Person, но не на Employee
            if (employee1 == null) Console.WriteLine("Преобразование не совершилось");      //в этом случае преобразование получилось
            else Console.WriteLine(employee1.Company);

            if (employee2 == null) Console.WriteLine("Преобразование не совершилось");      //тут преобразование не получилось, поэтому employee2 является null
            else Console.WriteLine(employee2.Company);

            //ключевое слово is похоже на оператор ==, но для типов, то есть возвращает true если левый операнд может быть типом правого
            if (person is Client)
            {
                Client clientCast = (Client)person;
                Console.WriteLine(clientCast.Bank);
            }
            else Console.WriteLine("Преобразование не доступно");
            //is также может сразу преобразовать переменную, в случае возвращения true:
            if (client is Client clientCast2)       //тут, если client можно преобразовать Client, то is сразу это делает и записывает в переменную clientCast2
                Console.WriteLine(clientCast2.Bank);
            else 
                Console.WriteLine("Преобразование не доступно");
        }
        interface ICreature
        {
            void Print();
        }
        class Person: ICreature
        {
            public string Name { get; set; }
            public Person(string name)
            {
                Name = name;
            }
            public void Print()
            {
                Console.WriteLine($"Person {Name}\n");
            }
        }
        class Employee : Person
        {
            public string Company { get; set; }
            public Employee(string name, string company) : base(name)
            {
                Company = company;
            }
        }
        class Client : Person
        {
            public string Bank { get; set; }
            public Client(string name, string bank) : base(name)
            {
                Bank = bank;
            }
        }
        class car { }
    }
}
