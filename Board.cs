using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ConwayRats
{
    internal class Board
    {
        public bool[,] state;
        private bool[,] next_state;
        public int grid_size;

        // Timer to execute the cycles, maxTime is the seconds per cycle
        public static double tickTime = 1d;
        public static double timer = tickTime;

        public Board(int grid_size)
        {
            state = new bool[grid_size, grid_size];
            next_state = new bool[grid_size, grid_size];
            this.grid_size = grid_size;
        }

        private int Get_live_neighbors(int x_cord, int y_cord)
        {
            int neighbors = 0;
            // These statements check to see if the neighboring cells, infinitely wrapping around the grid,
            // contain an active member, and increment the value accordingly
            if (state[((x_cord + grid_size) - 1) % grid_size, ((y_cord + grid_size) - 1) % grid_size]) { neighbors++; }
            if (state[((x_cord + grid_size) + 1) % grid_size, ((y_cord + grid_size) + 1) % grid_size]) { neighbors++; }
            if (state[((x_cord + grid_size) - 1) % grid_size, (y_cord + grid_size) % grid_size]) { neighbors++; }
            if (state[((x_cord + grid_size) + 1) % grid_size, (y_cord + grid_size) % grid_size]) { neighbors++; }
            if (state[(x_cord + grid_size) % grid_size, ((y_cord + grid_size) - 1) % grid_size]) { neighbors++; }
            if (state[(x_cord + grid_size) % grid_size, ((y_cord + grid_size) + 1) % grid_size]) { neighbors++; }
            if (state[((x_cord + grid_size) + 1) % grid_size, ((y_cord + grid_size) - 1) % grid_size]) { neighbors++; }
            if (state[((x_cord + grid_size) - 1) % grid_size, ((y_cord + grid_size) + 1) % grid_size]) { neighbors++; }

            return neighbors;
        }

        public void Cycle(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            timer -= dt;

            // Each time the timer ticks
            if (timer <= 0)
            {
                // This implements the rules of Conway's game of life
                for (int x = 0; x < grid_size; x++)
                {
                    for (int y = 0; y < grid_size; y++)
                    {
                        if (state[x, y])
                        {
                            if (Get_live_neighbors(x, y) < 2 || Get_live_neighbors(x, y) > 3)
                            {
                                next_state[x, y] = false;
                            }
                            else
                            {
                                next_state[x, y] = true;
                            }
                        }
                        else
                        {
                            if (Get_live_neighbors(x, y) == 3)
                            {
                                next_state[x, y] = true;
                            }
                            else
                            {
                                next_state[x, y] = false;
                            }
                        }
                    }
                }
                state = next_state;
                next_state = new bool[grid_size, grid_size];

                timer = tickTime;
            }

        }
    }
}
