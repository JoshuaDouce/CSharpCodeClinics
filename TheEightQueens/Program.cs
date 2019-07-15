using System;

namespace TheEightQueens
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new bool[,] {
                { false, false, false, false, false, false, false, false },
                { false, false, false, false, false, false, false, false }};
            var solutionFound = true;

            var queenPositions = new Coordinate[8];

            //if solution not found
            //check board for available positions
            //if available position place queen
            //check horizontal
            //check vertical
            //check diagonal
            //if no attacks - place queen
            //if attacks - break and try again

        }
    }
}
