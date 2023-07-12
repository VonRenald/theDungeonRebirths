using System;

namespace GenerationMap
{
    
    class Program
    {
        static int Main(string[] args)
        {
            Random random = new Random();
            for(int i=0; i<5;i++){

            
                GenerationMap gm = new GenerationMap(50,50,random.Next(5,7+1),5,10,1,3);
                // GenerationMap gm = new GenerationMap(20,20,4,2,3,1,1);
                // gm.printGrid();
                string str = "img";
                str += i.ToString() + ".png";
                Console.WriteLine(str);
                gm.createPng(str,10);
            }
            // Console.Read();
            return 0;
        }
    }
}

