using System;

namespace GenerationMap
{
    
    class Program
    {
        static int Main(string[] args)
        {
            Random random = new Random();
            for(int i=0; i<3;i++){

            
                GenerationMap gm = new GenerationMap(50,50,random.Next(15,30+1),3,5,1,2);
                // GenerationMap gm = new GenerationMap(50,50,i+2,3,5,1,2);
                // GenerationMap gm = new GenerationMap(20,20,4,2,3,1,1);
                // gm.printGrid();
                string str = "img";
                str += i.ToString() + ".png";
                Console.WriteLine(str);
                // gm.createPng(str,10);

                MapTitles titles = new MapTitles();
                titles.createPngV2("img/title3"+str,gm.grid2,50,50,"tilte3x3.png",3);
                titles.createPngV2("img/title16"+str,gm.grid2,50,50,"tilte16x16.png",16);
                gm.createPngV2("img/"+str,gm.grid2,1);
            }
            // Console.Read();
            return 0;
        }
    }
}

