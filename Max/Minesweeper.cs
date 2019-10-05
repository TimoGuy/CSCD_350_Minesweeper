using System;
using System.Collections.Generic;

// Max Iniguez, October 3rd 2019

namespace Minesweeper
{
    class Program
    {
        private static string GetSolvedSquare(char[,] grid, int row, int col)
        {
            if (grid[row + 1, col + 1] == '*') return "*";
            
            int count = 0;
            if (grid[row - 1 + 1,  col - 1 + 1] == '*') count++;
            if (grid[row - 1 + 1,  col + 1] == '*') count++;
            if (grid[row - 1 + 1,  col + 1 + 1] == '*') count++;
            if (grid[row + 1,  col - 1 + 1] == '*') count++;
            if (grid[row + 1,  col + 1 + 1] == '*') count++;
            if (grid[row + 1 + 1,  col - 1 + 1] == '*') count++;
            if (grid[row + 1 + 1,  col + 1] == '*') count++;
            if (grid[row + 1 + 1,  col + 1 + 1] == '*') count++;

            return count.ToString();
        }

        static void Main(string[] args)
        {
            bool running = true;
            List<char[,]> mineGrids = new List<char[,]>();
            int n = -1, m = -1;
            while (running)
            {
                string gridDims = Console.ReadLine();
                if (gridDims != "0 0")
                {
                    n = int.Parse(gridDims.Split(' ')[0]);
                    m = int.Parse(gridDims.Split(' ')[1]);

                    mineGrids.Add(new char[n + 2, m + 2]);

                    for (int i = 0; i < n; i++)
                    {
                        string row = Console.ReadLine();
                        for (int j = 0; j < m; j++)
                        {
                            mineGrids[mineGrids.Count - 1][i + 1, j + 1] = row[j];
                        }
                    }
                }
                else
                {
                    running = false;
                }
            }

            for (int gridInd = 0; gridInd < mineGrids.Count; gridInd++)
            {
                if (gridInd > 0)
                {
                    Console.WriteLine();
                }

                Console.WriteLine("Field #{0}:", gridInd + 1);

                char[,] hintGrid = new char[mineGrids[gridInd].GetLength(0), mineGrids[gridInd].GetLength(1)];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        Console.Write(GetSolvedSquare(mineGrids[gridInd], i, j));
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
