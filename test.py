def func (val1,val2=[]):
    print(val1,len(val2))

if __name__ == '__main__':
    final = []
    for y in range(0,10):
        temp = []
        for x in range(0,10):
            temp.append(x+y*10)
        final.append(temp.copy())
    
    for x in range(0,10):
        for y in range(0,10):
            print(final[x][y],end=" ")
        print()
