#include <iostream>
#include <list>
#include <cstdlib>
using namespace std;

void func(list<int*> *l)
{
    int* i= (int*) malloc(sizeof(int));
    *i = 4;
    l->push_front(i);
}

int main(){
    list<int*> l;
    func(&l);
    int * i = l.front();
    cout << *i << endl;
    return 0;
}