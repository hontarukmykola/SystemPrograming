namespace _02_MultyTheadTest
{
    internal class Program
    {

        static void InfinityLoop()
        {
            Console.WriteLine("Thread has been started!");
            while (true)
            {
                int a = 5;
                int b = a;
                int c = a + b;
                new Random().Next(100);
            }
        }


        static void Main(string[] args)
        {
            ConsoleKeyInfo input;

            do
            {
                input = Console.ReadKey();

                Thread thread = new Thread(InfinityLoop);
                thread.Start();

                //InfinityLoop();

            } while (input.Key != ConsoleKey.Escape);
        }
    }
}