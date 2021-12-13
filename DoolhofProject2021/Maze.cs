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
            reset(width, height);
            if (generate)
            {
                generateMaze();
            }
        }

        public void reset(int width, int height)
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

        private void checkComplexEnough()
        {
            if (path.Count() > 0 && path.Peek().isExit())
            {
                if (path.Count() < (getWidth()+getHeight()) * 5.5)
                {
                    Console.WriteLine("Rejecting maze.");
                    reset(getWidth(), getHeight());

                }
                else
                {
                    Console.WriteLine("Accepable complexity detected.");
                }
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
            int direction = 0;
            if (getUsableDirection(current, out direction))
            {
                makeConnection(current, direction);
                checkComplexEnough();
            }
            else
            {
                //Console.WriteLine("Back to " + current.getX() + ", " + current.getY());
                // No further connection possible...
                path.Pop();
                return;
            }
        }

        private void makeConnection(Room current, int direction)
        {
            //Console.WriteLine(current.getX() + "," + current.getY() + " moving " + direction);
            // Then create a connection... 
            // Retrieve the room that lies in that direction...
            int nx = current.getX() + Direction.getDx(direction);
            int ny = current.getY() + Direction.getDy(direction);
            Room other = maze[nx, ny];
            // Create a connection in that direction to the other room.
            current.createConnection(direction, other);
            // Let the other room create the opposite connection toward the current Room.
            other.createConnection(Direction.getOpposite(direction), current);
            // Add the new room at the end of the path.
            path.Push(other);
        }

        private bool getUsableDirection(Room current, out int direction)
        {
            direction = -1;
            for (int i = 0; i < 8; i++) //()
            {
                direction = LehmerRNG.Next(4);
                if (directionCanBeUsed(direction, current))
                {
                    return true;
                }
                else if (directionCanBeUsed(i%4, current))
                {
                    direction = i%4;
                    return true;
                }
            }
            return false;
        }



        private bool directionCanBeUsed(int direction, Room current)
        {
            // Check if that direction can be used...
            //  * Not out of bounds.
            if (direction == Direction.NORTH && current.getY() == 0)
            {
                return false;
            }
            else if (direction == Direction.WEST && current.getX() == 0)
            {
                return false;
            }
            else if (direction == Direction.SOUTH && current.getY() == (maze.GetLength(1) - 1))
            {
                return false;
            }
            else if (direction == Direction.EAST && current.getX() == (maze.GetLength(0) - 1))
            {
                return false;
            }
            else
            {
                //  * Target room is not yet connected to anything else!
                Room r = getRoom(
                    current.getX() + Direction.getDx(direction),
                    current.getY() + Direction.getDy(direction)
                    );
                return !r.isConnected();
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
