using System;

namespace TheEightQueens
{
    class Program
    {
        private static bool[,] board = new bool[8, 8];
        private static int solutions = 0;
        static void Main(string[] args)
        {
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    IsSolution(i, j);                   
                }
            }

            Console.WriteLine($"Solutions Found: {solutions}");
        }

        private static void IsSolution(int x, int y)
        {
            board[x, y] = true;

            var count = 1;
            //search board for position
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //if free
                    if (board[i, j] == false)
                    {
                        //check attacks
                        var attacked = CheckForAttacks(i, j);

                        //if not attacked place queen
                        if (!attacked)
                        {
                            board[i, j] = true;
                            i = 0;
                            j = 0;
                            count++;
                        }
                    }
                }
            }

            if (count == 8)
            {
                for (int i = 0; i <= 7; i++)
                {
                    for (int j = 0; j <= 7; j++)
                    {
                        if (board[i, j])
                        {
                            Console.Write("| X |");
                        }
                        else
                        {
                            Console.Write("| - |");
                        }
                    }
                    Console.WriteLine();
                }

                solutions++;
            }

            board = new bool[8, 8];
        }

        private static bool CheckForAttacks(int x, int y)
        {
            var attacked = false;

            if (!attacked)
            {
                attacked = CheckVerticals(y, attacked);
            }

            if (!attacked)
            {
                attacked = CheckHorizontals(x, attacked);
            }

            if (!attacked)
            {
                attacked = CheckDiagonals(x, y, attacked);
            }

            return attacked;
        }

        private static bool CheckDiagonals(int x, int y, bool attacked)
        {
            for (int i = x; i <= 7; i++)
            {
                var xpos = x;
                var ypos = y;

                while (xpos < 7 && ypos < 7)
                {
                    xpos++;
                    ypos++;
                    if (board[xpos, ypos])
                    {
                        attacked = true;
                    }
                }

                ResetXYPos(x, y, out xpos, out ypos);
                while (xpos > 0 && ypos > 0)
                {
                    xpos--;
                    ypos--;
                    if (board[xpos, ypos])
                    {
                        attacked = true;
                    }
                }

                ResetXYPos(x, y, out xpos, out ypos);
                while (xpos < 7 && ypos > 0)
                {
                    xpos++;
                    ypos--;
                    if (board[xpos, ypos])
                    {
                        attacked = true;
                    }
                }

                ResetXYPos(x, y, out xpos, out ypos);
                while (xpos > 0 && ypos < 7)
                {
                    xpos--;
                    ypos++;
                    if (board[xpos, ypos])
                    {
                        attacked = true;
                    }
                }
            }

            return attacked;
        }

        private static bool CheckHorizontals(int x, bool attacked)
        {
            for (int i = 0; i <= 7; i++)
            {
                if (board[x, i])
                {
                    attacked = true;
                }
            }

            return attacked;
        }

        private static bool CheckVerticals(int y, bool attacked)
        {
            for (int i = 0; i <= 7; i++)
            {
                if (board[i, y])
                {
                    attacked = true;
                }
            }

            return attacked;
        }

        private static void ResetXYPos(int x, int y, out int xpos, out int ypos)
        {
            xpos = x;
            ypos = y;
        }
    }
}
