using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Room
    {
        // Each room has four possible connections.
        private Room[] connections = new Room[4];
        // Store the rooms position.
        private readonly int x,y;

        // Can the maze be exited from this room?
        private bool exit;
        // Is there (currently) a treasure in this room?
        private bool treasure;

        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        #region Navigation
        /// <summary>
        /// Returns true when there is a connection in the provided direction.
        /// </summary>
        /// <param name="direction">The direction in which to move.</param>
        /// <returns>true when there is a connection.</returns>
        internal bool canGo(int direction)
        {
            return connections[direction%4] != null;
        }

        /// <summary>
        /// Returns the room that lies in that direction.
        /// Throws an exception when you cannot move in that direction.
        /// Please check with the {@link canGoNorth()} function for example.
        /// </summary>
        /// <param name="direction">The direction in which to move.</param>
        /// <returns></returns>
        internal Room go(int direction)
        {
            if (!canGo(direction))
            {
                throw new Exception("Illegal move no connection there.");
            }
            return connections[direction%4];
        }

        internal void createConnection(int direction, Room other)
        {
            // Don't overwrite existing connections...
            if (canGo(direction))
            {
                throw new Exception("A connection in that direction already exists!");
            }
            // Add a connection to the given room.
            connections[direction%4] = other;
        }

        public bool canGoEast()
        {
            return canGo(Direction.EAST);
        }

        public Room goEast()
        {
            return go(Direction.EAST);
        }

        public bool canGoWest()
        {
            return canGo(Direction.WEST);
        }

        public Room goWest()
        {
            return go(Direction.WEST);
        }

        public bool canGoNorth()
        {
            return canGo(Direction.NORTH);
        }

        public bool canGoSouth()
        {
            return canGo(Direction.SOUTH);
        }

        public Room goNorth()
        {
            return go(Direction.NORTH);
        }

        public Room goSouth()
        {
            return go(Direction.SOUTH);
        }
        #endregion

        #region Gameplay
        public bool isTreasure()
        {
            return this.treasure;
        }

        public bool pickUpTreasure()
        {
            // IF there is a treasure in this room.
            if (isTreasure())
            {
                // Remove the treasure from this room.
                treasure = false;
                // Yay! You got a treasure.
                return true;
            }
            else
            {
                // There is no (more) treasure in this room.
                return false;
            }
        }

        public bool isExit()
        {
            return exit;
        }

        internal void makeExit()
        {
            exit = true;
        }
        #endregion
    }
}
