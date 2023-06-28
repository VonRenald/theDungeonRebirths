import random

GRID_W = 20
GRID_H = 25

HORIZON  = 1
VERTICAL = 2

lastDoorId = 0 

ROOM = 1
WALL = 2
CORRIDOR = 3
DOOR = 4


class Elem:
    def __init__(self,x,y,id,p=0,dir=HORIZON,visited=False,parentId=-1,px=0,py=0):
        self.x = x
        self.y = y
        self.id = id
        self.parentId = parentId
        self.p = p
        self.visited = visited
        self.dir = dir
        self.px = px
        self.py = py

class Room:
  def __init__(self, w, h, x, y, id):
    self.w = w
    self.h = h
    self.x = x
    self.y = y
    self.id = id
class Door:
    def __init__(self,x,y,alignement,id,idParent):
        self.x = x
        self.y = y
        self.id = id
        self.idParent = idParent
        self.alignement = alignement
        self.link = -1
def printGrille(grille):
    print("   ",end='')
    for x in range (0,GRID_W):
        print(x%10,end="  ")
    print()
    for y in range (0,GRID_H):
        print(y%10,end="  ")
        for x in range (0,GRID_W):
            val = grille[x][y]
            if(val < 2):
                print(" ",end="  ")
            elif(val == WALL):
                print("2",end="  ")
            elif(val == CORRIDOR):
                print("*",end="  ")
            elif(val == DOOR):
                print(" ",end="  ")
        print()
def printGrilleInt(grille):
    for y in range (0,GRID_H):
        for x in range (0,GRID_W):
            print(grille[x][y],end="  ")
        print()
def createRooms(grid, roomList, nbRoom, minS, maxS):
    for id in range (0,nbRoom):
        x=0
        y=0
        w=0
        h=0
        stop = False
        while not stop:
            w = random.randint(minS,maxS)
            h = random.randint(minS,maxS)
            x = random.randint(1+2,GRID_W-1-w-2)
            y = random.randint(1+2,GRID_H-1-h-2)
            stop = (grid[x][y] == 0 and grid[x+w][y] == 0 and grid[x][y+h] == 0 and grid[x+w][y+h] == 0)
        room = Room(w,h,x,y,id)
        roomList.append(room)
        for i in range (x-1,x+w+1):
            for j in range (y-1,y+h+1):
                if ((i==x-1 or i==x+w) or (j==y-1 or j==y+h)):
                    grid[i][j]=WALL
                else: 
                    grid[i][j]=ROOM
def createDoors(grid, roomList, doorList, minD, maxD):
    global lastDoorId
    id = 0
    for room in roomList:
        for i in range(0,random.randint(minD,maxD)):
            cote = random.randint(1,4)
            x=0
            y=0
            align = 0
            if cote==1: #gauche
                x = room.x-1
                y = random.randint(room.y,room.y+room.h-1)
                align = HORIZON
            elif cote==2: #haut
                x = random.randint(room.x,room.x+room.w-1)
                y = room.y-1
                align = VERTICAL
            elif cote==3: #droite
                x = room.x+room.w
                y = random.randint(room.y,room.y+room.h-1)
                align = HORIZON
            else: #bas
                x = random.randint(room.x,room.x+room.w-1)
                y = room.y+room.h
                align = VERTICAL
            grid[x][y] = DOOR
            door = Door(x,y,align,id,room.id)
            doorList.append(door)
            
            id+=1
    lastDoorId = id
def foundRoomId(door,rooms,toNotCheck=[]):
    for room in rooms:
        if toNotCheck.count(room.id) == 0:
            if(door.x >= room.x-1 and door.x <= room.x+room.w 
            and door.y >= room.y-1 and door.y <= room.y+room.h):
                return room.id
    return -1
def cleanDoorWall(grid, doorList, doorListLink, roomList):
    global lastDoorId
    toRemove = []
    for door in doorList:
        if door.alignement == HORIZON:
            if (door.x -1 > 0 and grid[door.x-1][door.y] == 2): #Si gauche porte mur (surement bug si c'est un angle)
                grid[door.x-1][door.y] = DOOR
                lastDoorId += 1
                parentId = foundRoomId(door,roomList)
                nDoor = Door(door.x-1,door.y,HORIZON,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
                print("CAS 1")
            if (door.x +1 < GRID_W and grid[door.x+1][door.y]==2):#Si droite porte mur (surement bug si c'est un angle)
                grid[door.x+1][door.y] = DOOR
                lastDoorId += 1
                parentId = foundRoomId(door,roomList)
                nDoor = Door(door.x+1,door.y,HORIZON,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
            if (door.x-1 >= 0 and door.x+1 < GRID_W and grid[door.x-1][door.y]==1 and grid[door.x+1][door.y]==1):# si porte traversse deux mur
                lastDoorId += 1
                parentId = foundRoomId(door,roomList,[door.idParent])
                nDoor = Door(door.x,door.y,HORIZON,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
        if door.alignement == VERTICAL:
            if (door.y -1 > 0 and grid[door.x][door.y-1] == 2): #Si haut porte mur (surement bug si c'est un angle)
                grid[door.x][door.y-1] = DOOR
                lastDoorId += 1
                parentId = foundRoomId(door,roomList)
                nDoor = Door(door.x,door.y-1,VERTICAL,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
            if (door.y +1 < GRID_H and grid[door.x][door.y+1]==2):#Si droite porte mur (surement bug si c'est un angle)
                grid[door.x][door.y+1] = DOOR
                lastDoorId += 1
                parentId = foundRoomId(door,roomList)
                nDoor = Door(door.x,door.y+1,VERTICAL,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
            if (door.y-1 >= 0 and door.y+1 < GRID_H and grid[door.x][door.y-1]==1 and grid[door.x][door.y+1]==1):# si porte traversse deux mur
                lastDoorId += 1
                parentId = foundRoomId(door,roomList,[door.idParent])
                nDoor = Door(door.x,door.y,VERTICAL,lastDoorId,parentId)
                door.link = nDoor.id
                nDoor.link = door
                toRemove.append(door)
                doorListLink.append(door)
                doorListLink.append(nDoor)
    for elem in toRemove:
        doorList.remove(elem)
def grid4path(gridPath):
    gridPath
    for x in range(0,GRID_W):
        for y in range(0,GRID_H):
            if gridPath[x][y] != 0:
                gridPath[x][y] = -1
def copyGrid(grid):
    ret = []
    for x in range(0,GRID_W):
        ret.append(grid[x].copy())
    return ret
def changeDir(dir):
    if dir == HORIZON:
        return VERTICAL
    return HORIZON
def corridor(sx,sy,dir,tx,ty,grid):
    print("(",sx,sy,"),(",tx,ty,")")
    elemGrid = []
    for x in range(0,GRID_W):
        temp = []
        for y in range(0,GRID_H):
            temp.append(Elem(x,y,y+x*GRID_W))
        elemGrid.append(temp)

    # elemGrid = [[Elem(row,col,col+row*GRID_W) for row in range(GRID_H)] for col in range(GRID_W)]
    grid[sx][sy] = 0
    grid[tx][ty] = 0
    elemGrid[sx][sy].visited=True
    elemGrid[sx][sy].parentId = -2
    elemGrid[sx][sy].dir = dir
    toSee = [elemGrid[sx][sy]]
    # See = []

    driftX = [ 0, 1, 0,-1]
    driftY = [-1, 0, 1, 0]
    occ = 0
    occ2=0
    while len(toSee)>0:
        occ +=1
        current = toSee.pop(0)
        for i in range(0,4):
            nx = current.x+driftX[i]
            ny = current.y+driftY[i]
            # print(nx,ny)
            if (nx >= 0 and nx < GRID_W
                and ny >= 0 and ny<GRID_H 
                and grid[nx][ny] == 0):#si coordonée valide
                occ2 +=1
                if(elemGrid[nx][ny].visited):#si voisin visité
                    if(current.dir == HORIZON and driftY[i] == 0 or current.dir == VERTICAL and driftX[i] == 0): # pas de changment de direction
                        if(elemGrid[nx][ny].p > current.p+1): # nouveau chemin plus interesant
                            elemGrid[nx][ny].p = current.p+1
                            elemGrid[nx][ny].parentId = current.id
                            elemGrid[nx][ny].dir = current.dir
                            elemGrid[nx][ny].px = current.x
                            elemGrid[nx][ny].py = current.y
                            toSee.append(elemGrid[nx][ny])
                    else: # changement de direction 
                        if(elemGrid[nx][ny].p > current.p+4): # nouveau chemin plus interesant
                            elemGrid[nx][ny].p = current.p+4
                            elemGrid[nx][ny].parentId = current.id
                            elemGrid[nx][ny].dir = changeDir(current.dir)
                            elemGrid[nx][ny].px = current.x
                            elemGrid[nx][ny].py = current.y
                            toSee.append(elemGrid[nx][ny])
                else: # voisin non visité
                    if(current.dir == HORIZON and driftY[i] == 0 or current.dir == VERTICAL and driftX[i] == 0): # pas de changment de direction
                        elemGrid[nx][ny].p = current.p+1
                        elemGrid[nx][ny].dir = current.dir
                    else:
                        elemGrid[nx][ny].p = current.p+4
                        elemGrid[nx][ny].dir = changeDir(current.dir)
                    elemGrid[nx][ny].parentId = current.id
                    elemGrid[nx][ny].px = current.x
                    elemGrid[nx][ny].py = current.y
                    elemGrid[nx][ny].visited = True
                    toSee.append(elemGrid[nx][ny])
    print(elemGrid[tx][ty].parentId,occ,occ2)
     
    road = []
    nx = elemGrid[tx][ty].px
    ny = elemGrid[tx][ty].py
    road.append((nx,ny))
    while not(elemGrid[nx][ny].px == sx and elemGrid[nx][ny].py == sy):
        road.append((elemGrid[nx][ny].px,elemGrid[nx][ny].py))
        a = elemGrid[nx][ny].px
        ny = elemGrid[nx][ny].py
        nx = a

    
    
    grid[sx][sy] = -1
    grid[tx][ty] = -1

    return road
        


def buildCorridors(rawGrid,doorList,doorListLinked):
    
    grid = copyGrid(rawGrid)
    grid4path(grid)
    while len(doorList) > 1:
        door0 = doorList.pop(0)
        door1 = doorList.pop(len(doorList)-1)
        road = corridor(door0.x,door0.y,door0.alignement,door1.x,door1.y,grid)
        door0.link = door1.id
        door1.link = door0.id

        for coor in road:
            # print(coor[0],coor[1])
            rawGrid[coor[0]][coor[1]] = CORRIDOR 
    printGrille(rawGrid)


if __name__ == '__main__':
    random.seed()
    renduFinal = [[0 for col in range(GRID_H)] for row in range(GRID_W)]
    roomList = []
    doorList = []
    doorListlink = []
    for x in range (0,GRID_W):
        for y in range (0,GRID_H):
            renduFinal[x][y] = 0
    createRooms(renduFinal,roomList,4,2,5)
    createDoors(renduFinal,roomList,doorList,1,3)
    cleanDoorWall(renduFinal,doorList,doorListlink,roomList)
    buildCorridors(renduFinal,doorList,doorListlink)
    # printGrille(renduFinal)
    # print("salle",len(roomList))
    # print("door nL",len(doorList))
    # print("door  L",len(doorListlink))
    
    # grillePath = copyGrid(renduFinal)
    # grid4path(grillePath)

    # door0 = doorList.pop(0)
    # door1 = doorList.pop(0)
    # road = corridor(door0.x,door0.y,door0.alignement,door1.x,door1.y,grillePath)
    # print("road :",road)
    
    # for coor in road:
    #     # print(coor[0],coor[1])
    #     renduFinal[coor[0]][coor[1]] = CORRIDOR
    # printGrille(renduFinal)
    
    