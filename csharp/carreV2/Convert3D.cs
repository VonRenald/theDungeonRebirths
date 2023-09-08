using System;
using System.Numerics;
using System.IO;

namespace GenerationMap
{
    class Convert3D
    {
        // public int Width;
        // public int Height;
        // public Case[,] grid;
        public List<Vector3> vertices = new List<Vector3>();
        public List<int> triangle = new List<int>(); 
        int nextId = 0;
        IDictionary<Vector3,int> Coor2triangle = new Dictionary<Vector3,int>();

        public Convert3D(int width, int height, Case[,] grid){ //mur droit
            this.vertices = new List<Vector3>();
            this.triangle = new List<int>();
            
            this.nextId = 0;

            simplificationCase sc = new simplificationCase();
            for(int i=0; i<width; i++){
                for(int j=0; j<height;j++){
                    if(sc.wall.Contains( grid[i,j])){
                        // génere sommet du mur
                        Vector3 coor0 = new Vector3(i,j+1,0);
                        int id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
                        Coor2triangle[coor0] = id0;
                        Vector3 coor1 = new Vector3(i,j,0);
                        int id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
                        Coor2triangle[coor1] = id1;
                        Vector3 coor2 = new Vector3(i+1,j+1,0);
                        int id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
                        Coor2triangle[coor2] = id2;
                        Vector3 coor3 = new Vector3(i+1,j,0);
                        int id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
                        Coor2triangle[coor3] = id3;
                        
                        addVerticesIds(new Vector3[]{coor0, coor1, coor2, coor3},new int[]{id0, id1, id2, id2, id1, id3});
                        
                        // if (!vertices.Contains(coor0)) vertices.Add(coor0); 
                        // if (!vertices.Contains(coor1)) vertices.Add(coor1); 
                        // if (!vertices.Contains(coor2)) vertices.Add(coor2); 
                        // if (!vertices.Contains(coor3)) vertices.Add(coor3);
                        // triangle.Add(id0);
                        // triangle.Add(id1);
                        // triangle.Add(id2);
                        // triangle.Add(id2);
                        // triangle.Add(id1);
                        // triangle.Add(id3);

                        // case suivant le type de mur
                        caseWall(grid[i,j],coor1);
                    }
                }
            }
        }
        public void caseWall(Case c,Vector3 coor)
        {
            // Vector3 coor0, coor1, coor2, coor3;
            // int id0, id1, id2, id3;

            if(c == Case.WALL_UP || c == Case.WALL_DOUBLE_H || c == Case.WALL_T_UP_RIGHT || c == Case.CORNER_IN_MIDLE_LEFT || c == Case.CORNER_IN_UP_LEFT || c == Case.WALL_T_UP_LEFT || c == Case.CORNER_IN_MIDLE_RIGHT || c == Case.CORNER_IN_MIDLE_UP || c == Case.CORNER_IN_UP_RIGHT || c == Case.CORNER_DOUBLE_UP_RIGHT || c == Case.CORNER_DOUBLE_DOWN_RIGHT || c == Case.CORNER_IN_MIDLE || c == Case.WALL_T_UP || c == Case.CORNER_DOUBLE_UP_LEFT) 
                buildUP(coor);
            if(c == Case.WALL_DOWN || c == Case.WALL_T_DOWN_RIGHT || c == Case.WALL_DOUBLE_H || c == Case.CORNER_IN_MIDLE_LEFT || c == Case.CORNER_IN_MIDLE_DOWN || c == Case.CORNER_IN_DOWN_LEFT || c == Case.WALL_T_DOWN_LEFT || c == Case.CORNER_IN_MIDLE_RIGHT || c == Case.CORNER_IN_DOWN_RIGHT || c == Case.CORNER_DOUBLE_DOWN_RIGHT || c == Case.CORNER_IN_MIDLE || c == Case.WALL_T_DOWN || c == Case.CORNER_DOUBLE_DOWN_LEFT)
                buildDOWN(coor);
            if(c == Case.WALL_RIGHT || c == Case.WALL_DOUBLE_V || c == Case.WALL_T_RIGHT_UP || c == Case.CORNER_IN_MIDLE_DOWN || c == Case.WALL_T_RIGHT_DOWN || c == Case.CORNER_IN_MIDLE_RIGHT || c == Case.CORNER_IN_DOWN_RIGHT|| c == Case.WALL_T_RIGHT || c == Case.CORNER_IN_MIDLE_UP || c == Case.CORNER_IN_UP_RIGHT || c == Case.CORNER_DOUBLE_UP_RIGHT || c == Case.CORNER_DOUBLE_DOWN_RIGHT || c == Case.CORNER_IN_MIDLE)
                buildRIGHT(coor);
            if(c == Case.WALL_LEFT || c == Case.WALL_DOUBLE_V || c == Case.CORNER_IN_MIDLE_LEFT || c == Case.CORNER_IN_UP_LEFT || c == Case.CORNER_IN_MIDLE_DOWN || c == Case.CORNER_IN_DOWN_RIGHT || c == Case.WALL_T_LEFT_DOWN || c == Case.WALL_T_LEFT_UP || c == Case.CORNER_IN_MIDLE_UP || c == Case.WALL_T_LEFT || c == Case.CORNER_IN_MIDLE || c == Case.CORNER_DOUBLE_UP_LEFT || c == Case.CORNER_DOUBLE_DOWN_LEFT || c == Case.CORNER_IN_DOWN_LEFT)
                buildLEFT(coor);

            // switch(c){
            // case Case.WALL_UP:
            //     buildUP(coor);
            //     break;
            // case Case.WALL_DOWN:
            //     buildDOWN(coor);
            //     break;
            // case Case.WALL_RIGHT:
            //     buildRIGHT(coor);
            //     break;
            // case Case.WALL_LEFT:
            //     buildLEFT(coor);
            //     break;
            // default:
            //     coor0 = new Vector3(0,0,0); 
            //     id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
            //     coor1 = new Vector3(0,0,0);
            //     id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
            //     coor2 = new Vector3(0,0,0);
            //     id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
            //     coor3 = new Vector3(0,0,0);
            //     id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
            //     break;
            // }
        }
        private void addVerticesIds(Vector3[] coors,int[] ids)
        {
            foreach (var coor in coors)
                if (!vertices.Contains(coor)) vertices.Add(coor);
            foreach (var id in ids)
                triangle.Add(id);
        }
        private void buildUP(Vector3 coor)
        {
            Vector3 coor0, coor1, coor2, coor3;
            int id0, id1, id2, id3;
            coor0 = coor + new Vector3(0,1,-1); 
            id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
            coor1 = coor + new Vector3(0,1,0);
            id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
            coor2 = coor + new Vector3(1,1,-1);
            id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
            coor3 = coor + new Vector3(1,1,0);
            id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
            Coor2triangle[coor0] = id0;
            Coor2triangle[coor1] = id1;
            Coor2triangle[coor2] = id2;
            Coor2triangle[coor3] = id3;
            addVerticesIds(new Vector3[]{coor0, coor1, coor2, coor3},new int[]{id0, id1, id2, id2, id1, id3});
        }
        private void buildDOWN(Vector3 coor)
        {
            Vector3 coor0, coor1, coor2, coor3;
            int id0, id1, id2, id3;
            coor0 = coor + new Vector3(1,0,-1); 
            id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
            coor1 = coor + new Vector3(1,0,0);
            id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
            coor2 = coor + new Vector3(0,0,-1);
            id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
            coor3 = coor + new Vector3(0,0,0);
            id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
            Coor2triangle[coor0] = id0;
            Coor2triangle[coor1] = id1;
            Coor2triangle[coor2] = id2;
            Coor2triangle[coor3] = id3;
            addVerticesIds(new Vector3[]{coor0, coor1, coor2, coor3},new int[]{id0, id1, id2, id2, id1, id3});
        }
        private void buildRIGHT(Vector3 coor)
        {
            Vector3 coor0, coor1, coor2, coor3;
            int id0, id1, id2, id3;
            // génere le mur a l'oust
            coor0 = coor + new Vector3(0,0,-1); 
            id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
            coor1 = coor + new Vector3(0,0,0);
            id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
            coor2 = coor + new Vector3(0,1,-1);
            id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
            coor3 = coor + new Vector3(0,1,0);
            id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
            Coor2triangle[coor0] = id0;
            Coor2triangle[coor1] = id1;
            Coor2triangle[coor2] = id2;
            Coor2triangle[coor3] = id3;
            addVerticesIds(new Vector3[]{coor0, coor1, coor2, coor3},new int[]{id0, id1, id2, id2, id1, id3});
        }
        private void buildLEFT(Vector3 coor)
        {
            Vector3 coor0, coor1, coor2, coor3;
            int id0, id1, id2, id3;
            // génere le mur a l'est
            coor0 = coor + new Vector3(1,1,-1); 
            id0 = (Coor2triangle.ContainsKey(coor0))? Coor2triangle[coor0]:nextId++;
            coor1 = coor + new Vector3(1,1,0);
            id1 = (Coor2triangle.ContainsKey(coor1))? Coor2triangle[coor1]:nextId++;
            coor2 = coor + new Vector3(1,0,-1);
            id2 = (Coor2triangle.ContainsKey(coor2))? Coor2triangle[coor2]:nextId++;
            coor3 = coor + new Vector3(1,0,0);
            id3 = (Coor2triangle.ContainsKey(coor3))? Coor2triangle[coor3]:nextId++;
            Coor2triangle[coor0] = id0;
            Coor2triangle[coor1] = id1;
            Coor2triangle[coor2] = id2;
            Coor2triangle[coor3] = id3;
            addVerticesIds(new Vector3[]{coor0, coor1, coor2, coor3},new int[]{id0, id1, id2, id2, id1, id3});
        }
    
        public void writeObj(string str){
            try
            {
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(str+".obj");

                foreach(Vector3 vec in vertices){
                    sw.WriteLine("v {0}.0 {1}.0 {2}.0",vec.X,vec.Y,vec.Z);
                }
                for(int i = 0; i < triangle.Count; i+=3){
                    sw.WriteLine("f {0} {1} {2}",triangle[i]+1,triangle[i+1]+1,triangle[i+2]+1);
                }
                //Close the file
                sw.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            // finally
            // {
            //     Console.WriteLine("Executing finally block.");
            // }
        }
    }

}