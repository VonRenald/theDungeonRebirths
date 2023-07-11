using System;

namespace GenerationMap
{
    enum Case {
        VOID, ROOM, CORRIDOR, WALL, CORNER, DOOR
    };
    enum Dir {
        HORI,VERT
    };

    
    class GenerationMap
    {
        public int GRID_W = 20;
        public int GRID_H = 20;
        public Case[,] grid;
        private List<Room> rooms;
        private List<Door> doors;
        private List<Door> doorsWall;
        private List<Door> doorsLinked;

        private Room nullRoom;
        private Door nullDoor;
        
        private int randint(int min,int max)
        {
            return (new Random()).Next(min,max+1);
        }
        
        private class Room
        {
            public int x,y,w,h;
            public List<Door> doors {get;set;}
            public Room(int x_, int y_, int w_, int h_)
            {
                x=x_;
                y=y_;
                w=w_;
                h=h_;
                doors = new List<Door>();
            }
        }
        private class Door
        {
            public Room room {get;set;}
            public List<Door> link;
            public int x,y;
            public Dir dir;
            public Door(int x_, int y_, Dir dir_,Room room_)
            {
                x=x_;
                y=y_;
                dir=dir_;
                room = room_;
                link = new List<Door>();
            }
        }

        public class Elem
        {
            public int x,y,p;
            public Elem? parent;
            public Dir dir;
            public bool visited;

            private void init(int x_,int y_, int p_, Elem? parent_, Dir dir_, bool visited_)
            {
                x = x_;
                y = y_;
                p = p_;
                parent = parent_;
                dir = dir_;
                visited = visited_;
            }
            public Elem()
            {
                init(0,0,0,null,Dir.HORI,false);
            }
            public Elem(int x_,int y_, int p_=0, Dir dir_=Dir.HORI, bool visited_=false, Elem? parent_=null)
            {
                init(x_,y_,p_,parent_,dir_,visited_);
            }
        }

        public void printGrid()
        {
            Console.Write("   ");
            for(int x=0;x<GRID_W;x++)
            {
                Console.Write("{0}   ",x%10);
            }Console.Write('\n');
            for(int y=0; y<GRID_H;y++)
            {
                Console.Write("{0}   ",y%10);
                for(int x=0; x<GRID_W; x++)
                {
                    Case val = grid[x,y];
                    if(val == Case.VOID){
                        Console.Write("{0}   "," ");
                    }else if (val == Case.ROOM){
                        Console.Write("{0}   ",".");
                    }else if (val == Case.WALL){
                        Console.Write("{0}   ","2");
                    }else if (val == Case.CORRIDOR){
                        Console.Write("{0}   ","*");
                    }else if (val == Case.DOOR){
                        Console.Write("{0}   ","p");
                    }else if (val == Case.CORNER){
                        Console.Write("{0}   ","4");
                    }
                }Console.Write('\n');
            }
        }
        
        private Room getRoomByCoor(int x, int y)
        {
            return getRoomByCoor(x, y,new Room[] {});
        }
        private Room getRoomByCoor(int x, int y,Room[] toNotCheck)
        {
            for(int i=0;i<rooms.Count;i++)
            {
                if (!toNotCheck.Contains(rooms[i]))
                {
                    if ((x==rooms[i].x-1 || x==rooms[i].x+rooms[i].w) && (y>rooms[i].y-1 || y < rooms[i].y+rooms[i].h)){
                        return rooms[i];
                    }else if((y==rooms[i].y-1 || y==rooms[i].y+rooms[i].h) && (x>rooms[i].x-1 || x<rooms[i].x+rooms[i].w)){
                        return rooms[i];
                    }
                }
            }
            return (nullRoom);
        }
        private Door popDoorByCoor(int x,int y)
        {
            for(int i=0;i<doors.Count;i++){
                if(doors[i].x == x && doors[i].y == y)
                {
                    Door door = doors[i];
                    doors.RemoveAt(i);
                    return door;
                }
            }
            return nullDoor;
        }
        private Dir changerDir(Dir dir)
        {
            if(dir == Dir.HORI)
                return Dir.VERT;
            return Dir.HORI;
        }
        private List<Elem> corridor(int sx, int sy, Dir dir, int tx, int ty, int[,] grid)
        {
            Elem[,] elemGrid = new Elem[GRID_W,GRID_H];
            for(int x=0;x<GRID_W;x++){
                for(int y=0;y<GRID_H;y++){
                    elemGrid[x,y] = new Elem(x,y);
                }
            }
            grid[sx,sy] = 0;
            grid[tx,ty] = 0;
            elemGrid[sx,sy].visited=true;
            elemGrid[sx,sy].dir = dir;
            List<Elem> toSee = new List<Elem>();
            toSee.Add(elemGrid[sx,sy]);

            int [] driftX = {0, 1, 0,-1};
            int [] driftY = {-1, 0, 1, 0};
            int occ = 0;
            int occ2 = 0;
            int nx = 0;
            int ny =0;
            List<Elem> road = new List<Elem>(); 

            while(toSee.Count > 0)
            {
                occ +=1;
                Elem current = toSee[0];toSee.RemoveAt(0);
                for(int i=0; i<4; i++)
                {
                    nx = current.x+driftX[i];
                    ny = current.y+driftY[i];
                    if (nx >= 0 && nx < GRID_W
                        && ny >= 0 && ny<GRID_H 
                        && grid[nx,ny] == 0)//si coordonée valide
                    {
                        occ2 +=1;
                        if(elemGrid[nx,ny].visited)//si voisin visité
                        {
                            if(current.dir == Dir.HORI && driftY[i] == 0 || current.dir == Dir.VERT && driftX[i] == 0){ // pas de changment de direction
                                if(elemGrid[nx,ny].p > current.p+1)// nouveau chemin plus interesant
                                {
                                    elemGrid[nx,ny].p = current.p+1;
                                    elemGrid[nx,ny].parent = current;
                                    elemGrid[nx,ny].dir = current.dir;
                                    toSee.Add(elemGrid[nx,ny]);
                                }
                            }else{// changement de direction 
                                if(elemGrid[nx,ny].p > current.p+4)// nouveau chemin plus interesant
                                {
                                    elemGrid[nx,ny].p = current.p+4;
                                    elemGrid[nx,ny].parent = current;
                                    elemGrid[nx,ny].dir = changerDir(current.dir);
                                    toSee.Add(elemGrid[nx,ny]);
                                }
                            }
                        }else{//} voisin non visité
                            if(current.dir == Dir.HORI && driftY[i] == 0 || current.dir == Dir.VERT && driftX[i] == 0)// pas de changment de direction
                            {
                                elemGrid[nx,ny].p = current.p+1;
                                elemGrid[nx,ny].dir = current.dir;
                            }else{
                                elemGrid[nx,ny].p = current.p+4;
                                elemGrid[nx,ny].dir = changerDir(current.dir);
                            }
                            elemGrid[nx,ny].parent= current;
                            elemGrid[nx,ny].visited = true;
                            toSee.Add(elemGrid[nx,ny]);
                        }
                    }
                }
            }
            if(elemGrid[tx,ty].parent != null){
                road.Add((Elem)elemGrid[tx,ty].parent);
                nx = elemGrid[tx,ty].parent.x;
                ny = elemGrid[tx,ty].parent.y;
                int loop = 0;
                Console.WriteLine("coor {0} {1} start {2} {3} target {4} {5}",nx,ny,sx,sy,tx,ty);
                while (!(elemGrid[nx,ny].parent.x == sx && elemGrid[nx,ny].parent.y == sy))
                {
                    road.Add(elemGrid[nx,ny].parent);
                    int a = elemGrid[nx,ny].parent.x;
                    ny = elemGrid[nx,ny].parent.y;
                    nx = a;
                    loop +=1;
                    if (loop>1000)
                        return new List<Elem>();
                }
            }

            
            
            grid[sx,sy] = -1;
            grid[tx,ty] = -1;

            return road;
        }
        private int createRooms(int nbRoom, int minSize, int maxSize)
        {
            for(int e=0; e<nbRoom; e++){
                int x=0;
                int y=0;
                int w=0;
                int h=0;
                bool stop = false;
                //tant qu'une place libre n'est pas trouve
                int loop = 0;
                while(!stop)
                {
                    w = randint(minSize,maxSize);
                    h = randint(minSize,maxSize);
                    x = randint(1+2,GRID_W-1-w-2);
                    y = randint(1+2,GRID_H-1-h-2);
                    bool isVoid = true;
                    for(int i=x; i<x+w; i++){
                        for(int j=y; j<y+h; j++){
                            isVoid = isVoid && grid[i,j] == Case.VOID;
                        }
                    }
                    stop = isVoid;
                    loop++;
                    // si 1000 echec une erreur est levé
                    if(loop>1000)
                    {
                        return 1;
                    }
                }

                Room room = new Room(x,y,w,h);
                rooms.Add(room);

                //met a jour la grid
                for(int i=x-1; i<x+w+1; i++){
                    for(int j=y-1; j<y+h+1; j++){
                        if(grid[i,j]!=Case.CORNER){
                            if((i==x-1 || i==x+w) || (j==y-1 || j==y+h)){
                                grid[i,j]=Case.WALL;
                            }else{
                                grid[i,j]=Case.ROOM;
                            }
                        }
                    }
                }
                grid[x-1,y-1] = Case.CORNER;
                grid[x+w,y-1] = Case.CORNER;
                grid[x-1,y+h] = Case.CORNER;
                grid[x+w,y+h] = Case.CORNER;
            }

            return 0;
        }
        private int createDoors(int nbDoor)
        {
            for(int i=0;i<rooms.Count;i++)
            {
                for(int j=0;j<nbDoor;j++)
                {
                    int x=0,y=0;
                    Dir dir = Dir.HORI;
                    for(bool stop=false;!stop;)
                    {
                        int cote = randint(1,4);
                        x=0;
                        y=0;
                        switch(cote)
                        {
                        case 1:
                            x = rooms[i].x-1;
                            y = randint(rooms[i].y,rooms[i].y+rooms[i].h-2);
                            break;
                        case 2:
                            x = randint(rooms[i].x,rooms[i].x+rooms[i].w-2);
                            y = rooms[i].y-1;
                            dir = Dir.VERT;
                            break;
                        case 3:
                            x = rooms[i].x+rooms[i].w;
                            y = randint(rooms[i].y,rooms[i].y+rooms[i].h-2);
                            dir = Dir.HORI;
                            break;
                        case 4:
                            x = randint(rooms[i].x,rooms[i].x+rooms[i].w-2);
                            y = rooms[i].y+rooms[i].h;
                            dir = Dir.VERT;
                            break;
                        }
                        stop = grid[x,y] != Case.DOOR && grid[x,y] != Case.CORNER;
                    }
                    grid[x,y] = Case.DOOR;
                    Door door = new Door(x,y,dir,rooms[i]);
                    doors.Add(door);
                }
            }
            return 0;
        }
        private int cleanDoorWall()
        {
            for(int i=0, max=doors.Count; i<max;i++)
            {
                Door door = doors[0];
                doors.RemoveAt(0);
                Door nDoor;
                bool toRemove = false;
                if(door.dir == Dir.HORI)
                {
                    if(door.x-1>0){
                        if(grid[door.x-1,door.y] == Case.WALL){//Si gauche porte mur
                            grid[door.x-1,door.y] = Case.DOOR;
                            Room Parent = getRoomByCoor(door.x-1,door.y);
                            if(Parent.Equals(nullRoom)) return 2;
                            nDoor = new Door(door.x-1,door.y,Dir.HORI,Parent);
                            door.link.Add(nDoor);
                            nDoor.link.Add(door);
                            
                            toRemove = true;
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                        }else if(grid[door.x-1,door.y] == Case.CORNER){
                            if(grid[door.x,door.y+1] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.y+=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else if(grid[door.x,door.y-1] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.y-=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            } else
                                toRemove = true;
                        }else if(grid[door.x-1,door.y] == Case.DOOR){
                            nDoor = popDoorByCoor(door.x-1,door.y);
                            if(nDoor.Equals(nullDoor)) return 1;
                            nDoor.link.Add(door);
                            door.link.Add(nDoor);
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            toRemove=true;
                        }
                        
                    }else if (door.x+1 < GRID_W){//Si droite porte mur 
                        if (grid[door.x+1,door.y]==Case.WALL){
                            grid[door.x+1,door.y] = Case.DOOR;
                            Room parent = getRoomByCoor(door.x+1,door.y);
                            if(parent.Equals(nullRoom)) return 2;
                            nDoor = new Door(door.x+1,door.y,Dir.HORI,parent);
                            door.link.Add(nDoor);
                            nDoor.link.Add(door);
                            toRemove = true;
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            // # print("CAS 2.0")
                        }else if(grid[door.x+1,door.y]==Case.CORNER){
                            if(grid[door.x,door.y-1] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.y-=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else if(grid[door.x,door.y+1] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.y+=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else
                                toRemove = true;
                            // # print("CAS 2.1")
                        }else if(grid[door.x+1,door.y] == Case.DOOR){
                            nDoor = popDoorByCoor(door.x+1,door.y);
                            if(nDoor.Equals(nullDoor)) return 1;
                            nDoor.link.Add(door);
                            door.link.Add(nDoor);
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            toRemove=true;
                        }

                    }else if (  door.x-1 >= 0 && 
                                door.x+1 < GRID_W && 
                                grid[door.x-1,door.y]==Case.ROOM && 
                                grid[door.x+1,door.y]==Case.ROOM){// si porte traversse deux mur
                        Room Parent = getRoomByCoor(door.x,door.y, new Room[]{door.room});
                        if (Parent.Equals(nullRoom)) return 2;
                        nDoor = new Door(door.x,door.y,Dir.HORI,Parent);
                        door.link.Add(nDoor);
                        nDoor.link.Add(door);
                        toRemove = true;
                        doorsWall.Add(door);
                        doorsWall.Add(nDoor);
                        // # print("CAS 3.0")
                    }
                }
                if (door.dir == Dir.VERT){
                    if(door.y-1>0){//#Si haut porte mur
                        if(grid[door.x,door.y-1] == Case.WALL){
                            grid[door.x,door.y-1] = Case.DOOR;
                            Room parent = getRoomByCoor(door.x,door.y-1);
                            if(parent.Equals(nullRoom)) return 2;
                            nDoor = new Door(door.x,door.y-1,Dir.VERT,parent);
                            door.link.Add(nDoor);
                            nDoor.link.Add(door);
                            toRemove = true;
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            // # print("CAS 4.0")
                        }else if(grid[door.x,door.y-1] == Case.CORNER){
                            if(grid[door.x+1,door.y] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.x+=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else if(grid[door.x-1,door.y] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.x-=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            } else
                                toRemove=true;
                            // # print("CAS 4.1")
                        } else if(grid[door.x,door.y-1] == Case.DOOR){
                            nDoor = popDoorByCoor(door.x,door.y-1);
                            if(nDoor.Equals(nullDoor))return 1;
                            nDoor.link.Add(door);
                            door.link.Add(nDoor);
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            toRemove=true;
                        }

                    }else if (door.y+1<GRID_H){//#Si droite porte mur
                        if(grid[door.x,door.y+1]==Case.WALL){
                            grid[door.x,door.y+1] = Case.DOOR;
                            Room parent = getRoomByCoor(door.x,door.y+1);
                            if(parent.Equals(nullRoom)) return 2;
                            nDoor = new Door(door.x,door.y+1,Dir.VERT,parent);
                            door.link.Add(nDoor);
                            nDoor.link.Add(door);
                            toRemove = true;
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            // # print("CAS 5.0")
                        } else if(grid[door.x,door.y+1]==Case.CORNER){
                            if(grid[door.x+1,door.y] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.x+=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else if(grid[door.x-1,door.y] == Case.WALL){
                                grid[door.x,door.y]=Case.WALL;
                                door.x-=1;
                                grid[door.x,door.y]=Case.DOOR;
                                doors.Insert(0,door);
                                max++;
                            }else
                                toRemove=true;
                            // # print("CAS 5.1")
                            
                        }else if(grid[door.x,door.y+1] == Case.DOOR){
                            nDoor = popDoorByCoor(door.x,door.y+1);
                            if(nDoor.Equals(nullDoor)) return 1;
                            nDoor.link.Add(door);
                            door.link.Add(nDoor);
                            doorsWall.Add(door);
                            doorsWall.Add(nDoor);
                            toRemove=true;
                        }
                    }else if (door.y-1 >= 0 && 
                            door.y+1 < GRID_H && 
                            grid[door.x,door.y-1]==Case.ROOM && 
                            grid[door.x,door.y+1]==Case.ROOM){//# si porte traversse deux mur
                        Room parent = getRoomByCoor(door.x,door.y,new Room[]{door.room});
                        if(parent.Equals(nullRoom)) return 2;
                        nDoor = new Door(door.x,door.y,Dir.VERT, parent);
                        door.link.Add(nDoor);
                        nDoor.link.Add(door);
                        toRemove = true;
                        doorsWall.Add(door);
                        doorsWall.Add(nDoor);
                    }
                    if(!toRemove){
                        doors.Add(door);
                    }
                }
            }
            foreach(Door door in doorsWall)
            {
                doors.Remove(door);
            }
            return 0;
        }
        
        private int buildCorridors()
        {
            if(doors.Count == 1)
            {
                Door door = doors[0];doors.RemoveAt(0);
                grid[door.x,door.y] = Case.WALL;
                return 0;
            }
            int[,] grid4path = new int[GRID_W,GRID_H];
            for(int x=0;x<GRID_W;x++){
                for(int y=0;y<GRID_H;y++){
                    if(grid[x,y] == Case.VOID)
                        grid4path[x,y] = 0;
                    else
                        grid4path[x,y] = -1;
                }
            }
            int loop = 0;
            while(doors.Count > 1)
            {
                Door door = doors[0];doors.RemoveAt(0);
                Door door2link = nullDoor;
                bool stop = false;
                int index = -1;
                while(!stop){//verifie que les deux porte soit d'une salle differente
                    index = randint(0,doors.Count+doorsLinked.Count-1);
                    
                    if(index >= doors.Count){
                        door2link = doorsLinked[index-doors.Count];
                        doorsLinked.RemoveAt(index);
                    }else{
                        door2link = doors[index];
                        doors.RemoveAt(index);
                    }
                    stop = !(door2link.Equals(door) || (door2link.x-door.x)*(door2link.x-door.x) + (door2link.y-door.y)*(door2link.y-door.y)==1);
                }
                if(door.x == door2link.x && door.y == door2link.y || door.Equals(door2link))
                    doors.Add(door);
                else
                {
                    List<Elem> road = corridor(door.x,door.y,door.dir,door2link.x,door2link.y,grid4path);
                    if (road.Count == 0)
                        return 1;
                    door.link.Add(door2link);
                    door2link.link.Add(door);
                    doorsLinked.Add(door);
                    doorsLinked.Add(door2link);
                    foreach(Elem elem in road)
                    {
                        grid[elem.x,elem.y] = Case.CORRIDOR; 
                        Console.Write("({0},{1}) ",elem.x,elem.y);
                    }Console.Write("\n");
                }
            }
            if(doors.Count == 1)
            {
                Door door = doors[0];doors.RemoveAt(0);
                int i = randint(0,doorsLinked.Count-1);
                List<Elem> road = corridor(door.x,door.y,door.dir,doorsLinked[i].x,doorsLinked[i].y,grid4path);
                if (road.Count == 0)
                    return 1;
                doorsLinked[i].link.Add(door);
                door.link.Add(doorsLinked[i]);
                doorsLinked.Add(door);
                foreach(Elem elem in road){
                    grid[elem.x,elem.y] = Case.CORRIDOR; 
                }
            }
            return 0;
        }

        public GenerationMap(int GRID_W_, int GRID_H_, int nbRoom, int minSizeRoom, int maxSizeRoom, int minNbDoor, int maxNbDoor)
        {
            
            GRID_W = GRID_W_;
            GRID_H = GRID_H_;

            nullRoom = new Room(GRID_W,GRID_H,0,0);
            nullDoor = new Door(GRID_W,GRID_H,Dir.HORI,nullRoom);

            rooms = new List<Room>();
            doors = new List<Door>();
            doorsWall = new List<Door>();
            doorsLinked = new List<Door>();
            grid = new Case[GRID_W,GRID_H];
            for(int x=0;x<GRID_W;x++){
                for(int y=0;y<GRID_H;y++){
                    grid[x,y] = Case.VOID;
                }
            }
            if(createRooms(nbRoom, minSizeRoom, maxSizeRoom)!=0)
            {
                Console.WriteLine("ERROR CREATE ROOMS");
            }
            if(createDoors(randint(minNbDoor,maxNbDoor))!=0)
            {
                Console.WriteLine("ERROR CREATE DOORS");
            }
            Console.WriteLine("len doors {0}",doors.Count);
            // switch(cleanDoorWall()) {
            //     case 1:
            //         Console.WriteLine("ERROR CLEAN NULL DOOR");
            //         break;
            //     case 2:
            //         Console.WriteLine("ERROR CLEAN NULL ROOM");
            //         break;
            // }
            // printGrid();
            // Console.WriteLine("len doors {0}",doors.Count);
            // if(buildCorridors()!=0)
            // {
            //     Console.WriteLine("ERROR CREATE CORRIDORS");
            // }
            // Console.WriteLine("len doors {0}",doors.Count);
        }
    };
}