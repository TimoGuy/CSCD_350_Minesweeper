using System;
using System.IO;

//Max Iniguez, Austen Frostad, Timothy Bennett

namespace MineSweeperGroup
{
    class MineSweeper
    {
        static void Main(string[] args)
        {
            

            int[] currentFieldDimensions = GetFieldDimensionViaRedirect();
            

            int fieldNumber = 1;
            int bufferForAvoidingArrayBounds=2;
            bool moreFieldsinInputFile = true;
            bool dontPrintLineAtFirst = false;
            while (moreFieldsinInputFile)
            {

               
                char[,] currentField = CreateMapWithRedirect((currentFieldDimensions[0] + bufferForAvoidingArrayBounds), (currentFieldDimensions[1] + bufferForAvoidingArrayBounds));
                char [,] mineFieldCounted = CountMines(currentField, currentFieldDimensions[0], currentFieldDimensions[1]);
                if (dontPrintLineAtFirst) {
                    Console.WriteLine();
                }
                dontPrintLineAtFirst = true;
                Console.WriteLine("field #" + fieldNumber);

                PrintMineField(mineFieldCounted, currentFieldDimensions[0], currentFieldDimensions[1]);

                Console.WriteLine();
                fieldNumber++;

                currentFieldDimensions = GetFieldDimensionViaRedirect();
                if (currentFieldDimensions[0] == 0 && currentFieldDimensions[1] == 0) {
                    moreFieldsinInputFile = false;
                }

            }

        }

        private static char[,] CountMines(char[,] currentField, int row, int col)
        {
            char[,] numeratedMineField = new char[row,col];
            int count = 0;

            for (int i = 0, fieldI = 1; i < row; i++, fieldI++)
            {

                for (int j = 0, fieldJ = 1; j < col; j++, fieldJ++)
                {
                    count = 0;
                    //using chracter values to do math
                    if (currentField[fieldI,fieldJ] == '*') { count = -6; }

                    else
                    {

                        currentField = IncrementIfMine(currentField, fieldI, fieldJ, count);
                    }


                    numeratedMineField[i,j] = (char)(count + 48);

                }
            }


            return numeratedMineField;
        }

        private static char[,] IncrementIfMine(char[,] currentMineField, int fieldI, int fieldJ, int count) 
        {
           

                if (currentMineField[fieldI - 1, fieldJ - 1] == '*') { count++; }
                if (currentMineField[fieldI - 1, fieldJ] == '*') { count++; }
                if (currentMineField[fieldI - 1, fieldJ + 1] == '*') { count++; }
                           
                if (currentMineField[fieldI, fieldJ - 1] == '*') { count++; }
                if (currentMineField[fieldI, fieldJ + 1] == '*') { count++; }
                          
                if (currentMineField[fieldI + 1, fieldJ - 1] == '*') { count++; }
                if (currentMineField[fieldI + 1, fieldJ] == '*') { count++; }
                if (currentMineField[fieldI + 1, fieldJ + 1] == '*') { count++; }


            return currentMineField;
           
        }

        private static void PrintMineField(char[,] currentField, int i, int j)
        {
            for (int row = 0; row < i; row++)
            {

                for (int col = 0; col < j; col++)
                {

                   Console.Write(currentField[row,col]);
                }
                Console.WriteLine();
            }
        }

     
        private static int[] GetFieldDimensionViaRedirect() //returns x and y dimensions of minefield
        {
            int[] dimensions = new int[2];
            String[] dimensionLine = Console.ReadLine().Split(' ');
            dimensions[0] = int.Parse(dimensionLine[0]);
            dimensions[1] = int.Parse(dimensionLine[1]);

            return dimensions;
        }

        public static char[,] CreateMineFieldArray(int xDimension, int yDimension)
        {
            char[,] newMineMap = new char[xDimension, yDimension];
            char c;

            for (int row = 1; row < xDimension - 1; row++)
            {
                char[] currentRow = Console.ReadLine().ToCharArray();

                for (int col = 1; col < yDimension - 1; col++)
                {

                    c = currentRow[col - 1];
                    newMineMap[row, col] = c;

                }

            }
            return newMineMap;
        }

        private static char[,] CreateMapWithRedirect(int xDimension, int yDimension)
        {

            char[,] newMineMap = CreateMineFieldArray(xDimension, yDimension);

            //add buffer characters
            //col 0
            for (int row = 0; row < xDimension; row++)
            {
                newMineMap[row,0] = '.';
            }

            //col j-1
            for (int row = 0; row < xDimension; row++)
            {
                newMineMap[row,yDimension - 1] = '.';
            }

            //row 0
            for (int col = 0; col < yDimension; col++)
            {
                newMineMap[0,col] = '.';
            }

            //row 0
            for (int col = 0; col < yDimension; col++)
            {
                newMineMap[xDimension - 1,col] = '.';
            }



            return newMineMap;
        }

       
    }
}
