using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab4
{
    class Program//fix, when choosing to enter a new number after entering an invalid number, sends to exit
    {
        public static void Main(string[] args)
        {
            string userName = null;

            User(ref userName);
            DimCalc(userName);
        }

        public static void User(ref string userName)
        {
            Console.WriteLine("HI! What is your name? ");
            userName = Console.ReadLine();
        }

        public static void DimCalc(string userName)
        {

            int index = 0;
            List<int> inputs = new List<int>();
            bool retry = true;

            while (retry)
            {
                Console.Clear();

                UserInput(userName, ref index, ref inputs);
                PrintResults(index, inputs);
                retry = Retry(ref retry, userName, ref inputs, ref index);
            }
            Exit(userName);
        }

        public static void UserInput(string userName, ref int index, ref List<int> inputs)
        {
            Console.Write("Welcome {0}, Learn your squares and cubes!\nEnter an integer: ", userName);
            string input = Console.ReadLine();////////////////////////////////get input from user
            if (int.TryParse(input, out int inputValid) && inputValid >= 0)//check input is valid
            {
                index = index + 1;
                inputs.Insert(index - 1, inputValid);///////////////////////////add input to list
            }
            else if (input.All(Char.IsDigit))/////////////check if invalid input is valid integer
            {
                Console.WriteLine("The integer you entered is too large");
            }
            else
            {
                Console.WriteLine("Invalid Input, Try Again...");
                UserInput(userName, ref index, ref inputs);
            }
        }

        public static void PrintResults(int index, List<int> inputs)
        {
            string[] title = new string[3] { "Number", "Squared", "Cubed" };//////////title to list

            bool printHeader = true;//only print header on first iteration
            int maxWidth = 15;//place value of largest number possible
            int padding = 10;//padding between columns

            double numberMax = inputs.Max();//get largest number entered (for spacing)

            double columnCharSpace1 = numberMax;//calculate and convert max number for each column
            double columnCharSpace2 = Math.Pow(numberMax, 2);
            double columnCharSpace3 = Math.Pow(numberMax, 3);

            int placeValueMax1 = columnCharSpace1.ToString().Length;//find place value for max number in each column
            if (placeValueMax1 > maxWidth)
            {
                placeValueMax1 = maxWidth;
            }
            int placeValueMax2 = columnCharSpace2.ToString().Length;
            if (placeValueMax2 > maxWidth)
            {
                placeValueMax2 = maxWidth;
            }
            int placeValueMax3 = columnCharSpace3.ToString().Length;
            if (placeValueMax3 > maxWidth)
            {
                placeValueMax3 = maxWidth;
            }

            int placeValueHeader1 = title[0].ToString().Length;//place value for headers
            int placeValueHeader2 = title[1].ToString().Length;
            int placeValueHeader3 = title[2].ToString().Length;

            string headerSpacer1 = new string(' ', placeValueMax1 + (placeValueMax1 - placeValueHeader1) + padding);//spacing for headers
            string headerSpacer2 = new string(' ', placeValueMax2 + (placeValueMax2 - placeValueHeader2) + padding);
            string headerSpacer3 = new string(' ', placeValueMax3 + (placeValueMax3 - placeValueHeader3) + padding);

            for (int i = 0; i < index; i++)
            {
                double numberCurrent = inputs[i];//current number
                double numberCurrentSquared = Math.Pow(numberCurrent, 2);
                double numberCurrentCubed = Math.Pow(numberCurrent, 3);

                string number = Convert.ToString(numberCurrent);
                string squared = Convert.ToString(numberCurrentSquared);
                string cubed = Convert.ToString(numberCurrentCubed);

                double columnCharSpaceCurrent1 = numberCurrent;//calculate and convert current number for each column
                double columnCharSpaceCurrent2 = Math.Pow(numberCurrent, 2);
                double columnCharSpaceCurrent3 = Math.Pow(numberCurrent, 3);

                int placeValueCurrent1 = columnCharSpaceCurrent1.ToString().Length;//find place value for current number and change large numbers to scientific notation
                if (placeValueCurrent1 > maxWidth)
                {
                    placeValueCurrent1 = maxWidth;
                    number = string.Format("{0:#.####E+00}", numberCurrent);
                }
                int placeValueCurrent2 = columnCharSpaceCurrent2.ToString().Length;
                if (placeValueCurrent2 > maxWidth)
                {
                    placeValueCurrent2 = maxWidth;
                    squared = string.Format("{0:#.####E+00}", numberCurrentSquared);
                }
                int placeValueCurrent3 = columnCharSpaceCurrent3.ToString().Length;
                if (placeValueCurrent3 > maxWidth)
                {
                    placeValueCurrent3 = maxWidth;
                    cubed = string.Format("{0:#.####E+00}", numberCurrentCubed);
                }

                string columnSpacer1 = new string(' ', placeValueMax1 + (placeValueMax1 - placeValueCurrent1) + padding);//total spaces added to first column
                string columnSpacer2 = new string(' ', placeValueMax2 + (placeValueMax2 - placeValueCurrent2) + padding);
                string columnSpacer3 = new string(' ', placeValueMax3 + (placeValueMax3 - placeValueCurrent3) + padding);
                string divideColumn1 = new string('=', placeValueMax1 + (placeValueMax1 - placeValueCurrent1) + placeValueHeader1 + padding);
                string divideColumn2 = new string('=', placeValueMax2 + (placeValueMax1 - placeValueCurrent1) + placeValueHeader2 + padding);
                string divideColumn3 = new string('=', placeValueMax3 + (placeValueMax1 - placeValueCurrent1) + placeValueHeader3);

                while (printHeader)//print header
                {
                    Console.WriteLine(Environment.NewLine);
                    Console.WriteLine(title[0] + headerSpacer1 + title[1] + headerSpacer2 + title[2] + headerSpacer3);
                    Console.WriteLine(divideColumn1 + divideColumn2 + divideColumn3);
                    printHeader = false;
                }

                Console.WriteLine(number + columnSpacer1 + squared + columnSpacer2 + cubed);//print input rows
            }
        }

        public static void Invalid(string userName)
        {
            Console.WriteLine("\nERROR - Sorry {0}, your input is invalid...", userName);
        }

        public static bool Retry(ref bool retry, string userName, ref List<int> inputList, ref int index)
        {
            Console.WriteLine("\nWould you like to add a new number, {0}? (y/n): ", userName);
            char answer = Console.ReadKey().KeyChar;
            if (answer == 'Y' || answer == 'y')
            {
                retry = true;
            }
            else if (answer == 'N' || answer == 'n')
            {
                ClearData(ref retry, userName, ref inputList, ref index);
            }
            else
            {
                Invalid(userName);
            }
            return retry;
        }

        public static void ClearData(ref bool retry, string userName, ref List<int> inputs, ref int index)
        {
            Console.WriteLine("Would you like to clear the data? (y/n)");
            ConsoleKeyInfo answer = Console.ReadKey(true);
            string strAnswer = answer.KeyChar.ToString();
            if (strAnswer == "Y" || strAnswer == "y")
            {
                inputs.Clear();
                inputs.TrimExcess();
                index = 0;
                Retry(ref retry, userName, ref inputs, ref index);
            }
            else if (strAnswer == "N" || strAnswer == "n")
            {
                retry = false;
            }
            else
            {
                Invalid(userName);
            }
        }

        public static void Exit(string userName)
        {
            Console.WriteLine("\nGoodbye {0}, Press ESCAPE to Exit", userName);

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Console.ReadKey();
                continue;
            }
        }
    }
}//October 10, 2018 - revised October 17, 2018
