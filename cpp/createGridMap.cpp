#include <iostream>
#include <list>
#include <cstdlib>

using namespace std;

enum Case {
    VOID, ROOM, CORRIDOR, WALL, CORNER, DOOR
};
enum Dir {
    HORI,VERT
};
typedef struct s_door Door;
typedef struct s_room Room;

int GRID_W = 20;
int GRID_H = 20;

struct s_door
{
    s_door(int x_=0, int y_=0, Room* parent_=nullptr)
    {
        init(x_,y_,parent_);
    }

    void init(int x_=0, int y_=0, Room* parent_=nullptr)
    {
        x = x_;
        y = y_;
        parent = parent_;
        id = reinterpret_cast<uint64_t>(this);
    }
    int x,y;
    list<Door*> link;
    Room* parent;
    uint64_t id;
};
struct s_room
{
    s_room(int x_=0, int y_=0, int w_=0, int h_=0)
    {
        init(x_,y_,w_,h_);
    }
    void init(int x_=0, int y_=0, int w_=0, int h_=0)
    {
        x = x_;
        y = y_;
        w = w_;
        h = h_;
        id = reinterpret_cast<uint64_t>(this);
    }
    int x, y, w, h; 
    uint64_t id;
    list<Door*> doors;
};
int m(int x, int y)
{
    return x + GRID_W*y;
}
int randint(int min, int max)
{
    return rand()%(max-min) + min;
}
void printGrid(Case*grid)
{
    cout << "   ";
    for(int x=0;x<GRID_W;x++)
    {
        cout << x%10 << "   ";
    }
    cout << endl;
    for(int y=0; y<GRID_H;y++)
    {
        cout << y%10 << "   ";
        for(int x=0; x<GRID_W; x++)
        {
            Case val = grid[m(x,y)];
            if(val == Case::VOID){
                cout << " " << "   ";
            }else if (val == Case::ROOM){
                cout << "." << "   ";
            }else if (val == Case::WALL){
                cout << "2" << "   ";
            }else if (val == Case::CORRIDOR){
                cout << "*" << "   ";
            }else if (val == Case::DOOR){
                cout << "p" << "   ";
            }else if (val == Case::CORNER){
                cout << "4" << "   ";
            }
        }
        cout << endl;
    }
}
Case* initGrid()
{
    Case* grid = (Case *)malloc(GRID_W * GRID_H * sizeof(Case));
    
    for(int x=0; x<GRID_W; x++){
        for(int y=0; y<GRID_H; y++){
            grid[m(x,y)] = Case::VOID;
        }
    }
    return grid;
}
int creatRooms(Case* grid, list<Room*> *rooms, int nbRoom, int minSize, int maxSize)
{
    // autant de boucle que de salle voulu
    for(int e=0; e<nbRoom; e++){
        int x,y,w,h = 0;
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
                    isVoid = isVoid && grid[m(i,j)] == Case::VOID;
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

        Room* room = (Room*)malloc(sizeof(Room));
        room->init(x,y,w,h);
        // room->x = x;
        rooms->push_back(room);

        //met a jour la grid
        for(int i=x-1; i<x+w+1; i++){
            for(int j=y-1; j<y+h+1; j++){
                if(grid[m(i,j)]!=CORNER){
                    if((i==x-1 || i==x+w) || (j==y-1 || j==y+h)){
                        grid[m(i,j)]=WALL;
                    }else{
                        grid[m(i,j)]=ROOM;
                    }
                }
            }
        }
        grid[m(x-1,y-1)] = CORNER;
        grid[m(x+w,y-1)] = CORNER;
        grid[m(x-1,y+h)] = CORNER;
        grid[m(x+w,y+h)] = CORNER;
    }

    return 0;
}
int creatRooms(Case* grid, list<Room*> *rooms, list<Door*> *doors, int nbDoor)
{
    for(Room* room : rooms)
    {
        for(int i=0;i<nbDoor;i++)
        {
            for(bool stop=false; !stop;)
            {
                int cote = randint(1,4);//defini sur quel mur va etre la porte
                int x,y = 0;
                Dir dir;
                switch (cote)
                {
                case 1:
                    x = room->x-1;
                    y = randint(room->y,room->y+room.h-2)
                    dir = HORI;
                    break;
                case 2:
                    
                default:
                    break;
                }
            }
        }
    }
    return 0;
}

int main()
{
    Case* grid = initGrid();
    list<Room*> rooms;
    list<Door*> doors;

    creatRooms(grid,&rooms,3,2,5);
    
    printGrid(grid);
    free(grid);
    for(auto elem : rooms){
        free(elem);
    }
    for(auto elem : doors){
        free(elem);
    }
    return 0;
}