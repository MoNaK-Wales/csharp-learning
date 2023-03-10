using System;
using System.IO;    //для работы с файлами нужно пространство имён System.IO
using System.Text;

namespace files
{
    class Program 
    {    
        static void Main(string[] args)
        {
            //для работы с дисками используется класс DriveInfo. для получения всех дисков используется статический метод GetDrives():
            DriveInfo[] drives = DriveInfo.GetDrives();           
            
            foreach(DriveInfo d in drives)                               //свойства:
            {
                Print("название: " + d.Name);                       //Name возвращает имя диска
                Print("каталог: " + d.RootDirectory);               //RootDirectory возвращает корневой каталог диска
                Print("тип: " + d.DriveType);                       //DriveType возвращает тип диска: Fixed, CDRom, Ram, Network, Removeable или NoRootDirectory
                Print("диск готов? " + d.IsReady);                  //возвращает true или false в зависимости от того, готов ли диск
                if (!d.IsReady)
                    continue;
                Print("тип ф.с.: " + d.DriveFormat);                //DriveFormat возвращает тип файловой системы (у меня NTFS)
                Print("места на диске: " + d.TotalSize);            //TotalSize возвращает общий размер диска в байтах
                Print("свободного места: " + d.TotalFreeSpace);     //TotalFreeSize возвращает весь объём свободного места диска в байтах
                Print("доступного места: " + d.AvailableFreeSpace); //AvailableFreeSize возвращает объём свободного места диска в байтах с учетом ограничений
                Print("метка диска: " + d.VolumeLabel);             //VolumeLabel возвращает метку тома (у меня их нет)
                Print("");
            }
            Console.WriteLine();

            //для работы с папками используются Directory и статический DirectoryInfo
            //Directory
            const string path = @"D:\темп";   //для обозначения пути используется @
            const string dir = @"d:\Users\Maks\Desktop\learn\8.3 files";
            const string downloads = @"d:\Users\Maks\Downloads\темп";
            Directory.CreateDirectory(path);            //создаёт папку по указанному пути, если её ещё нет: создаётся папка "темп" в диске D
            Directory.Move(path, downloads);            //перемещает папку из первого указанного пути во второй (нужно указать путь с самой папкой)
            Directory.SetCurrentDirectory(dir);         //устанавливает папку, в которой происходит работа
            Print(Directory.GetCurrentDirectory());     //возвращает весь путь к папке, в которой сейчас происходит работа
            Print(Directory.Exists(downloads));         //возвращает true, если указанный путь существует: папка "темп" в загрузках есть, true  
            Print(Directory.GetDirectories(dir));       //возвращает ͟с͟п͟и͟с͟о͟к папок в указанном пути
            Print(Directory.GetDirectories(dir, "P*")); //можно использовать фильтрацию, для нахождения определённых папок. так возвращается список папок на букву P
            Print(Directory.GetFiles(dir));             //возвращает ͟с͟п͟и͟с͟о͟к файлов в указанном пути
            Print(Directory.GetFiles(dir, "*.cs"));     //можно использовать фильтрацию, для нахождения определённых файлов. так возвращается список только cs файлов
            Print(Directory.GetFileSystemEntries(dir)); //возвращает ͟с͟п͟и͟с͟о͟к папок и файлов в указанном пути
            Print(Directory.GetParent(dir).FullName);   //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DirectoryInfo с родительской папкой указанной директивы: learn
            Print(Directory.GetCreationTime(dir));      //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время создания указанной директивы: 09.05.2022 21:16:27
            Print(Directory.GetLastWriteTime(dir));     //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего изменения в указанной директиве
            Print(Directory.GetLastAccessTime(dir));    //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего обращения к указанной директиве
            Directory.Delete(downloads);                //удаляет указанную папку/файл (закомментировать, чтобы проверить наличие папки)

            Print("");

            //DirectoryInfo
            const string folderPath = dir+@"\forFiles"; //папка для работы с ней в этом проекте
            const string sub = "подпапка";
            Directory.CreateDirectory(folderPath);
            DirectoryInfo folder = new DirectoryInfo(folderPath);   //для создания экземпляра DirectoryInfo в конструктор передаётся путь к папке
            //основные свойства:
            Print(folder.Name);                                 //возвращает название папки экземпляра
            Print(folder.FullName);                             //возвращает полный путь к папке
            Print(folder.Root.FullName);                        //Root возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DirectoryInfo с корневой папкой указанной директивы
            Print(folder.Parent.FullName);                      //Parent возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DirectoryInfo с родительской папкой указанной директивы: 8.3 files
            Print(folder.Exists);                               //возвращает true, если путь экземпляра существует
            Print(folder.CreationTime);                         //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время создания каталога
            Print(folder.LastWriteTime);                        //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего изменения каталога
            Print(folder.LastAccessTime);                       //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего обращения каталога
            //основные методы:
            try { folder.Delete(); }                            //удаляет папку если она пуста (в ином случае вызывается ошибка)
            catch { folder.Delete(true); }                      //удаляет папку со всеми файлами и папками внутри
            folder.Create();                                    //создаёт папку по пути указанному в объекте, если такой папки ещё нет
            folder.CreateSubdirectory(sub);                     //создаёт вложенную папку в папке экземпляра. путь можно задавать относительно объекта папки
            DirectoryInfo[] dirs = folder.GetDirectories();     //возвращает ͟с͟п͟и͟с͟о͟к͟ ͟о͟б͟ъ͟е͟к͟т͟о͟в͟ ͟к͟л͟а͟с͟с͟а DyrectoryInfo — все папки внутри папки экземпляра
            FileInfo[] files = folder.GetFiles();               //возвращает ͟с͟п͟и͟с͟о͟к͟ ͟о͟б͟ъ͟е͟к͟т͟о͟в͟ ͟к͟л͟а͟с͟с͟а FileInfo — все файлы внутри папки экземпляра
            Print(files);
            Print(dirs);

            Print("");


            //для работы с файлами используются FileInfo и статический File
            //FileInfo
            const string filePath = dir + "\\forFiles\\fileinfo.txt";   
            FileInfo file = new FileInfo(filePath);           //для создания экземпляра FileInfo в конструктор передаётся путь к папке
            //основные методы:
            file.Create().Close();                              //создаёт файл с заданным в конструктор путём, если такого ещё нет
            file.MoveTo(dir + "\\file.txt");                    //перемещает файл в указанный путьй: файл теперь в основной папке проекта
            file.CopyTo(filePath, true);                        //копирует файл в указанный путь. аргумент true указывает что файл может быть перезаписан
            //основные свойства:
            Print(file.Name);                                   //возвращает название файла экземпляра
            Print(file.FullName);                               //возвращает полный путь к файлу
            Print(file.Directory.FullName);                     //Directory возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DirectoryInfo с родительской папкой файла
            Print(file.DirectoryName);                          //возвращает строку с путём к каталогу файла
            Print(file.Exists);                                 //возвращает true, если путь экземпляра существует
            Print(file.Extension);                              //возвращает тип файла включая точку: .txt
            Print(file.IsReadOnly);                             //возвращает true если файл доступен только для чтения
            Print(file.Length);                                 //возвращает значение типа l͟o͟n͟g͟ с размером файла в байтах (сейчас 0)
            Print(file.CreationTime);                           //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время создания каталога
            Print(file.LastWriteTime);                          //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего изменения каталога
            Print(file.LastAccessTime);                         //возвращает ͟о͟б͟ъ͟е͟к͟т͟ ͟к͟л͟а͟с͟с͟а DateTime — время последнего обращения каталога

            file.Delete();                                      //метод удаляет файл

            Print("");

            //File
            const string textFile = @"forFiles\file.txt";   //можно использовать относительный путь от текущей папки (Directory.GetCurrentDirectory())
            File.Create(textFile).Close();                  //создаёт файл по указанному пути. Close закрывает файл, так как его создание сразу открывает его
            File.Move(textFile, dir + "\\file.txt");        //перемещает файл из первого указанного пути во второй: файл теперь в основной папке проекта
            File.Copy(dir + "\\file.txt", textFile, true);  //копирует файл из первого пути во второй. аргумент true указывает что файл может быть перезаписан
            File.Delete(dir+"\\file.txt");                  //удаляет файл по указанному пути: файл теперь только в папке для файла
            Print(File.Exists(textFile));                   //проверяет наличия указанного файла: True

            Print("");

            //ЧТЕНИЕ И ЗАПИСЬ ФАЙЛОВ
            //для чтения и записи файлов можно использовать как File, так и FileInfo
            //если выполняется много операций над однима файлом лучше использовать FileInfo, а если операций над одним файлом 1-2 лучше использовать File
            const string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, \nsed do eiusmod tempor incididunt ut labore et dolore";
            string[] textArr = { "Lorem", "ipsum", "dolor" };
            string[] textArr2 = { "список", "из", "слов" };
            File.WriteAllText(textFile, lorem);     //записывает текст в файл (указывать путь). если файла нет, создаётся новый. если файл есть то он перезаписывается
            File.WriteAllLines(textFile, textArr);  //записывает все строки списка в файл (каждая с новой строчки). если файла нет, создаётся новый
                                                    //предыдущий текст перезаписан
            File.AppendAllText(textFile, lorem);    //добавляет в конец файла текст. если файла нет, создаётся новый
            File.AppendAllLines(textFile, textArr2);//добавляет в конец файла строки (каждая с новой строчки). если файла нет, создаётся новый
            Print(File.ReadAllText(textFile));      //возвращает ͟с͟т͟р͟о͟к͟у с текстом всего файла
            Print(File.ReadAllLines(textFile));     //возвращает ͟с͟п͟и͟с͟о͟к͟ ͟с͟т͟р͟о͟к со всеми строчками файла
            File.WriteAllText(filePath, lorem, Encoding.Unicode);   //можно указывать кодировку записи с помощью Encoding из пространства имён System.Text
            Print(File.ReadAllText(filePath, Encoding.Unicode));

            Print("");

            //FileStream
            //класс FileStream даёт возможности для считывания и записи файла. для создания экземляра FileStream cуществует много конструкторов, но основной —
            /*FileStream(string path, FileMode mode), где mode элемент из перечисления FileMode:
                Append - открывает файл и находит конец файла. если файла нет, создаёт новый
                Create - создаёт файл. если файл уже есть — перезаписывается 
                CreateNew - создаёт файл. если файл уже есть — создается исключение
                Open - открывает файл. если файла нет — создается исключение
                OpenOrCreate - открывает файл. если файла нет — создаётся новый
                Truncate - открывает файл только для чтения. если файла нет — создается исключение 
            */
            string streamPath = @"forFiles\stream.txt";
            FileStream stream = new FileStream(streamPath, FileMode.OpenOrCreate);  //так как файла сначала нет, создаёт новый и открывает его
            //основные свойства:
            stream.Position = 49;       //указывает позицию курсора в файле
            Print(stream.Position);     //49
            Print(stream.Length);       //возвращает длину потока в байтах (пока он пустой, длина равна 0)
            Print(stream.CanRead);      //возвращает true, если поток поддерживает чтение: True
            Print(stream.CanWrite);     //возвращает true, если поток поддерживает запись: True
            Print(stream.CanSeek);      //возвращает true, если поток поддерживает поиск: True
            Print(stream.CanTimeout);   //возвращает true, если для потока может истечь время ожидания: False
            //FileStream работает с файлами на уровне байтов, поэтому строки надо преобразовывать в байтов с помощью Encoding.Default.GetBytes()
            byte[] text = Encoding.Default.GetBytes("прривет"); //перевод текст в список байтов
            stream.Write(text, 0 , text.Length);  //Write принимает три аргумента: список байтов для записи, смещение байтов (0), максимальное число байтов для записи
                                                  //строка записалась начиная с 49 позиции курсора, так как мы так установили свойство Position
            stream.Close(); //в конце работы с потоком, файл нужно закрыть

            FileStream fileStream = File.Open(streamPath, FileMode.Open);  //откроем файл ещё раз, но другим способом и другим именем 
            int length = Convert.ToInt32(fileStream.Length);
            byte[] readBytes = new byte[length];
            fileStream.Read(readBytes, 0, length);    //Read записывает в указанный список байты, со смещением (0), c максимальным к-вом байтов (длинна потока)
            Print(Encoding.Default.GetString(readBytes));   //перевод списка байтов обратно в строку
            //для удобного перемещения курсора можно использовать функцию Seek(long, SeekOrigin), где 1 параметр это количество символов для смещения, а 2 это
            //позиция откуда идёт смещение Begin, Current, End — с начала, текущего положения или с конца соответсвенно
            fileStream.Seek(-5, SeekOrigin.End); //смещает курсор на 5 символом назад с конца
            fileStream.WriteByte(32);            //WriteByte записывает один байт на текущее место курсора: добавлен байт 32 (пробел), теперь в файле слово "пр ривет"
                                                 //мой компьютер использует кодовую страницу (кодировку)   ͇C͇P͇8͇6͇6
            fileStream.Close();                  //закрытие потока

            //для работы непосредственно с текстовыми файлами есть специальные классы: StreamWriter и StreamReader
            //StreamWriter
            string writerPath = @"forFiles\writer reader.txt";
            StreamWriter writer = new StreamWriter(writerPath, true);   //если 2 параметр true, то новые данные будут записаны в конец, если false —
                                                                        //файл перезаписывается. если файла нет, то создаётся новый
            writer.Write(lorem);        //Write записывает в файл значение. имеет много перегрузок для разных типов значений
            writer.Write(4243f);        //перегрузка Write для типа float
            writer.Write("\n");         //перевод строки с помощью Write
            writer.WriteLine("Hi!");    //WriteLine записывает в файл значение и переходит на следующую строку в конце. также имеет много перегрузок
            writer.WriteLine(true);     //перегрузка WriteLine для типа bool
            writer.Close();             //закрывает поток и объект StreamWriter
            //StreamReader. StreamReader имеет текущую позицию, однако её трудно изменить, поэтому когда позиция доходит до конца, вернуться уже сложно
            StreamReader reader = new StreamReader(writerPath);

            Print(reader.Peek());           //возвращает следующий символ в байтах. если это конец файла, возвращает -1: 76 (L в CP866)
            Print(reader.Read());           //возвращает слудующий символ в байтах и перемещает позицию к следующему символу: 76
            Print(reader.ReadLine()+"\n");  //возвращает строку от текущей позиции до конца строчки: первая строчка без L
            Print(reader.ReadToEnd());      //возвращает текст с текущей позиции до конца файла и перемещает туда позицию потока
            if(reader.Peek() == -1)         //проверка на конец файла
            {
                Print("Конец файла");
                reader.Close();             //закрывает поток и объект StreamReader
            }

            Console.WriteLine("ЗАКРЫВАТЬ ЧЕРЕЗ ENTER");
            Console.Read();
            folder.Delete(true);
        }

        static void Print(string s) => Console.WriteLine(s);    //укороченная запись
        static void Print(bool s) => Console.WriteLine(s.ToString());
        static void Print(long s) => Console.WriteLine(s.ToString());
        static void Print(string[] s)
        {
            Console.WriteLine("{");
            foreach(string str in s)
            {
                Console.WriteLine("\t" + str + ",");
            }
            Console.WriteLine("}");
        }
        static void Print(byte[] s)
        {
            Console.WriteLine("{");
            foreach(byte str in s)
            {
                Console.WriteLine("\t" + str + ",");
            }
            Console.WriteLine("}");
        } 
        static void Print(FileInfo[] s)
        {
            Console.WriteLine("{");
            foreach(FileInfo str in s)
            {
                Console.WriteLine("\t" + str + ",");
            }
            Console.WriteLine("}");
        }
        static void Print(DirectoryInfo[] s)
        {
            Console.WriteLine("{");
            foreach(DirectoryInfo str in s)
            {
                Console.WriteLine("\t" + str + ",");
            }
            Console.WriteLine("}");
        }
        static void Print(DateTime s) => Console.WriteLine(s);
    }
}
