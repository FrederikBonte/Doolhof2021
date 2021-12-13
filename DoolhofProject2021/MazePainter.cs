using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoolhofProject2021
{
    public partial class MazePainter : UserControl
    {
        private const int SCALE = 26;
        private Maze maze;


        public MazePainter()
        {
            InitializeComponent();
        }

        private void MazePainter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            Pen pen = new Pen(Color.Black);

            // Loop through all the rooms...
            // Draw that room...
            for (int x = 0; x < maze.getWidth(); x++)
            {
                for (int y = 0; y < maze.getHeight(); y++)
                {
                    drawRoom(g, pen, maze.getRoom(x, y));
                }
            }

            maze.drawPath(g, SCALE);
        }

        private void drawRoom(Graphics g, Pen pen, Room room)
        {
            Pen w = new Pen(Color.White);
            // Always draw a room square.
            g.DrawRectangle(pen, room.getX() * SCALE, room.getY() * SCALE, SCALE, SCALE);
            if (room.canGoWest())
            {
                int rx = (room.getX() * SCALE) - 4;
                int ry = (room.getY() * SCALE) + 3;
                int rh = SCALE - (2 * 3);
                g.FillRectangle(w.Brush, rx, ry, 8, rh);
            }
            if (room.canGoNorth())
            {
                int rx = (room.getX() * SCALE) + 3;
                int ry = (room.getY() * SCALE) - 4;
                int rw = SCALE - (2 * 3);
                g.FillRectangle(w.Brush, rx, ry, rw, 8);
            }
        }

        private void MazePainter_Load(object sender, EventArgs e)
        {
            // Actually create a maze.
            // Assume some size...
            // Also DONT GENERATE STUFF!!!
            maze = new Maze(Width / SCALE, Height / SCALE, false);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!maze.isMazeDone())
            {
                maze.generateStep();
            }
            // Repaint the entire screen.
            Invalidate();
        }
    }
}
