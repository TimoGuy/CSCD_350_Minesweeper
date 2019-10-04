import java.io.FileNotFoundException;
import java.io.PrintStream;
import java.util.Random;
import java.util.Scanner;

public class testfileBuilder {
    public static PrintStream outputFile;
    public static String inputFileName = "input.txt";
    public static void  main(String [] args){
        String inputFile="input.txt";
        boolean go =true;
        int rows;
        int cols;
        int goInt;

        getOutputFile();

        while (go) {

            Scanner input = new Scanner(System.in);

            System.out.print("Enter Dimensions, How many rows?: ");
            rows = input.nextInt();
            System.out.println();
            System.out.print("Enter Dimensions, How many columns?: ");
            cols = input.nextInt();

            makeMap(rows,cols);

            System.out.println("add another map? 0/1");
           // goInt = input.next();
            if ( input.nextInt()<1){
                go=false;
            }

        }
        outputFile.println("0 0");
        outputFile.close();

    }

    public static void makeMap(int rows, int cols){



        outputFile.println(rows+" "+cols);

        Random r= new Random();
        int percentage=34;

        for (int i =0; i<rows; i++){

            for (int j=0; j<cols; j++){

                if (r.nextInt()<percentage){
                    outputFile.print('*');
                }
                else {
                    outputFile.print('.');
                }

            }
            outputFile.println();
        }

    }


    public static void getOutputFile() {

        try {
            outputFile = new PrintStream(inputFileName);

        } catch (FileNotFoundException e) {
            System.out.print(e.getMessage());
        }

    }
}



