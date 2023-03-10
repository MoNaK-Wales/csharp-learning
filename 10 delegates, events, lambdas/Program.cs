namespace delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Header("Делегаты и лямбды");
            Delegates.DelegatesMain();
            Header("События");
            Events.EventsMain();
        }
        static void Header(string t)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t" + t);
            Console.ResetColor();
        }
    }
}