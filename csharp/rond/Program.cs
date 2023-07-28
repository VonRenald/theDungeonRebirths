using System;
using System.Drawing;

namespace GenerationDonjon2
{
    class Program
    {
        static int Main(string[] args) 
        {
            // Point p1 = new Point(1,2);
            // Point p2 = new Point(4,8);
            // Console.WriteLine("{0} {1} {2} {3} {4} ",p1+p2,p1-p2,p1*p2,p1/p2,p1==p2);
            // Cercle c = new Cercle(p1,4);
            // Console.WriteLine("{0}",c);

            for(int i=0;i<5;i++){
                Piece piece = new Piece(15,100,100, 45);
                piece.Draw(i.ToString()+"test.png");
                Console.WriteLine("--");
            }
            for(int i=5;i<10;i++){
                Piece piece = new Piece(15,100,100, 25);
                piece.Draw(i.ToString()+"test.png");
                Console.WriteLine("--");
            }
            for(int i=10;i<15;i++){
                Piece piece = new Piece(15,100,100, 170);
                piece.Draw(i.ToString()+"test.png");
                Console.WriteLine("--");
            }
            
            return 0;
        }
    }
    
}