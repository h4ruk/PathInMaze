using System;

namespace PathInMaze
{
    [Flags]
    public enum Wall
    {
        RIGHT = 1, //0001
        LEFT = 2, //0010
        BOTTOM = 4, //0100
        TOP = 8, //1000
        ALL = 15 //1111
    }
}