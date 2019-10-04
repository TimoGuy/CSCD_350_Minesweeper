using System;
using System.IO;

namespace MineSweeperAF
{
    class MineSweeperAF
    {
        static void Main(string[] args)
        {
            //if (args.Length > 0) { 
            //string inputFilePath = OpenInputFile(args);
            //StreamReader inputReader = new StreamReader(inputFilePath);
            //int[] currentFieldDimensions = GetFieldDimension(inputReader);
            //}

            int[] currentFieldDimensions = GetFieldDimensionViaRedirect();
            

       
            int fieldNumber = 1;
            int bufferForAvoidingArrayBounds=2;
            bool moreFieldsinInputFile = true;

            while (moreFieldsinInputFile)
            {



                //char[,] currentField = CreateMap((currentFieldDimensions[0] + bufferForAvoidingArrayBounds), (currentFieldDimensions[1] + bufferForAvoidingArrayBounds), inputReader);
                char[,] currentField = CreateMapWithRedirect((currentFieldDimensions[0] + bufferForAvoidingArrayBounds), (currentFieldDimensions[1] + bufferForAvoidingArrayBounds));
                char [,] mineFieldCounted = CountMines(currentField, currentFieldDimensions[0], currentFieldDimensions[1]);
                Console.WriteLine();
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

                    if (currentField[fieldI,fieldJ] == '*') { count = -6; }

                    else
                    {

                        if (currentField[fieldI - 1,fieldJ - 1] == '*') { count++; }
                        if (currentField[fieldI - 1,fieldJ] == '*') { count++; }
                        if (currentField[fieldI - 1,fieldJ + 1] == '*') { count++; }

                        if (currentField[fieldI,fieldJ - 1] == '*') { count++; }
                        if (currentField[fieldI,fieldJ + 1] == '*') { count++; }

                        if (currentField[fieldI + 1,fieldJ - 1] == '*') { count++; }
                        if (currentField[fieldI + 1,fieldJ] == '*') { count++; }
                        if (currentField[fieldI + 1,fieldJ + 1] == '*') { count++; }
                    }


                    numeratedMineField[i,j] = (char)(count + 48);

                }
            }


            return numeratedMineField;
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

        //private static int[] GetFieldDimensionViaFile(StreamReader inputReader) //returns x and y dimensions of minefield
        //{
        //    int[] dimensions = new int[2];
        //    String[] dimensionLine = inputReader.ReadLine().Split(' ');
        //    dimensions[0] = int.Parse(dimensionLine[0]);
        //    dimensions[1] = int.Parse(dimensionLine[1]);

        //    return dimensions;
        //}



        private static int[] GetFieldDimensionViaRedirect() //returns x and y dimensions of minefield
        {
            int[] dimensions = new int[2];
            String[] dimensionLine = Console.ReadLine().Split(' ');
            dimensions[0] = int.Parse(dimensionLine[0]);
            dimensions[1] = int.Parse(dimensionLine[1]);

            return dimensions;
        }

        //private static char[,] CreateMap(int xDimension, int yDimension, StreamReader inputReader)
        //{
        //    char[,] newMineMap = new char[xDimension,yDimension];
        //    char c;

        //    for (int row = 1; row < xDimension - 1; row++)
        //    {
        //        char[] currentRow = inputReader.ReadLine().ToCharArray();

        //        for (int col = 1; col < yDimension - 1; col++)
        //        {

        //            c = currentRow[col - 1];
        //            newMineMap[row,col] = c;

        //        }

        //    }

        private static char[,] CreateMapWithRedirect(int xDimension, int yDimension)
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

        private static string OpenInputFile(string[] args)
        {
            string inputFilePath = @"C:\Users\saffron\source\repos\MineSweeperAF\input.txt";
            if (args.GetLength(0) > 0)
            {
                inputFilePath = args[0];
            }
            Console.WriteLine(inputFilePath);


            if (!System.IO.File.Exists(inputFilePath))
            {
                
                throw new System.IO.FileNotFoundException("the input file: \"" + inputFilePath + "\" could not be found.");
            }

            return inputFilePath;
        }
    }
}
