using System;
using System.Drawing;

namespace GenerationMap
{
    
    enum Dir {
        HORI,VERT
    };  

      
    class GenerationMap
    {
        public int GRID_W = 20;
        public int GRID_H = 20;
        public Case[,] grid;
        public Case[,] grid2;
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
                    }else if (val == Case.DOOR || val == Case.DOOR_H || val == Case.DOOR_V){
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
                    if(rooms[i].x<=0 || rooms[i].x >= GRID_W || rooms[i].y<=0 || rooms[i].y>=GRID_H)
                        Console.WriteLine("Room data : X1{0} Y1{1} X2{2} Y2{3}",rooms[i].x-1,rooms[i].x+rooms[i].w,rooms[i].y-1,rooms[i].y+rooms[i].h);
                    if(x>=rooms[i].x-1 && x<=rooms[i].x+rooms[i].w && y>=rooms[i].y-1 && y<=rooms[i].y+rooms[i].h)
                        return rooms[i];
                    // if ((x==rooms[i].x-1 || x==rooms[i].x+rooms[i].w) && (y>rooms[i].y-1 || y < rooms[i].y+rooms[i].h)){
                    //     return rooms[i];
                    // }else if((y==rooms[i].y-1 || y==rooms[i].y+rooms[i].h) && (x>rooms[i].x-1 || x<rooms[i].x+rooms[i].w)){
                    //     return rooms[i];
                    // }
                }
            }
            grid[x,y] = Case.ERROR;
            Console.WriteLine("X{0} Y{1}",x,y);
            Console.WriteLine("Room data : X{0} Y{1} W{2} H{3}",toNotCheck[0].x,toNotCheck[0].y,toNotCheck[0].w,toNotCheck[0].h);
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
        private void initGrid4path(int[,] grid4path, params Case[] cases){
            for(int x=0; x<GRID_W; x++){
                for(int y=0; y<GRID_H;y++){
                    if (cases.Contains(grid[x,y])){
                        grid4path[x,y] = 0;
                    }else{
                        grid4path[x,y] = -1;
                    }
                }
            }
        }
        private List<Elem> corridorV2(int sx, int sy, Dir dir, int tx, int ty, int[,] grid)
        {
            Elem[,] elemGrid = new Elem[GRID_W,GRID_H];
            for(int x=0;x<GRID_W;x++){
                for(int y=0;y<GRID_H;y++){
                    elemGrid[x,y] = new Elem(x,y);
                }
            }
            int startState = grid[sx,sy];
            int targetState = grid[tx,ty];
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
                                int costRoad = 1;
                                if (this.grid[nx,ny] != Case.CORRIDOR)
                                    costRoad = 2;
                                if(elemGrid[nx,ny].p > current.p+costRoad)// nouveau chemin plus interesant
                                {
                                    elemGrid[nx,ny].p = current.p+costRoad;
                                    elemGrid[nx,ny].parent = current;
                                    elemGrid[nx,ny].dir = current.dir;
                                    toSee.Add(elemGrid[nx,ny]);
                                }
                            }else {
                                int costRoad = 1;
                                if (this.grid[nx,ny] != Case.CORRIDOR)
                                    costRoad = 4;
                                if(elemGrid[nx,ny].p > current.p+costRoad)// nouveau chemin plus interesant
                                {
                                    
                                    elemGrid[nx,ny].p = current.p+costRoad;
                                    elemGrid[nx,ny].parent = current;
                                    elemGrid[nx,ny].dir = changerDir(current.dir);
                                    toSee.Add(elemGrid[nx,ny]);
                                    
                                }
                            }
                        }else{//} voisin non visité
                            if(current.dir == Dir.HORI && driftY[i] == 0 || current.dir == Dir.VERT && driftX[i] == 0)// pas de changment de direction
                            {
                                int costRoad = 1;
                                if (this.grid[nx,ny] != Case.CORRIDOR)
                                    costRoad = 2;
                                elemGrid[nx,ny].p = current.p+costRoad;
                                elemGrid[nx,ny].dir = current.dir;
                            }else{
                                int costRoad = 1;
                                if (this.grid[nx,ny] != Case.CORRIDOR)
                                    costRoad = 4;
                                elemGrid[nx,ny].p = current.p+costRoad;
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
                // Console.WriteLine("coor {0} {1} start {2} {3} target {4} {5}",nx,ny,sx,sy,tx,ty);
                while (elemGrid[nx,ny].parent!=null && !(elemGrid[nx,ny].parent.x == sx && elemGrid[nx,ny].parent.y == sy))
                {
                    road.Add(elemGrid[nx,ny].parent);
                    int a = elemGrid[nx,ny].parent.x;
                    ny = elemGrid[nx,ny].parent.y;
                    nx = a;
                    loop +=1;
                    if (loop>1000 || elemGrid[nx,ny].parent == null)
                        return new List<Elem>();

                }
            }

            
            
            grid[sx,sy] = startState;
            grid[tx,ty] = targetState;

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
                            dir = Dir.HORI;
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
                    rooms[i].doors.Add(door);
                    doors.Add(door);
                }
            }
            return 0;
        }
        private int cleanDoorWallV2()
        {
            // List<Door> doorsTriee = new List<Door>();
            int countLoop = doors.Count;
            for(int i=0; i<countLoop; i++){
                Door door = doors[0];doors.Remove(door);
                Point[] drifts = (door.dir == Dir.HORI)? new Point[]{new Point(-1,0),new Point(1,0)}:new Point[]{new Point(0,-1),new Point(0,1)};
                Door nDoor;
                bool stop = false;
                foreach(Point drift in drifts){
                    if(door.x+drift.x>0 && door.x+drift.x < GRID_W && door.y+drift.y>0 && door.y+drift.y<GRID_H && !stop)
                    {   
                        switch(grid[door.x+drift.x,door.y+drift.y]){
                            case Case.WALL:
                                Room parent = getRoomByCoor(door.x+drift.x,door.y+drift.y);
                                if(parent.Equals(nullRoom)) return 2;
                                grid[door.x+drift.x,door.y+drift.y] = Case.ROOM;
                                nDoor = new Door(door.x,door.y,door.dir,parent);
                                parent.doors.Add(nDoor);
                                door.link.Add(nDoor);
                                nDoor.link.Add(door);
                                doorsWall.Add(door);
                                doorsWall.Add(nDoor);
                                stop = true;
                                break;
                            case Case.CORNER:
                                Point drift2 = (door.dir == Dir.HORI)? new Point(0,1): new Point(1,0);
                                if(grid[door.x-drift2.x,door.y-drift2.y] == Case.WALL){
                                    grid[door.x,door.y]=Case.WALL;
                                    door.x-=drift2.x;
                                    door.y-=drift2.y;
                                    grid[door.x,door.y]=Case.DOOR;
                                    doors.Remove(door);
                                    doors.Insert(0,door);
                                    stop = true;
                                    countLoop++;
                                }else if(grid[door.x+drift2.x,door.y+drift2.y] == Case.WALL){
                                    grid[door.x,door.y]=Case.WALL;
                                    door.x+=drift2.x;
                                    door.y+=drift2.y;
                                    grid[door.x,door.y]=Case.DOOR;
                                    doors.Remove(door);
                                    doors.Insert(0,door);
                                    countLoop++;
                                    stop=true;
                                }
                                break;
                            case Case.DOOR:
                                nDoor = popDoorByCoor(door.x+drift.x,door.y+drift.y);
                                if (nDoor.Equals(nullDoor)) return 1;
                                grid[door.x,door.y] = Case.ROOM;
                                door.x+=drift.x;
                                door.y+=drift.y;
                                nDoor.link.Add(door);
                                door.link.Add(nDoor);
                                doorsWall.Add(door);
                                doorsWall.Add(nDoor);
                                stop = true;
                                break;
                        }
                    }
                }
                if(!stop && grid[door.x+drifts[0].x,door.y+drifts[0].y]==Case.ROOM && grid[door.x+drifts[1].x,door.y+drifts[1].y]==Case.ROOM)
                {
                    // Console.WriteLine("RxR {0} {1}",door.x,door.y);
                    Room Parent = getRoomByCoor(door.x, door.y, new Room[]{door.room});
                    if(Parent.Equals(nullRoom)) return 2;
                    nDoor = new Door(door.x,door.y,door.dir,Parent);
                    Parent.doors.Add(nDoor);
                    door.link.Add(nDoor);
                    nDoor.link.Add(door);
                    doorsWall.Add(door);
                    doorsWall.Add(nDoor);
                    stop = true;
                }
                if (!stop)
                    doors.Add(door);
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
            initGrid4path(grid4path,Case.VOID,Case.CORRIDOR);
            // for(int x=0;x<GRID_W;x++){
            //     for(int y=0;y<GRID_H;y++){
            //         if(grid[x,y] == Case.VOID || grid[x,y] == Case.CORRIDOR)
            //             grid4path[x,y] = 0;
            //         else
            //             grid4path[x,y] = -1;
            //     }
            // }
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
                        doorsLinked.RemoveAt(index-doors.Count);
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
                    List<Elem> road = corridorV2(door.x,door.y,door.dir,door2link.x,door2link.y,grid4path);
                    if (road.Count == 0){
                        Console.WriteLine("start {0} {1} target {2} {3}",door.x,door.y,door2link.x,door2link.y);
                        grid[door.x,door.y] = Case.ERROR;
                        grid[door2link.x,door2link.y] = Case.ERROR;
                        return 1;}
                    door.link.Add(door2link);
                    door2link.link.Add(door);
                    doorsLinked.Add(door);
                    doorsLinked.Add(door2link);
                    foreach(Elem elem in road)
                    {
                        grid[elem.x,elem.y] = Case.CORRIDOR; 
                        // Console.Write("({0},{1}) ",elem.x,elem.y);
                    }// Console.Write("\n");
                }
            }
            if(doors.Count == 1)
            {
                Door door = doors[0];doors.RemoveAt(0);
                int i = randint(0,doorsLinked.Count-1);
                List<Elem> road = corridorV2(door.x,door.y,door.dir,doorsLinked[i].x,doorsLinked[i].y,grid4path);
                if (road.Count == 0){
                    Console.WriteLine("start {0} {1} target {2} {3}",door.x,door.y,doorsLinked[i].x,doorsLinked[i].y);
                    grid[door.x,door.y] = Case.ERROR;
                    grid[doorsLinked[i].x,doorsLinked[i].y] = Case.ERROR;
                    return 1;
                }
                doorsLinked[i].link.Add(door);
                door.link.Add(doorsLinked[i]);
                doorsLinked.Add(door);
                foreach(Elem elem in road){
                    grid[elem.x,elem.y] = Case.CORRIDOR; 
                }
            }
            return 0;
        }
        private int blockUnique(){
            if(doorsLinked.Count == 0)
                return 3;
            Door startDoor = doorsLinked[randint(0,doorsLinked.Count-1)];doorsLinked.Remove(startDoor);
            List<Room> roomAcces = new List<Room>(); roomAcces.Add(startDoor.room);
            List<Door> doorSee = new List<Door>(); doorSee.Add(startDoor);
            List<Door> door2See = new List<Door>();
            foreach(Door door in startDoor.room.doors){door2See.Remove(door); door2See.Add(door);}
            foreach(Door door in startDoor.link){door2See.Remove(door); door2See.Add(door);}
            foreach(Door door in doorSee){door2See.Remove(door);}
            int loop = 0;
            while(door2See.Count>0)
            {
                Door currentDoor = door2See[0]; door2See.RemoveAt(0);
                if(!roomAcces.Contains(currentDoor.room))
                    roomAcces.Add(currentDoor.room);
                foreach(Door door in currentDoor.room.doors){door2See.Remove(door); door2See.Add(door);}
                foreach(Door door in currentDoor.link){door2See.Remove(door); door2See.Add(door);}
                doorSee.Add(currentDoor);
                foreach(Door door in doorSee){door2See.Remove(door);}
                loop++;
                if (loop > 1000){
                    return 1;
                }
            }
            if(rooms.Count > roomAcces.Count){
                Room roomOut = nullRoom;
                foreach(Room room in rooms){
                    if(!roomAcces.Contains(room)){
                        roomOut = room;
                    }
                }
                if(!roomOut.Equals(nullRoom)){
                    Door door2link = roomOut.doors[0];
                    int[,] grid4path = new int[GRID_W,GRID_H];
                    initGrid4path(grid4path,Case.VOID,Case.CORRIDOR);
                    // for(int x=0;x<GRID_W;x++){for(int y=0;y<GRID_H;y++){
                    //         if(grid[x,y] == Case.VOID || grid[x,y] == Case.CORRIDOR)
                    //             grid4path[x,y] = 0;
                    //         else
                    //             grid4path[x,y] = -1;
                    // }   }
                    List<Elem> road = corridorV2(startDoor.x,startDoor.y,startDoor.dir,door2link.x,door2link.y,grid4path);
                    if (road.Count == 0){
                        // Console.WriteLine("start {0} {1} target {2} {3}",startDoor.x,startDoor.y,door2link.x,door2link.y);
                        return 2;}
                    startDoor.link.Add(door2link);
                    door2link.link.Add(startDoor);
                    doorsLinked.Remove(startDoor);doorsLinked.Add(startDoor);
                    doorsLinked.Remove(door2link);doorsLinked.Add(door2link);
                    foreach(Elem elem in road){
                        grid[elem.x,elem.y] = Case.CORRIDOR; 
                    }
                    // Console.WriteLine("Union Group");
                    return blockUnique();
                }
            }

            return 0;
        }
        private int blockUniqueV2(){
            // creation grille 
            int[,] grid4path = new int[GRID_W,GRID_H];
            initGrid4path(grid4path,Case.CORRIDOR,Case.ROOM, Case.DOOR, Case.DOOR_H, Case.DOOR_V);
            for(int x=0; x<GRID_W; x++){ // permet de parcourir les couloirs, pieces et portes
                for(int y=0; y<GRID_H; y++){
                    if(grid[x,y] == Case.CORRIDOR || grid[x,y] == Case.ROOM || grid[x,y] == Case.DOOR || grid[x,y] == Case.DOOR_H || grid[x,y] == Case.DOOR_V )
                    {
                        grid4path[x,y] = 0;
                    }else{
                        grid4path[x,y] = -1;
                    }
                }
            }
            // detection d'erreur
            Door door = doorsLinked[0];
            List<Door> linked = new List<Door>();
            List<Door> notLinked = new List<Door>();
            List<Elem> road;
            foreach(Door door2link in doorsLinked){
                if(!door.Equals(door2link)){
                    road = corridorV2(door.x,door.y,door.dir,door2link.x,door2link.y,grid4path);
                    if(road.Count==0){
                        notLinked.Add(door2link);
                    }else{linked.Add(door2link);}
                }else{linked.Add(door2link);}
            }foreach(Door door2link in doorsWall){
                road = corridorV2(door.x,door.y,door.dir,door2link.x,door2link.y,grid4path);
                if(road.Count==0){
                    notLinked.Add(door2link);
                }else{linked.Add(door2link);}
            }
            // Console.WriteLine("{0} {1} {2}", linked.Count, doorsLinked.Count, doorsWall.Count);
            if(linked.Count != doorsLinked.Count+doorsWall.Count){
                // correction d'erreur
                foreach(Door door2link in notLinked){
                    if (doorsLinked.Contains(door2link)){
                        // cree une nouveau couloire
                        Console.WriteLine("Erreur en cours de correction");
                        initGrid4path(grid4path,Case.VOID,Case.CORRIDOR);
                        road = corridorV2(door.x,door.y,door.dir,door2link.x,door2link.y,grid4path);
                        if (road.Count==0)
                            return 2; // erreur generation couloire
                        foreach(Elem elem in road){
                            if(!((elem.x == door.x && elem.y == door.y) || (elem.x==door2link.x && elem.y==door2link.y))){
                                grid[elem.x,elem.y] = Case.CORRIDOR;
                            }
                        }
                        // return rappelle blockuniqueV2
                        return blockUniqueV2();
                        
                    }
                }// si non trouvée, liaison impossible
                return 1;
            }
            
            return 0;
        }
        private int closeWall()
        {
            for(int x=0; x<GRID_W; x++){
                for(int y=0; y<GRID_H; y++){
                    if(grid[x,y]!=Case.VOID && grid[x,y]!=Case.WALL && grid[x,y]!=Case.CORNER){
                        for(int shiftx=x-1; shiftx<x+2; shiftx++){
                            for(int shifty=y-1; shifty<y+2; shifty++){
                                if(shiftx>=0 && shiftx<GRID_W && shifty>=0 && shifty<GRID_H && grid[shiftx,shifty] == Case.VOID){
                                    grid[shiftx,shifty] = Case.WALL;
                                }
                            }
                        }
                    }
                }
            }
            return 0;
        }
        public void createPng(string name, int ratio=1){
            Bitmap bmp = new Bitmap(GRID_W*ratio,GRID_H*ratio);
            // Graphics g = Graphics.FromImage(bmp);
            for(int x=0; x<GRID_W; x++){
                for(int y=0; y<GRID_H; y++){
                    Color color = new Color();
                    switch(grid[x,y]){
                        case Case.VOID:
                            color = Color.White;
                            break;
                        case Case.CORNER:
                            color = Color.FromArgb(51,51,51);
                            break;
                        case Case.WALL:
                            color = Color.FromArgb(51,51,51);
                            break;
                        case Case.ROOM:
                            color = Color.FromArgb(255,133,51);
                            break;
                        case Case.CORRIDOR:
                            color = Color.FromArgb(255,255,102);
                            break;
                        case Case.DOOR:
                            color = Color.FromArgb(102,51,0);
                            break;
                        default:
                            color = Color.FromArgb(0,0,0);
                            break;
                    }
                    for(int dx=0; dx<ratio; dx++){
                        for(int dy=0; dy<ratio; dy++){
                            bmp.SetPixel(x*ratio+dx,y*ratio+dy,color);
                        }
                    }
                    
                }
            }
            bmp.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
        }
        public void createPngV2(string name, Case[,] myGrid, int ratio = 1){
            Bitmap bmp = new Bitmap(GRID_W*ratio,GRID_H*ratio);
            // Graphics g = Graphics.FromImage(bmp);
            for(int x=0; x<GRID_W; x++){
                for(int y=0; y<GRID_H; y++){
                    Color color = new Color();
                     
                    if( myGrid[x,y] == Case.ROOM || myGrid[x,y] == Case.CORRIDOR){
                        color = Color.White;
                    }else if(myGrid[x,y] == Case.DOOR || myGrid[x,y] == Case.DOOR_H || myGrid[x,y] == Case.DOOR_V){
                        color = Color.FromArgb(102,51,0);
                    }else if(myGrid[x,y] == Case.NULL){
                        color = Color.FromArgb(0,0,0);
                    }else if(myGrid[x,y] == Case.ERROR){
                        color = Color.Red;
                    }else if(myGrid[x,y] == Case.CORNER){
                        color = Color.Green;
                    }else if(myGrid[x,y] != Case.VOID) {
                        color = Color.Blue;
                    }
                    for(int dx=0; dx<ratio; dx++){
                        for(int dy=0; dy<ratio; dy++){
                            bmp.SetPixel(x*ratio+dx,y*ratio+dy,color);
                        }
                    }
                    
                }
            }
            bmp.Save(name, System.Drawing.Imaging.ImageFormat.Png);
            bmp.Dispose();
        }
        public Case[,] chageCaseForTilte(){
            Case[,] newGrid = new Case[GRID_W,GRID_H];
            sortCaseList cases = new sortCaseList();
            for(int x=0; x<GRID_W; x++){
                for(int y=0; y<GRID_H; y++){
                    Case up = (y>0)? grid[x,y-1]: Case.VOID;
                    Case down = (y<GRID_H-1)? grid[x,y+1]: Case.VOID;
                    Case left = (x>0)? grid[x-1,y]: Case.VOID;
                    Case right = (x<GRID_W-1)? grid[x+1,y]: Case.VOID;
                    Case upRight = (y>0 && x<GRID_W-1)? grid[x+1,y-1]: Case.VOID;
                    Case upLeft = (y>0 && x>0)? grid[x-1,y-1]: Case.VOID;
                    Case downRight = (y<GRID_H-1 && x<GRID_W-1)? grid[x+1,y+1]: Case.VOID;
                    Case downLeft = (y<GRID_H-1 && x>0)? grid[x-1,y+1]: Case.VOID;

                    List<sortCase> possible = new List<sortCase>();
                    if (grid[x,y] == Case.WALL || grid[x,y] == Case.CORNER){
                        foreach(sortCase elem in cases.listCase){
                            if( elem.up.Contains(up) && elem.down.Contains(down) && elem.right.Contains(right) && elem.left.Contains(left) && 
                                elem.upRight.Contains(upRight) && elem.upLeft.Contains(upLeft) && elem.downRight.Contains(downRight) && elem.DownLeft.Contains(downLeft))
                                possible.Add(elem);
                        }
                        if (possible.Count == 0){
                            newGrid[x,y] = Case.NULL;
                        }
                        else if (possible.Count > 1){
                            newGrid[x,y] = Case.ERROR;
                        } else {
                            newGrid[x,y] = possible[0].myCase;
                        }
                    }else{
                        newGrid[x,y] = grid[x,y];
                    }
                    
                }
            }
            
            return newGrid;
        }
        public GenerationMap(int GRID_W_, int GRID_H_, int nbRoom, int minSizeRoom, int maxSizeRoom, int minNbDoor, int maxNbDoor, int iter = 0)
        {
            bool stop = false;
            int err = 0;
            while(!stop){
                stop = true;
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
                    Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" CREATE ROOMS");

                    createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                    stop = false;
                    continue;
                }
                if(createDoors(randint(minNbDoor,maxNbDoor))!=0)
                {
                    Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" CREATE DOORS");
                    createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                    stop = false;
                    continue;
                }
                // printGrid();
                // Console.WriteLine("len doors {0}",doors.Count);
                switch(cleanDoorWallV2()) {
                    case 1:
                        Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" CLEAN NULL DOOR DD");
                        createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                        stop = false;
                        continue;
                    case 2:
                        Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" CLEAN NULL ROOM RxR");
                        createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                        stop = false;
                        continue;
                }
                
                // Console.WriteLine("len doors {0}",doors.Count);
                if(buildCorridors()!=0)
                {
                    Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" CREATE CORRIDORS");
                    createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                    stop = false;
                    continue;
                }
                // Console.WriteLine("len doors {0}",doors.Count);
                
                // Console.WriteLine("len doorsLinked {0}",doorsLinked.Count);
                // switch(blockUnique()) //ne marche pas
                // {
                //     case 1:
                //         Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK INFINIT LOOP");
                //         createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                //         // stop = false;
                //         break;
                //     case 2:
                //         Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK ERROR ROAD");
                //         createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                //         // stop = false;
                //         break;
                //     case 3:
                //         Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK LINKED LIST EMPTY");
                //         createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                //         // stop = false;
                //         break;
                // }
                switch(blockUniqueV2()){
                    case 1:
                        Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK 2 NO CONNECTION");
                        createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                        stop = false;
                        continue;
                    case 2:
                        Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK 2 ERROR ROAD");
                        createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                        stop = false;
                        continue;

                }
                // if(blockUniqueV2() != 0){
                //     Console.WriteLine("ERROR "+iter.ToString()+"-"+err.ToString()+" UNIFICATE BLOCK 2 NOT LINK");
                //     createPngV2("img/"+iter.ToString()+"-"+err++.ToString()+"error.png",grid,1);
                //     stop = false;
                //     continue;
                // }
                closeWall();
            }
            grid2 = chageCaseForTilte();
        }
    };
}