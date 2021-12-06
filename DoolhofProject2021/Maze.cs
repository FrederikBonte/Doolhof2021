using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Maze
    {
        // Create a 2D plane of rooms.
        private Room[,] maze;
        // Store the explore path through the maze.
        private Stack<Room> path;

        public Maze(int width, int height, bool generate = true)
        {
            // Create enough space.
            maze = new Room[width, height];
            // Loop through all rows.
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                // Loop through all columns.
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    // Actual create a room.
                    maze[x, y] = new Room(x,y);
                }
            }
            maze[width - 1, height - 1].makeExit();

            // Start the exploration path.
            path = new Stack<Room>();
            path.Push(maze[0, 0]);

            if (generate)
            {
                generateMaze();
            }
        }

        /// <summary>
        /// How wide is this maze?
        /// </summary>
        /// <returns></returns>
        public int getWidth()
        {
            return maze.GetLength(0);
        }

        /// <summary>
        /// How high is this maze?
        /// </summary>
        /// <returns></returns>
        public int getHeight()
        {
            return maze.GetLength(1);
        }

        /// <summary>
        /// Retrieve one particular room.
        /// </summary>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <returns></returns>
        public Room getRoom(int cx, int cy)
        {
            return maze[cx, cy];
        }

        public bool isMazeDone()
        {
            return path.Count == 0;
        }

        private void generateMaze()
        {
            while (!isMazeDone())
            {
                generateStep();
            }
        }

        public void generateStep()
        {
            if (isMazeDone())
            {
                return;
            }
            Room current = path.Peek();
            // Choose a random direction...
            int direction = LehmerRNG.Next(4);
            // Check if that direction can be used...
            //  * Not out of bounds.
            //  * Not yet connected.
            while (!directionCanBeUsed(direction % 4, current.getX(), current.getY(), current))
            {
                direction = direction + 1;
                if (direction == 7)
                {
                    Console.WriteLine("Back to " + current.getX() + ", " + current.getY());
                    // No further connection possible...
                    current = path.Pop();
                    return;
                }
            }
            direction = direction % 4;
            Console.WriteLine(current.getX() + "," + current.getY() + " moving " + direction);
            // Then create a connection... 
            // Retrieve the room that lies in that direction...
            int nx = current.getX() + Direction.getDx(direction);
            int ny = current.getY() + Direction.getDy(direction);
            Room other = maze[nx, ny];
            // Create a connection in that direction to the other room.
            current.createConnection(direction, other);
            // Let the other room create the opposite connection toward the current Room.
            other.createConnection(Direction.getOpposite(direction), current);
            // Repeat for that room...
            current = other;
            path.Push(current);
        }

        private bool directionCanBeUsed(int direction, int cx, int cy, Room current)
        {
            if (direction == Direction.NORTH && cy == 0)
            {
                return false;
            }
            else if (direction == Direction.WEST && cx == 0)
            {
                return false;
            }
            else if (direction == Direction.SOUTH && cy == (maze.GetLength(1) - 1))
            {
                return false;
            }
            else if (direction == Direction.EAST && cx == (maze.GetLength(0) - 1))
            {
                return false;
            }
            else
            {
                // If the direction is already used, you cannot use it again!
                return !current.canGo(direction);
            }
        }

        public void drawPath(Graphics g, int scale)
        {
            int lx = -1;
            int ly = -1;
            Pen p = new Pen(Color.Blue, 5);
            p.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            p.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            foreach (Room r in path)
            {
                int cx = r.getX()*scale+ (scale/2);
                int cy = r.getY()*scale +(scale/2);
                if (lx!=-1)
                {
                    g.DrawLine(p, lx, ly, cx, cy);
                }
                lx = cx;
                ly = cy;
            }
        }
    }
}
