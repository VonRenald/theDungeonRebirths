import random

GRID_W = 20
GRID_H = 25

HORIZON  = 1
VERTICAL = 2

lastDoorId = 0 

VOID = 0
ROOM = 1
WALL = 2
CORRIDOR = 3
DOOR = 4
CORNER = 5


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
        self.link = []
def printGrille(grille):
    print("   ",end='')
    for x in range (0,GRID_W):
        print(x%10,end="  ")
    print()
    for y in range (0,GRID_H):
        print(y%10,end="  ")
        for x in range (0,GRID_W):
            val = grille[x][y]
            if(val == VOID):
                print(" ",end="  ")
            elif(val == ROOM):
                print(".",end="  ")
            elif(val == WALL):
                print("2",end="  ")
            elif(val == CORRIDOR):
                print("*",end="  ")
            elif(val == DOOR):
                print("p",end="  ")
            elif(val == CORNER):
                print("4",end="  ")
        print()
def printGrilleInt(grille):
    for y in range (0,GRID_H):
        for x in range (0,GRID_W):
            print(grille[x][y],end="  ")
        print()
def printIdList(list):
    print("[",end="")
    for elem in list:
        print(elem.id,end=",")
    print("]")
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
            isVoid = True
            for i in range(x,x+w):
                for j in range(y,y+h):
                    isVoid = isVoid and grid[i][j]==VOID
            stop = isVoid
            # stop = (grid[x][y] == VOID and grid[x+w-1][y] == VOID and grid[x][y+h-1] == VOID and grid[x+w-1][y+h-1] == VOID)
        room = Room(w,h,x,y,id)
        roomList.append(room)
        for i in range (x-1,x+w+1):
            for j in range (y-1,y+h+1):
                if grid[i][j]!=CORNER:
                    if ((i==x-1 or i==x+w) or (j==y-1 or j==y+h)):
                        grid[i][j]=WALL
                    else: 
                        grid[i][j]=ROOM
        grid[x-1][y-1] = CORNER
        grid[x+w][y-1] = CORNER
        grid[x-1][y+h] = CORNER
        grid[x+w][y+h] = CORNER
def createDoors(grid, roomList, doorList, minD, maxD):
    global lastDoorId
    id = 0
    for room in roomList:
        for i in range(0,random.randint(minD,maxD)):
            stop = False
            while not stop:
                cote = random.randint(1,4)
                x=0
                y=0
                align = 0
                if cote==1: #gauche
                    x = room.x-1
                    y = random.randint(room.y,room.y+room.h-2)
                    align = HORIZON
                elif cote==2: #haut
                    x = random.randint(room.x,room.x+room.w-2)
                    y = room.y-1
                    align = VERTICAL
                elif cote==3: #droite
                    x = room.x+room.w
                    y = random.randint(room.y,room.y+room.h-2)
                    align = HORIZON
                else: #bas
                    x = random.randint(room.x,room.x+room.w-2)
                    y = room.y+room.h
                    align = VERTICAL
                stop = grid[x][y] != DOOR and grid[x][y] != CORNER
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
def foundRoomCoor(x,y,rooms,toNotCheck=[]):
    for room in rooms:
        if (x==room.x-1 or x==room.x+room.w) and (y==room.y-1 or y==room.y+room.h) and toNotCheck.count(room.id) == 0:
            return room.id
    return None 
def popCoorDoor(list,x,y):
    i=0
    while i<len(list):
        if list[i].x == x and list[i].y == y:
            return list.pop(i)
        i+=1
    return None
def cleanDoorWall(grid, doorList, doorWallList, roomList):
    global lastDoorId
    maxa = len(doorList)
    i= 0
    while i < maxa:
        door = doorList.pop(0)
        toRemove = False
        if door.alignement == HORIZON:
            if(door.x-1>0):
                if(grid[door.x-1][door.y] == WALL):#Si gauche porte mur
                    grid[door.x-1][door.y] = DOOR
                    lastDoorId += 1
                    parentId = foundRoomCoor(door.x-1,door.y,roomList)
                    nDoor = Door(door.x-1,door.y,HORIZON,lastDoorId,parentId)
                    door.link.append(nDoor.id)
                    nDoor.link.append(door.id)
                    # toRemove.append(door)
                    toRemove = True
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    print("CAS 1.0")
                elif(grid[door.x-1][door.y] == CORNER):
                    if(grid[door.x][door.y+1] == WALL):
                        grid[door.x][door.y]=WALL
                        door.y+=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    elif(grid[door.x][door.y-1] == WALL):
                        grid[door.x][door.y]=WALL
                        door.y-=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    else:
                        toRemove = True
                    print("CAS 1.1")
                elif(grid[door.x-1][door.y] == DOOR):
                    nDoor = popCoorDoor(doorList,door.x-1,door.y)
                    nDoor.link.append(door.id)
                    door.link.append(nDoor.id)
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    toRemove=True



            if (door.x+1 < GRID_W):#Si droite porte mur 
                if (grid[door.x+1][door.y]==WALL):
                    grid[door.x+1][door.y] = DOOR
                    lastDoorId += 1
                    parentId = foundRoomCoor(door.x+1,door.y,roomList)
                    nDoor = Door(door.x+1,door.y,HORIZON,lastDoorId,parentId)
                    door.link.append(nDoor.id)
                    nDoor.link.append(door.id)
                    # toRemove.append(door)
                    toRemove = True
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    print("CAS 2.0")
                elif(grid[door.x+1][door.y]==CORNER):
                    if(grid[door.x][door.y-1] == WALL):
                        grid[door.x][door.y]=WALL
                        door.y-=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    elif(grid[door.x][door.y+1] == WALL):
                        grid[door.x][door.y]=WALL
                        door.y+=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    else:
                        toRemove = True
                    print("CAS 2.1")
                elif(grid[door.x+1][door.y] == DOOR):
                    nDoor = popCoorDoor(doorList,door.x+1,door.y)
                    nDoor.link.append(door.id)
                    door.link.append(nDoor.id)
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    toRemove=True

            if (door.x-1 >= 0 and door.x+1 < GRID_W and grid[door.x-1][door.y]==ROOM and grid[door.x+1][door.y]==ROOM):# si porte traversse deux mur
                lastDoorId += 1
                parentId = foundRoomCoor(door.x,door.x,roomList,[door.idParent])
                nDoor = Door(door.x,door.y,HORIZON,lastDoorId,parentId)
                door.link.append(nDoor.id)
                nDoor.link.append(door.id)
                # toRemove.append(door)
                toRemove = True
                doorWallList.append(door)
                doorWallList.append(nDoor)
                print("CAS 3.0")

        if door.alignement == VERTICAL:
            if(door.y-1>0):#Si haut porte mur
                if(grid[door.x][door.y-1] == WALL):
                    grid[door.x][door.y-1] = DOOR
                    lastDoorId += 1
                    parentId = foundRoomCoor(door.x,door.y-1,roomList)
                    nDoor = Door(door.x,door.y-1,VERTICAL,lastDoorId,parentId)
                    door.link.append(nDoor.id)
                    nDoor.link.append(door.id)
                    # toRemove.append(door)
                    toRemove = True
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    print("CAS 4.0")
                elif(grid[door.x][door.y-1] == CORNER):
                    if(grid[door.x+1][door.y] == WALL):
                        grid[door.x][door.y]=WALL
                        door.x+=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    elif(grid[door.x-1][door.y] == WALL):
                        grid[door.x][door.y]=WALL
                        door.x-=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    else:
                        toRemove=True
                    print("CAS 4.1")
                elif(grid[door.x][door.y-1] == DOOR):
                    nDoor = popCoorDoor(doorList,door.x,door.y-1)
                    if(nDoor==None):
                        return 1
                    nDoor.link.append(door.id)
                    door.link.append(nDoor.id)
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    toRemove=True


            if (door.y+1<GRID_H):#Si droite porte mur
                if(grid[door.x][door.y+1]==WALL):
                    grid[door.x][door.y+1] = DOOR
                    lastDoorId += 1
                    parentId = foundRoomCoor(door.x,door.y+1,roomList)
                    nDoor = Door(door.x,door.y+1,VERTICAL,lastDoorId,parentId)
                    door.link.append(nDoor.id)
                    nDoor.link.append(door.id)
                    # toRemove.append(door)
                    toRemove = True
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    print("CAS 5.0")
                elif(grid[door.x][door.y+1]==CORNER):
                    if(grid[door.x+1][door.y] == WALL):
                        grid[door.x][door.y]=WALL
                        door.x+=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    elif(grid[door.x-1][door.y] == WALL):
                        grid[door.x][door.y]=WALL
                        door.x-=1
                        grid[door.x][door.y]=DOOR
                        doorList.insert(0,door)
                    else:
                        toRemove=True
                    print("CAS 5.1")
                elif(grid[door.x][door.y+1] == DOOR):
                    nDoor = popCoorDoor(doorList,door.x,door.y+1)
                    nDoor.link.append(door.id)
                    door.link.append(nDoor.id)
                    doorWallList.append(door)
                    doorWallList.append(nDoor)
                    toRemove=True

            if (door.y-1 >= 0 and door.y+1 < GRID_H and grid[door.x][door.y-1]==ROOM and grid[door.x][door.y+1]==ROOM):# si porte traversse deux mur
                lastDoorId += 1
                parentId = foundRoomCoor(door.x,door.y,roomList,[door.idParent])
                nDoor = Door(door.x,door.y,VERTICAL,lastDoorId,parentId)
                door.link.append(nDoor.id)
                nDoor.link.append(door.id)
                # toRemove.append(door)
                toRemove = True
                doorWallList.append(door)
                doorWallList.append(nDoor)
                print("CAS 6.0")
        if not toRemove:
            doorList.append(door)
        i+=1
    for elem in doorWallList:
        for i in range (0,doorList.count(elem)):
            doorList.remove(elem)
    return 0
def grid4path(gridPath):
    gridPath
    for x in range(0,GRID_W):
        for y in range(0,GRID_H):
            if gridPath[x][y] == VOID or gridPath[x][y] == CORRIDOR:
                gridPath[x][y] = 0
            else:
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

        
        stop = False
        while not stop:#verifie que les deux porte soit d'une salle differente
            index = random.randint(0,len(doorList)+len(doorListLinked)-1)
            if(index>=len(doorList)):
                value = doorListLinked[index-len(doorList)].idParent
            else:
                value = doorList[index].idParent
            stop = value != door0.idParent
            print("la")

        if(index>=len(doorList)):
            door1 = doorListLinked.pop(index-len(doorList))
        else:
            door1 = doorList.pop(index)
        # door1 = doorList.pop(len(doorList)-1)
        if (door0.id == door1.id or (door0.x == door1.x and door0.y == door1.y)):
            doorList.append(door0)
        else:
            print("ids :",door0.id,door1.id)
            print("Pids :",door0.idParent,door1.idParent)
            road = corridor(door0.x,door0.y,door0.alignement,door1.x,door1.y,grid)
            door0.link.append(door1.id)
            door1.link.append(door0.id)
            doorListLinked.append(door0)
            doorListLinked.append(door1)
            for coor in road:
                # print(coor[0],coor[1])
                rawGrid[coor[0]][coor[1]] = CORRIDOR 
    if len(doorList) == 1:
        door0 = doorList.pop(0)
        i = random.randint(0,len(doorListLinked)-1)
        road = corridor(door0.x,door0.y,door0.alignement,doorListLinked[i].x,doorListLinked[i].y,grid)
        doorListLinked[i].link.append(door0.id)
        door0.link.append(doorListLinked[i].id)
        doorListLinked.append(door0)
        for coor in road:
            # print(coor[0],coor[1])
            rawGrid[coor[0]][coor[1]] = CORRIDOR 
def getListIdDoor(roomId,doors):
    l = []
    for door in doors:
        if door.idParent == roomId:
            l.append(door.id)
    return l
def suprimeDoublonListInt(l):
    ret = []
    while len(l)>0:
        elem = l.pop(0)
        if ret.count(elem) == 0:
            ret.append(elem)
    return ret
def supprListAdeB(a,b):
    ret = []
    for elem in b:
        if(a.count(elem)==0):
            ret.append(elem)
    return ret
def getDoor(id,doors):
    for door in doors:
        if door.id == id:
            return door
    return None
def popDoor(id,doors):
    i = 0
    for door in doors:
        if door.id == id:
            return doors.pop(i)
        i+=1
    return None
def blockUnique(rawGrid,doorListLinked,doorList,roomList):
    print("doors :",end="")
    printIdList(doorList)
    StartDoor = doorListLinked.pop(random.randint(0,len(doorListLinked)-1))
    roomAcces = [StartDoor.idParent]
    doorSee = [StartDoor.id]
    door2See = getListIdDoor(StartDoor.id,doorList) + StartDoor.link
    door2See = suprimeDoublonListInt(door2See)
    door2See = supprListAdeB(doorSee,door2See)
    print("see",doorSee)
    print("2see",door2See)
    while len(door2See) > 0:
        currentDoor = getDoor(door2See.pop(0),doorList)
        if(roomAcces.count(currentDoor.idParent)==0):
            roomAcces.append(currentDoor.idParent)
        door2See = door2See+getListIdDoor(currentDoor.id,doorList) + currentDoor.link
        doorSee.append(currentDoor.id)
        door2See = suprimeDoublonListInt(door2See)
        door2See = supprListAdeB(doorSee,door2See)
        print("see",doorSee)
        print("2see",door2See)


    print("room acces :",len(roomList),len(roomAcces),len(roomList)==len(roomAcces))
    print("acces :",roomAcces)
    if(len(roomList)>len(roomAcces)):
        #prend une room non accessible
        idRoom = None
        for room in roomAcces:
            if(roomList.count(room) == 0):
                idRoom = room
        door2link = popDoor(getListIdDoor(idRoom,doorList)[0],doorListLinked)
        grid = copyGrid(rawGrid)
        grid4path(grid)
        print("ids :",StartDoor.id,door2link.id)
        print("Pids :",StartDoor.idParent,door2link.idParent)
        road = corridor(StartDoor.x,StartDoor.y,StartDoor.alignement,door2link.x,door2link.y,grid)
        StartDoor.link.append(door2link.id)
        door2link.link.append(StartDoor.id)
        doorListLinked.append(StartDoor)
        doorListLinked.append(door2link)
        for coor in road:
            rawGrid[coor[0]][coor[1]] = CORRIDOR 
        blockUnique(rawGrid,doorListLinked,doorList,roomList)






def writeLog(seed,roomList,doorList,doorListlink,doorWallList):
    f = open("log.txt", "a")
    f.write(f'----------{seed}\n')
    f.write(f'rooms:{len(roomList)}\n')
    for room in roomList:
        f.write(f'id:{room.id} x:{room.x} y:{room.y} w:{room.w} h:{room.h}\n')
    f.write(f'doorList:{len(doorList)}\n')
    for door in doorList:
        f.write(f'id:{door.id} parentId:{door.idParent} x:{door.x} y:{door.y} alignement:{door.alignement} link:{door.link}\n')
    f.write(f'doorListlink:{len(doorListlink)}\n')
    for door in doorListlink:
        f.write(f'id:{door.id} parentId:{door.idParent} x:{door.x} y:{door.y} alignement:{door.alignement} link:{door.link}\n')
    f.write(f'doorWallList:{len(doorWallList)}\n')
    for door in doorWallList:
        f.write(f'id:{door.id} parentId:{door.idParent} x:{door.x} y:{door.y} alignement:{door.alignement} link:{door.link}\n')
    f.close()

        # self.x = x
        # self.y = y
        # self.id = id
        # self.idParent = idParent
        # self.alignement = alignement
        # self.link = []

if __name__ == '__main__':
    random.seed()
    seed = random.randint(0,9999999999999999999999999)
    random.seed(seed)
    renduFinal = [[0 for col in range(GRID_H)] for row in range(GRID_W)]
    roomList = []
    doorList = []
    doorListlink = []
    doorWallList = []
    for x in range (0,GRID_W):
        for y in range (0,GRID_H):
            renduFinal[x][y] = 0
    createRooms(renduFinal,roomList,4,2,5)
    createDoors(renduFinal,roomList,doorList,1,1)
    if cleanDoorWall(renduFinal,doorList,doorWallList,roomList) != 0:
        print("error")
    printIdList(doorList)
    printIdList(doorWallList)
    printIdList(doorListlink)
    buildCorridors(renduFinal,doorList,doorListlink)
    
    printGrille(renduFinal)

    print("doorList:",end="")
    printIdList(doorList)
    print("doorListlink:",end="")
    printIdList(doorListlink)
    print("doorWallList:",end="")
    printIdList(doorWallList)
    blockUnique(renduFinal,doorListlink,doorList+doorListlink+doorWallList,roomList)
    print("roomList:",end="")
    printIdList(roomList)
    writeLog(seed,roomList,doorList,doorListlink,doorWallList)
    
    
    