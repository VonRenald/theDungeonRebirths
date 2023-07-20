using System;
using System.Drawing;

namespace GenerationMap
{
    struct Point{
        public int x,y;
        public Point(int x_=0,int y_=0){
            x=x_;
            y=y_;
        }
    }

    class MapTitles
    {
        private Dictionary<Case, Point> title;
        public MapTitles()
        {
            title = new Dictionary<Case, Point>();
            title.Add(Case.DOOR_H,new Point(0,0));
            title.Add(Case.DOOR_H_OPEN,new Point(0,1));
            title.Add(Case.DOOR_V,new Point(0,2));
            title.Add(Case.DOOR_V_OPEN,new Point(0,3));
            title.Add(Case.NULL,new Point(0,4));
            title.Add(Case.ERROR,new Point(0,5));

            title.Add(Case.WALL_T_RIGHT_LEFT,new Point(1,0));
            title.Add(Case.WALL_T_RIGHT_UP,new Point(1,1));
            title.Add(Case.WALL_T_RIGHT_DOWN,new Point(1,2));
            title.Add(Case.WALL_T_LEFT_DOWN,new Point(1,3));
            title.Add(Case.CROSS_DIAG_LEFT,new Point(1,4));
            title.Add(Case.CROSS_DIAG_RIGHT,new Point(1,5));
            
            title.Add(Case.WALL_T_UP_DOWN,new Point(2,0));
            title.Add(Case.WALL_T_UP_RIGHT,new Point(2,1));
            title.Add(Case.WALL_T_UP_LEFT,new Point(2,2));
            title.Add(Case.WALL_T_LEFT_UP,new Point(2,3));
            title.Add(Case.CROSS_DOWN_RIGHT,new Point(2,4));
            title.Add(Case.CROSS_DOWN_LEFT,new Point(2,5));
            
            title.Add(Case.WALL_T_DOWN_UP,new Point(3,0));
            title.Add(Case.WALL_T_DOWN_RIGHT,new Point(3,1));
            title.Add(Case.WALL_T_DOWN_LEFT,new Point(3,2));
            title.Add(Case.WALL_T_LEFT_RIGHT,new Point(3,3));
            title.Add(Case.CROSS_UP_RIGHT,new Point(3,4));
            title.Add(Case.CROSS_UP_LEFT,new Point(3,5));
            
            title.Add(Case.WALL_UP,new Point(4,0));
            title.Add(Case.WALL_LEFT,new Point(4,1));
            title.Add(Case.WALL_RIGHT,new Point(4,2));
            title.Add(Case.WALL_DOWN,new Point(4,3));
            title.Add(Case.ROOM,new Point(4,4));
            title.Add(Case.CORRIDOR,new Point(4,4));
            title.Add(Case.DOOR,new Point(4,4));
            title.Add(Case.WALL_T_DOWN,new Point(4,5));
            
            title.Add(Case.WALL_DOUBLE_V,new Point(5,0));
            title.Add(Case.WALL_DOUBLE_H,new Point(5,1));
            title.Add(Case.CROSS,new Point(5,2));
            title.Add(Case.WALL_T_RIGHT,new Point(5,3));
            title.Add(Case.WALL_T_LEFT,new Point(5,4));
            title.Add(Case.WALL_T_UP,new Point(5,5));
            
            title.Add(Case.CORNER_UP_LEFT,new Point(6,0));
            title.Add(Case.CORNER_DOWN_LEFT,new Point(6,1));
            title.Add(Case.CORNER_DOWN_RIGHT,new Point(6,2));
            title.Add(Case.CORNER_UP_RIGHT,new Point(6,3));
            title.Add(Case.CORNER_DOUBLE_UP_RIGHT,new Point(6,4));
            title.Add(Case.CORNER_DOUBLE_UP_LEFT,new Point(6,5));
            
            title.Add(Case.CORNER_IN_MIDLE_LEFT,new Point(7,0));
            title.Add(Case.CORNER_IN_MIDLE_DOWN,new Point(7,1));
            title.Add(Case.CORNER_IN_MIDLE_RIGHT,new Point(7,2));
            title.Add(Case.CORNER_IN_MIDLE_UP,new Point(7,3));
            title.Add(Case.CORNER_DOUBLE_DOWN_RIGHT,new Point(7,4));
            title.Add(Case.CORNER_DOUBLE_DOWN_LEFT,new Point(7,5));
            
            title.Add(Case.CORNER_IN_UP_LEFT,new Point(8,0));
            title.Add(Case.CORNER_IN_DOWN_LEFT,new Point(8,1));
            title.Add(Case.CORNER_IN_DOWN_RIGHT,new Point(8,2));
            title.Add(Case.CORNER_IN_UP_RIGHT,new Point(8,3));
            title.Add(Case.CORNER_IN_MIDLE,new Point(8,4));
            title.Add(Case.VOID,new Point(8,5));
        }
        public void createPngV2(string name, Case[,] myGrid, int w, int h, string titleName, int titleSize){
            Bitmap bmp = new Bitmap(w*titleSize,h*titleSize);
            Bitmap titleImg = new Bitmap(titleName);
            for(int x=0; x<w; x++){
                for(int y=0; y<h; y++){
                    Case caseRead = myGrid[x,y];
                    Point p = title[myGrid[x,y]];
                    
                    for(int i=0; i<titleSize; i++){
                        for(int j=0; j<titleSize; j++){
                            bmp.SetPixel(x*titleSize+i,y*titleSize+j,titleImg.GetPixel(p.x*titleSize+i,p.y*titleSize+j));
                        }
                    }
                    // titleImg.GetPixel()



                    // Color color = new Color();
                    
                    // // if (myGrid[x,y] == Case.WALL_UP || myGrid[x,y] == Case.WALL_DOWN || myGrid[x,y] == Case.WALL_RIGHT || myGrid[x,y] == Case.WALL_LEFT)
                    // // {
                    // //     //WALL
                    // //     color = Color.Blue;
                    // // }else if(myGrid[x,y] == Case.WALL_DOUBLE_H || myGrid[x,y] == Case.WALL_DOUBLE_V)
                    // // {
                    // //     //WALL DOUBLE
                    // //     color = Color.BlueViolet;
                    // // }else if(myGrid[x,y] == Case.CORNER_UP_RIGHT || myGrid[x,y] == Case.CORNER_UP_LEFT || myGrid[x,y] == Case.CORNER_DOWN_RIGHT || myGrid[x,y] == Case.CORNER_DOWN_LEFT){
                    // //     //CORNER out
                    // //     color = Color.Green;
                    // // }else if(myGrid[x,y] == Case.CORNER_IN_UP_RIGHT || myGrid[x,y] == Case.CORNER_IN_UP_LEFT || myGrid[x,y] == Case.CORNER_IN_DOWN_RIGHT || myGrid[x,y] == Case.CORNER_IN_DOWN_LEFT){
                    // //     //CORNER in
                    // //     color = Color.GreenYellow;
                    // // }else if(myGrid[x,y] == Case.CORNER_IN_MIDLE_UP|| myGrid[x,y] == Case.CORNER_IN_MIDLE_DOWN || myGrid[x,y] == Case.CORNER_IN_MIDLE_RIGHT || myGrid[x,y] == Case.CORNER_IN_MIDLE_LEFT || myGrid[x,y] == Case.CORNER_IN_MIDLE){
                    // //     //CORNER in midle
                    // //     color = Color.GreenYellow;
                    // // }else 
                    // if( myGrid[x,y] == Case.ROOM || myGrid[x,y] == Case.CORRIDOR){
                    //     color = Color.White;
                    // }else if(myGrid[x,y] == Case.DOOR){
                    //     color = Color.FromArgb(102,51,0);
                    // }else if(myGrid[x,y] == Case.NULL){
                    //     color = Color.FromArgb(0,0,0);
                    // }else if(myGrid[x,y] == Case.ERROR){
                    //     color = Color.Red;
                    // }else if(myGrid[x,y] != Case.VOID) {
                    //     color = Color.Blue;
                    // }
                    // for(int dx=0; dx<ratio; dx++){
                    //     for(int dy=0; dy<ratio; dy++){
                    //         bmp.SetPixel(x*ratio+dx,y*ratio+dy,color);
                    //     }
                    // }
                    
                }
            }
            bmp.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
        }

    }
}