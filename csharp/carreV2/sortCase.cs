using System;

namespace GenerationMap
{
    enum Case {
        VOID, 
        // marchable
        ROOM, CORRIDOR,
        // wall 
        WALL, WALL_UP, WALL_DOWN, WALL_RIGHT, WALL_LEFT, 
        // wall double
        WALL_DOUBLE_H, WALL_DOUBLE_V,
        // cross
        CROSS, CROSS_UP_RIGHT, CROSS_UP_LEFT, CROSS_DOWN_RIGHT, CROSS_DOWN_LEFT, CROSS_DIAG_LEFT, CROSS_DIAG_RIGHT,
        // T
        WALL_T_UP, WALL_T_UP_DOWN, WALL_T_UP_RIGHT, WALL_T_UP_LEFT, 
        WALL_T_DOWN, WALL_T_DOWN_UP, WALL_T_DOWN_RIGHT, WALL_T_DOWN_LEFT, 
        WALL_T_RIGHT, WALL_T_RIGHT_LEFT, WALL_T_RIGHT_UP, WALL_T_RIGHT_DOWN, 
        WALL_T_LEFT, WALL_T_LEFT_RIGHT, WALL_T_LEFT_UP, WALL_T_LEFT_DOWN,
        // corner
        CORNER, CORNER_UP_RIGHT, CORNER_UP_LEFT, CORNER_DOWN_RIGHT, CORNER_DOWN_LEFT, 
        // corner in
        CORNER_IN_UP_RIGHT, CORNER_IN_UP_LEFT, CORNER_IN_DOWN_RIGHT, CORNER_IN_DOWN_LEFT, 
        // corner in midle
        CORNER_IN_MIDLE_UP, CORNER_IN_MIDLE_DOWN, CORNER_IN_MIDLE_RIGHT, CORNER_IN_MIDLE_LEFT, CORNER_IN_MIDLE,
        // corner double
        CORNER_DOUBLE_UP_RIGHT, CORNER_DOUBLE_UP_LEFT, CORNER_DOUBLE_DOWN_RIGHT, CORNER_DOUBLE_DOWN_LEFT,
        // door
        DOOR, DOOR_H, DOOR_H_OPEN, DOOR_V,  DOOR_V_OPEN,
        //error
        NULL,ERROR
    };
    class simplificationCase{
        public Case[] roof = {Case.ROOM, Case.CORRIDOR};
        public Case[] door = {Case.DOOR, Case.DOOR_H, Case.DOOR_H_OPEN, Case.DOOR_V,  Case.DOOR_V_OPEN};
        public Case[] wall = {Case.WALL, Case.WALL_UP, Case.WALL_DOWN, Case.WALL_RIGHT, Case.WALL_LEFT, 
        Case.WALL_DOUBLE_H, Case.WALL_DOUBLE_V,
        Case.CROSS, Case.CROSS_UP_RIGHT, Case.CROSS_UP_LEFT, Case.CROSS_DOWN_RIGHT, Case.CROSS_DOWN_LEFT, Case.CROSS_DIAG_LEFT, Case.CROSS_DIAG_RIGHT,
        Case.WALL_T_UP, Case.WALL_T_UP_DOWN, Case.WALL_T_UP_RIGHT, Case.WALL_T_UP_LEFT, 
        Case.WALL_T_DOWN, Case.WALL_T_DOWN_UP, Case.WALL_T_DOWN_RIGHT, Case.WALL_T_DOWN_LEFT, 
        Case.WALL_T_RIGHT, Case.WALL_T_RIGHT_LEFT, Case.WALL_T_RIGHT_UP, Case.WALL_T_RIGHT_DOWN, 
        Case.WALL_T_LEFT, Case.WALL_T_LEFT_RIGHT, Case.WALL_T_LEFT_UP, Case.WALL_T_LEFT_DOWN,
        Case.CORNER, Case.CORNER_UP_RIGHT, Case.CORNER_UP_LEFT, Case.CORNER_DOWN_RIGHT, Case.CORNER_DOWN_LEFT, 
        Case.CORNER_IN_UP_RIGHT, Case.CORNER_IN_UP_LEFT, Case.CORNER_IN_DOWN_RIGHT, Case.CORNER_IN_DOWN_LEFT, 
        Case.CORNER_IN_MIDLE_UP, Case.CORNER_IN_MIDLE_DOWN, Case.CORNER_IN_MIDLE_RIGHT, Case.CORNER_IN_MIDLE_LEFT, Case.CORNER_IN_MIDLE,
        Case.CORNER_DOUBLE_UP_RIGHT, Case.CORNER_DOUBLE_UP_LEFT, Case.CORNER_DOUBLE_DOWN_RIGHT, Case.CORNER_DOUBLE_DOWN_LEFT};
        
        public Case simpl(Case c){
            if(roof.Contains(c))
                return Case.ROOM;
            if(door.Contains(c))
                return Case.DOOR;
            if(wall.Contains(c))
                return Case.WALL;
            if(c == Case.NULL)
                return c;
            return Case.ERROR;
        }
    
    }
    class sortCase{
        public List<Case> up;
        public List<Case> down;
        public List<Case> left;
        public List<Case> right;
        
        public List<Case> upRight;
        
        public List<Case> upLeft;
        
        public List<Case> downRight;
        
        public List<Case> DownLeft;
        public Case myCase;

        public sortCase(Case myCase_, List<Case> up_, List<Case> down_, List<Case> right_, List<Case> left_,
            List<Case> upRight_, List<Case> upLeft_, List<Case> downRight_, List<Case> downLeft_){
            myCase = myCase_;
            up = up_;
            down = down_;
            right = right_;
            left = left_;
            upRight = upRight_;
            upLeft = upLeft_;
            downRight = downRight_;
            DownLeft = downLeft_;
        }
    }
    class sortCaseList{

        public List<sortCase> listCase;
        public sortCaseList()
        {
            listCase = new List<sortCase>{
                //WALL
                new sortCase(Case.WALL_UP,
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_DOWN,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.WALL_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                
                

                
                // middle wall
                new sortCase(Case.WALL_DOUBLE_H,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_DOUBLE_V,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                

                

                // Cross
                new sortCase(Case.CROSS,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CROSS_UP_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CROSS_UP_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CROSS_DOWN_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CROSS_DOWN_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.CROSS_DIAG_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CROSS_DIAG_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                                
                
                // T UP
                new sortCase(Case.WALL_T_UP,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_UP_DOWN,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.WALL_T_UP_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_UP_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),


                // T DOWN
                new sortCase(Case.WALL_T_DOWN,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_DOWN_UP,
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_DOWN_RIGHT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_DOWN_LEFT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),


                // T RIGHT
                new sortCase(Case.WALL_T_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_RIGHT_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.WALL_T_RIGHT_UP,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_RIGHT_DOWN,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),


                // T LEFT
                new sortCase(Case.WALL_T_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_LEFT_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_LEFT_UP,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.WALL_T_LEFT_DOWN,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),




                //Corner out
                new sortCase(Case.CORNER_UP_RIGHT,
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_UP_LEFT,
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.CORNER_DOWN_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),
                new sortCase(Case.CORNER_DOWN_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),




                //corner middle
                new sortCase(Case.CORNER_DOUBLE_UP_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_DOUBLE_UP_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_DOUBLE_DOWN_RIGHT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_DOUBLE_DOWN_LEFT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR}),




                //corner in
                new sortCase(Case.CORNER_IN_UP_RIGHT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_UP_LEFT,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_DOWN_RIGHT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_DOWN_LEFT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.VOID, Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V}),



                    
                //corner in midle
                new sortCase(Case.CORNER_IN_MIDLE_UP,
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_MIDLE_DOWN,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_MIDLE_RIGHT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_MIDLE_LEFT,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR}),
                new sortCase(Case.CORNER_IN_MIDLE,
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR},
                    new List<Case>{Case.WALL, Case.CORNER, Case.DOOR, Case.DOOR_H, Case.DOOR_V, Case.ROOM,Case.CORRIDOR})

            };
        }
    }
}