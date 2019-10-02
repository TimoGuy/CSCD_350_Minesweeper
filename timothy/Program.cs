using System;
using System.Collections.Generic;

namespace CSCD_Minesweeper
{
    class MineGrid
    {
        int requestedRows, requestedCols;
        private char[,] _grid;

        public void CreateGrid(int rows, int columns)
        {
            requestedRows = rows;
            requestedCols = columns;

            // Buffed grid
            _grid = new char[requestedRows + 2, requestedCols + 2];
        }

        public void SetElement(char elem, int row, int col)
        {
            _grid[row + 1, col + 1] = elem;
        }

        public char GetElement(int row, int col)
        {
            return _grid[row + 1, col + 1];
        }

        public char[,] GetHintedGrid()
        {
            char[,] ret = new char[requestedRows, requestedCols];
            for (int i = 0; i < requestedRows; i++)
            {
                for (int j = 0; j < requestedCols; j++)
                {
                    ret[i, j] = GetSolvedSquare(i, j);
                }
            }

            return ret;
        }

        private char GetSolvedSquare(int row, int col)
        {
            if (GetElement(row, col) == '*')
            {
                // Hey I'm a mine
                // You're mine 笑笑
                return '*';
            }

            int count = 0;
            IncremIfMine(ref count, row - 1, col - 1);
            IncremIfMine(ref count, row - 1, col + 0);
            IncremIfMine(ref count, row - 1, col + 1);

            IncremIfMine(ref count, row + 0, col - 1);
            // Don't check yourself!
            IncremIfMine(ref count, row + 0, col + 1);
            
            IncremIfMine(ref count, row + 1, col - 1);
            IncremIfMine(ref count, row + 1, col + 0);
            IncremIfMine(ref count, row + 1, col + 1);
            return char.Parse(count.ToString());
        }

        private void IncremIfMine(ref int count, int row, int col)
        {
            if (GetElement(row, col) == '*')
            {
                count++;
            }
        }
    }

    class Program
    {
        private List<MineGrid> _mg;
        void ReadGrid(int index, int rows, int columns)
        {
            _mg[index].CreateGrid(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                // Read a row
                string row = Console.ReadLine();
                for (int j = 0; j < columns; j++)
                {
                    _mg[index].SetElement(row[j], i, j);
                }
            }
        }

        void ProcessGridInput()
        {
            bool running = true;
            _mg = new List<MineGrid>();
            while (running)
            {
                string gridDims = Console.ReadLine();
                if (gridDims != "0 0")
                {
                    // Read dimensions (EXPECT 0 < n,m <= 100)
                    int n = int.Parse(gridDims.Split(' ')[0]);
                    int m = int.Parse(gridDims.Split(' ')[1]);

                    _mg.Add(new MineGrid());
                    ReadGrid(_mg.Count - 1, n, m);
                }
                else
                {
                    // Exit
                    running = false;
                }
            }
        }

        void PrintOutput()
        {
            // オーイ、しつこいなー、これを翻訳してて
            for (int gridInd = 0; gridInd < _mg.Count; gridInd++)
            {
                if (gridInd > 0)
                {
                    // 新しいグリッドを計算
                    Console.WriteLine();
                }

                Console.WriteLine("Field #{0}:", gridInd + 1);
                char[,] hintGrid = _mg[gridInd].GetHintedGrid();
                for (int i = 0; i < hintGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < hintGrid.GetLength(1); j++)
                    {
                        Console.Write(hintGrid[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            
        }

        void Run()
        {
            ProcessGridInput();
            PrintOutput();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
