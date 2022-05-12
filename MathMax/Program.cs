using System;
using System.Text.RegularExpressions;
using System.Linq;

//-I take the input and convert to 2D array (read from file is also included) -Removed spaces and empty lines -Check all the 2D matrix if the there is'Prime Numbers' -Make Prime numbers '-1' -Moving downwards by calculating the max possible ways
//A: You will have an orthogonal triangle input from a file and you need to find the maximum sum of the numbers according to given rules below;
//You will start from the top and move downwards to an adjacent number as in below.
//You are only allowed to walk downwards and diagonally.
//You can only walk over NON PRIME NUMBERS.
//You have to reach at the end of the pyramid as much as possible.
//You have to treat the input as pyramid.
//According to above rules the maximum sum of the numbers from top to bottom in below example is 24.

// *1
// *8 4
//2 *6 9
//8 5 *9 3
//As you can see this has several paths that fits the rule of NOT PRIME NUMBERS; 1 > 8 > 6 > 9, 1 > 4 > 6 > 9, 1 > 4 > 9 > 9 1 + 8 + 6 + 9 = 24.As you see 1, 8, 6, 9 are all NOT PRIME NUMBERS and walking over these yields the maximum sum.

//B: According to assignment in 3 that you implemented what is the maximum sum of below input? It means please take this input (as file or constants directly inside the code) for your implementation and solve by using it.

//215
//193 124
//117 237 442
//218 935 347 235
//320 804 522 417 345
//229 601 723 835 133 124
//248 202 277 433 207 263 257
//359 464 504 528 516 716 871 182
//461 441 426 656 863 560 380 171 923
//381 348 573 533 447 632 387 176 975 449
//223 711 445 645 245 543 931 532 937 541 444
//330 131 333 928 377 733 017 778 839 168 197 197
//131 171 522 137 217 224 291 413 528 520 227 229 928
//223 626 034 683 839 053 627 310 713 999 629 817 410 121
//924 622 911 233 325 139 721 218 253 223 107 233 230 124 233

namespace CalculateMaxPathSum
{
    public static class Program
    {

        public static void Main(string[] args)
        {

            InputExDisplay();

            int[,] convertedTriangle2 = input2.ConvertTo2DArrayWithoutSpaces();
            int maxSum2 = convertedTriangle2.MoveDownwards();

            Console.WriteLine("The result for B : " + maxSum2);
            if (maxSum2 == 0 || maxSum2 == -1)
                Console.WriteLine("There is no possible path");
            Console.ReadKey();



        }
        //Solution A.)
        private static void InputExDisplay()
        {
            var triInput =
                       @"1
				        8 4
					   2 6 9
					  8 5 9 3";
        var splInput = ConvertStringArray(triInput);
        var DimensionalArray = BuildArray(splInput);
        var PrimeNumbersArray = PrimesNumberstoZero(DimensionalArray);
        var MaxSum = MaxArray(DimensionalArray);

        Console.WriteLine($"The result for A : {MaxSum}");

        }

        private static int[,] BuildArray(string[] splArray)
        {
            var DimensionalArray = new int[splArray.Length, splArray.Length + 1];
            var rowIndex = 0;
            foreach (var row in splArray)
            {
                var intArray = row.ConvertStringIntegerArray();
                var columnIndex = 0;
                foreach (var integer in intArray)
                {
                    DimensionalArray[rowIndex, columnIndex] = integer;
                    columnIndex++;
                }
                rowIndex++;
            }
            return DimensionalArray;
        }
        private static int[,] PrimesNumberstoZero(int[,] DimensionalArray)
        {
            var length = DimensionalArray.GetLength(0);
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    if (DimensionalArray[i, j] == 0) continue;
                    if (Prime(DimensionalArray[i, j]))
                        DimensionalArray[i, j] = 0;
                }
            }
            return DimensionalArray;

        }


        private static string[] ConvertStringArray(string triInput)
        {
            return triInput.Split('\n');
        }

        private static int[] ConvertStringIntegerArray(this string rows)
        {
            return
                Regex
                .Matches(rows, "[0-9]+")
                .Cast<Match>()
                .Select(m => int.Parse(m.Value)).ToArray();
        }

        public static bool Prime(int number)
        {

            if (number == 0 || number == 1) return false;
            return Enumerable.Range(2, (int)Math.Sqrt(number) - 1).All(divisor => number % divisor != 0);

        }

        private static int MaxArray(int[,] DimensionalArray)
        {
            var data = DimensionalArray;
            var length = DimensionalArray.GetLength(0);

            for (var i = length - 2; i >= 0; i--)
            {
                for (var j = 0; j < length; j++)
                {
                    var c = data[i, j];
                    var b = data[i + 1, j];
                    var a = data[i + 1, j + 1];

                    if ((!Prime(c) && !Prime(a)) || (!Prime(c) && !Prime(b)))
                        DimensionalArray[i, j] = c + Math.Max(a, b);
                }
            }
            return DimensionalArray[0, 0];
        }

        //Solution Finished A.)




        //Solutıon B.)
        private const string input2 = @" 215
                                         193 124
                                         117 237 442
                                         218 935 347 235
                                         320 804 522 417 345
                                         229 601 723 835 133 124
                                         248 202 277 433 207 263 257
                                         359 464 504 528 516 716 871 182
                                         461 441 426 656 863 560 380 171 923
                                         381 348 573 533 447 632 387 176 975 449
                                         223 711 445 645 245 543 931 532 937 541 444
                                         330 131 333 928 377 733 017 778 839 168 197 197
                                         131 171 522 137 217 224 291 413 528 520 227 229 928
                                         223 626 034 683 839 053 627 310 713 999 629 817 410 121
                                         924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";


        private static int MoveDownwards(this int[,] matrixOfTriangle)
        {
            int length = matrixOfTriangle.GetLength(0);

            int res = -1;
            for (int i = 0; i < length - 2; i++)
                res = Math.Max(res, matrixOfTriangle[0, i]);

            for (int i = 1; i < length; i++)
            {
                res = -1;
                for (int j = 0; j < length; j++)
                {
                    if (j == 0 && matrixOfTriangle[i, j] != -1)
                    {
                        if (matrixOfTriangle[i - 1, j] != -1)
                            matrixOfTriangle[i, j] += matrixOfTriangle[i - 1, j];
                        else
                            matrixOfTriangle[i, j] = -1;
                    }
                    else if (j > 0 && j < length - 1 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNodeValue(matrixOfTriangle[i - 1, j],
                                   matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }

                    else if (j > 0 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNodeValue(matrixOfTriangle[i - 1, j],
                                         matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }
                    else if (j != 0 && j < length - 1 && matrixOfTriangle[i, j] != -1)
                    {
                        int tmp = CalculateNodeValue(matrixOfTriangle[i - 1, j],
                                     matrixOfTriangle[i - 1, j - 1]);
                        if (tmp == -1)
                        {
                            matrixOfTriangle[i, j] = -1;
                        }
                        else
                            matrixOfTriangle[i, j] += tmp;
                    }
                    res = Math.Max(matrixOfTriangle[i, j], res);
                }
            }
            return res;
        }

        private static int[,] ConvertTo2DArrayWithoutSpaces(this string input)
        {
            string[] array = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);  //remove spaces

            int[,] matrixOfTriangle = new int[array.Length, array.Length + 1];      //converting to a matrix

            for (var row = 0; row < array.Length; row++)
            {
                int[] digitsInRow = Regex.Matches(array[row], "[0-9]+")
                    .Cast<Match>()
                    .Select(m => int.Parse(m.Value)).ToArray();

                //  int[] digitsInRow = array[row].Split(' ').Select(int.Parse).ToArray();
                for (var column = 0; column < digitsInRow.Length; column++)
                {
                    matrixOfTriangle[row, column] = digitsInRow[column];
                }
            }
            return matrixOfTriangle.RemovePrimeNumbers();  // checks non-prime numbers and returns them from converted array
        }

        private static int CalculateNodeValue(int input1, int input2)  //returns max value
        {
            if (input1 == -1 && input2 == -1 || input1 == 0 && input2 == 0)
                return -1;
            else
                return Math.Max(input1, input2);
        }

        private static bool isPrime(this int number)
        {
            if ((number & 1) == 0)
            {
                if (number == 2)
                {
                    return true;
                }
                return false;
            }
            for (var i = 3; (i * i) <= number; i += 2)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return number != 1;
        }

        private static int[,] RemovePrimeNumbers(this int[,] matrixOfTriangle)
        {
            int length = matrixOfTriangle.GetLength(0);
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    if (matrixOfTriangle[i, j] == 0)
                    {
                        continue;
                    }
                    else if (isPrime(matrixOfTriangle[i, j]))       //replacing prime numbers with -1 in matrix
                    {
                        matrixOfTriangle[i, j] = -1;
                    }
                }
            }
            return matrixOfTriangle;
        }
    }
    //Solution finished B.)
}
