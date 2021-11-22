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

        public Player(string name, Room room)
        {
            this.name = name;
            this.room = room;
        }

        public string getName()
        {
            return name;
        }

        public Room getRoom()
        {
            return room;
        }
    }
}
