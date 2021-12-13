using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoolhofProject2021
{
    public class Player
    {
        // In which room is the player?
        private Room room;
        // Name of the player.
        private string name;
        // How much treasure does the player have?
        private int treasure;
        // What direction are we facing.
        private int direction;

        public Player(string name, Room room)
        {
            this.name = name;
            this.room = room;
            // @TODO: Choose random direction.
            this.direction = Direction.getRandomDirection();
        }

        public string getName()
        {
            return name;
        }

        public Room getRoom()
        {
            return room;
        }

        public void setDirection(int direction)
        {
            this.direction = direction;
        }

        public bool moveForward()
        {
            // Check if there is a connection in the current direction.
            if (room.canGo(direction))
            {
                // Actually move to that room.
                room = room.go(direction);
                // Player passed through the door.
                return true;
            }
            else
            {
                // Player is facing a wall...
                return false;
            }
        }

        public void turnLeft()
        {
            direction = Direction.getToTheLeft(direction);
        }

        public void turnRight()
        {
            direction = Direction.getToTheRight(direction);
        }

        public void turnBack()
        {
            direction = Direction.getOpposite(direction);
        }
    }
}
