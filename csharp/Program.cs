using System;

namespace GenerationMap
{
    
    class Program
    {
        static int Main(string[] args)
        {
            // Console.WriteLine("Hello, World!");
            // Console.WriteLine("test {0} {1}",324,"ono");
            Random random = new Random();
            GenerationMap gm = new GenerationMap(20,20,random.Next(3,5+1),2,5,1,4);
            gm.printGrid();


            Console.Read();
            return 0;
        }
    }
}

