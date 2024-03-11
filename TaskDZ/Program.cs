namespace TaskDZ
{
    internal class Program
    {

        static void GetPrimeNumber(int a, int b)
        {
            Console.WriteLine($"Simple numbers from {a} to {b}:");

            for (int i = a; i <= b; i++)
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    Console.Write(i + " ");
                }

            }
        }

        static void getSum() 
        {
            int[] ints = new int[7];
            ints[0] = 1;
            ints[1] = 2;
            ints[2] = 3;
            ints[3] = 4;
            ints[4] = 5;
            ints[5] = 6;
            ints[6] = 7;


            Console.WriteLine($"Sum : {ints.Sum()}"); 
            
        }
        static void getMin()
        {
            int[] ints = new int[7];
            ints[0] = 1;
            ints[1] = 2;
            ints[2] = 3;
            ints[3] = 4;
            ints[4] = 5;
            ints[5] = 6;
            ints[6] = 7;


            Console.WriteLine($"Min : {ints.Min()}");
        }
        static void getAVG()
        {
            int[] ints = new int[7];
            ints[0] = 1;
            ints[1] = 2;
            ints[2] = 3;
            ints[3] = 4;
            ints[4] = 5;
            ints[5] = 6;
            ints[6] = 7;


            Console.WriteLine($"Avg : {ints.Average()}");
        }
        static void getMax()
        {
            int[] ints = new int[7];
            ints[0] = 1;
            ints[1] = 2;
            ints[2] = 3;
            ints[3] = 4;
            ints[4] = 5;
            ints[5] = 6;
            ints[6] = 7;


            Console.WriteLine($"Max : {ints.Max()}");
        }


        static int[] DeleteDublicate()
        {
            Console.WriteLine("You have array");
            int[] ints = new int[] { 4, 3, 5, 4, 3, 6, 7, 7, 4, 1, 3, 3, 4, 2, 4 };
            int[] uniqueInts = ints.Distinct().ToArray();
            foreach ( int i in uniqueInts )
            {
                Console.WriteLine(i);
            }
            return uniqueInts;

        }

        static int[] OrderByArray(Task<int[]> PreviousTask)
        {
            Console.WriteLine("\n\nSorted array");
            Array.Sort(PreviousTask.Result);
            foreach (int i in PreviousTask.Result)
            {
                Console.WriteLine(i);
            }
            return PreviousTask.Result;

        }
        static void BinarySearth(Task<int[]> PreviousTask)
        {
            Console.WriteLine("\n");
            int search = int.Parse(Console.ReadLine()) ;
            
           int c = Array.BinarySearch(PreviousTask.Result, search);
           Console.WriteLine($"\n\nCearch index Number : {c}");

       
        }



        static void Main(string[] args)
        {
            //1

            //DateTime date = DateTime.Now;
            //Task task1 = new Task(() => Console.WriteLine($"date new(task start) : {date.ToShortDateString()}\n"));
            //task1.Start();

            //Task task2 = Task.Factory.StartNew(() => Console.WriteLine($"date new(task startNew) : {date.ToShortDateString()}\n"));

            //Task task3 = Task.Run(() =>
            //{
            //    Console.WriteLine($"date new(task run) : {date.ToShortDateString()}\n");
            //});

            //Console.ReadKey();
            //Console.Clear();
            ////2,3

            //Console.WriteLine("Enter two number : ");
            //int a = int.Parse(Console.ReadLine());
            //int b = int.Parse(Console.ReadLine());

            //Task task4 = Task.Run(() => GetPrimeNumber(a,b));

            //Console.ReadKey();
            //Console.Clear();

            ////4


            //Task task5 = Task.Run(() => getSum());
            //Task task6 = Task.Run(() => getAVG());
            //Task task7 = Task.Run(() => getMax());
            //Task task8 = Task.Run(() => getMin());

            //Console.ReadKey();
            //Console.Clear();


            //5

            Task<int[]> task9 = new Task<int[]>(() => DeleteDublicate());
            Task res = task9.ContinueWith(OrderByArray).ContinueWith(BinarySearth);
            task9.Start();
            res.Wait();



        }
    }
}