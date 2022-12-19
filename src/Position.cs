using System;

namespace PathInMaze
{
    public class Position
    {
        public int row;
        public int column;

        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public static Position operator -(Position basePos, Position anotherPos)
        {
            return new Position(basePos.row - anotherPos.row, basePos.column - anotherPos.column);
        }

        public static bool operator ==(Position basePos, Position anotherPos)
        {
            return basePos.row == anotherPos.row && basePos.column == anotherPos.column;
        }

        public static bool operator !=(Position basePos, Position anotherPos)
        {
            return basePos.row != anotherPos.row || basePos.column != anotherPos.column;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
}