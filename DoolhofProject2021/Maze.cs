using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Maze
    {
        // Create a 2D plane of rooms.
        private Room[,] maze;

        public Maze(int width, int height)
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

            generateMaze();
        }

        private void generateMaze()
        {
            // Start in a room.
            Room current = maze[0, 0];
            Stack<Room> path = new Stack<Room>();
            path.Push(current);
            while (path.Count>0)
            {
                // Choose a random direction...
                int direction = LehmerRNG.Next(4);
                // Check if that direction can be used...
                //  * Not out of bounds.
                //  * Not yet connected.
                while (directionCannotBeUsed(direction % 4, current.getX(), current.getY(), current))
                {
                    direction = direction + 1;
                    if (direction == 7)
                    {
                        if (path.Count==0)
                        {
                            return;
                        }
                        // No further connection possible...
                        current = path.Pop();
                        direction = LehmerRNG.Next(4);
                        Console.WriteLine("Back to " + current.getX() + ", " + current.getY());
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
        }

        private Room getRoom(int cx, int cy, int direction)
        {
            return maze[
                cx + Direction.getDx(direction),
                cy + Direction.getDy(direction)
                ];
        }

        private bool directionCannotBeUsed(int direction, int cx, int cy, Room current)
        {
            if (direction == Direction.NORTH && cy == 0)
            {
                return true;
            }
            else if (direction == Direction.WEST && cx == 0)
            {
                return true;
            }
            else if (direction == Direction.SOUTH && cy == (maze.GetLength(1) - 1))
            {
                return true;
            }
            else if (direction == Direction.EAST && cx == (maze.GetLength(0) - 1))
            {
                return true;
            }
            else
            {
                return current.canGo(direction);
            }
        }
    }
}
