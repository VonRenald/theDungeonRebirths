using System;
using System.Numerics;

namespace GenerationMap
{
    
    class Program
    {
        static int Main(string[] args)
        {
            
            Random random = new Random();
            for(int i=0; i<1;i++){

            
                // GenerationMap gm = new GenerationMap(50,50,random.Next(15,30+1),3,5,1,2,i);
                GenerationMap gm = new GenerationMap(50,50,random.Next(7,10+1),7,15,1,2,i);
                // GenerationMap gm = new GenerationMap(50,50,2,2,2,1,1,i);
                
                // GenerationMap gm = new GenerationMap(50,50,i+2,3,5,1,2);
                // GenerationMap gm = new GenerationMap(20,20,4,2,3,1,1,i);
                // gm.printGrid();
                string str = "img";
                str += i.ToString() + ".png";
                Console.WriteLine(str);
                // gm.createPng(str,10);

                MapTitles titles = new MapTitles();
                titles.createPngV2("img/title3"+str,gm.grid2,50,50,"tilte3x3.png",3);
                // titles.createPngV2("img/title16"+str,gm.grid2,50,50,"tilte16x16.png",16);
                gm.createPngV2("img/"+str,gm.grid2,10);
                gm.createPngV2("img/result"+str,gm.grid,1);
            }
            // Console.Read();








            // Vector2 p1 = new Vector2(1,5);    
            // Vector2 p2 = new Vector2(3,2);    
            // Vector2 p3 = new Vector2(5,5);    
            // Vector2 p4 = new Vector2(1,1);

            // IDictionary<Vector2,int> Coor2Vec = new Dictionary<Vector2,int>();
            // Coor2Vec.Add(p1,1);    
            // Coor2Vec.Add(p2,2);    
            // Coor2Vec.Add(p3,3);    
            // Coor2Vec.Add(p4,4);    

            // foreach(KeyValuePair<Vector2, int> kvp in Coor2Vec)
            //     Console.WriteLine("Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            
            // int ret;
            // if (Coor2Vec.TryGetValue(new Vector2(5,5),out ret))
            //     Console.WriteLine("Foud {0}", ret);
            // else
            //     Console.WriteLine("Not Found");
            return 0;
        }
    }
}
