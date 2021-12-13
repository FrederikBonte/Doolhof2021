using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Direction
    {
        // There are four possible directions to move from Room to Room.
        public const int NORTH = 0;
        public const int EAST = 1;
        public const int SOUTH = 2;
        public const int WEST = 3;

        private static int[] dx = { 0, 1, 0, -1 };
        private static int[] dy = { -1, 0, 1, 0 };

        public static int getRandomDirection()
        {
            // Choose randomly one of the direction.
            return LehmerRNG.Next(4);
        }

        public static int getOpposite(int direction)
        {
            int result = direction + 2;
            return result % 4;
        }

        public static int getToTheLeft(int direction)
        {
            int result = direction + 3;
            return result % 4;
        }

        public static int getToTheRight(int direction)
        {
            int result = direction + 1;
            return result % 4;
        }

        internal static int getDx(int direction)
        {
            return dx[direction % 4];
        }

        internal static int getDy(int direction)
        {
            return dy[direction % 4];
        }
    }
}
