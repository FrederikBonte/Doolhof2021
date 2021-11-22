using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Room
    {
        // There are four possible directions to move from Room to Room.
        private const int NORTH = 0;
        private const int EAST = 1;
        private const int SOUTH = 2;
        private const int WEST = 3;

        // Each room has four possible connections.
        private Room[] connections = new Room[4];

        // Can the maze be exited from this room?
        private bool exit;
        // Is there (currently) a treasure in this room?
        private bool treasure;

        #region Navigation
        /// <summary>
        /// Returns true when there is a connection in the provided direction.
        /// </summary>
        /// <param name="direction">The direction in which to move.</param>
        /// <returns>true when there is a connection.</returns>
        private bool canGo(int direction)
        {
            return connections[direction] != null;
        }

        /// <summary>
        /// Returns the room that lies in that direction.
        /// Throws an exception when you cannot move in that direction.
        /// Please check with the {@link canGoNorth()} function for example.
        /// </summary>
        /// <param name="direction">The direction in which to move.</param>
        /// <returns></returns>
        private Room go(int direction)
        {
            if (!canGo(direction))
            {
                throw new Exception("Illegal move no connection there.");
            }
            return connections[direction];
        }

        public bool canGoEast()
        {
            return canGo(EAST);
        }

        public Room goEast()
        {
            return go(EAST);
        }

        public bool canGoWest()
        {
            return canGo(WEST);
        }

        public Room goWest()
        {
            return go(WEST);
        }

        public bool canGoNorth()
        {
            return canGo(NORTH);
        }

        public bool canGoSouth()
        {
            return canGo(SOUTH);
        }

        public Room goNorth()
        {
            return go(NORTH);
        }

        public Room goSouth()
        {
            return go(SOUTH);
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
        #endregion
    }
}
