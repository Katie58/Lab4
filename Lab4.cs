using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_NUMBER_4
{
    class Program//fix, when choosing to enter a new number after entering an invalid number, sends to exit
    {
        public static void Main(string[] args)
        {
            int index = 0;
            int input = 0;
            List<int> inputList = new List<int>();
            string userName = "";
            Boolean retry = true;

            UserInfo(ref userName);
            while (retry)
            {
                Boolean print = true;
                Boolean add = true;

                Console.Clear();
                Greeting(userName);
                UserInput(ref add, ref retry, ref print, ref index, ref input, userName, ref inputList);
                UserInputStorage(add, ref index, input, ref inputList);
                PrintResults(ref retry, ref print, index, input, inputList, userName);
                retry = Retry(ref retry, ref print, userName, ref inputList, ref index);
            }
            Exit(userName);
        }

        public static string UserInfo(ref string userName)
        {
            Console.WriteLine("HI! What is your name? ");
            userName = Console.ReadLine();
            return userName;
        }

        public static void Greeting(string userName)
        {
            Console.WriteLine("Welcome {0}, Learn your squares and cubes!\nEnter an integer: ", userName);
        }

        public static void UserInput(ref bool retry, ref bool print, ref bool add, ref int index, ref int input, string userName, ref List<int> inputList)
        {
            add = true;//sets whether to store input
            string userInput = Console.ReadLine();
            index = index + 1;
            if (int.TryParse(userInput, out input) && input >= 0)
            {
                add = true;
            }
            else
            {
                if (userInput.All(Char.IsDigit))
                {
                    Console.WriteLine("The integer you entered is too large");
                }
                Invalid(ref retry, ref print, userName, ref inputList, ref index);
                add = false;
            }
        }

        public static List<int> UserInputStorage(bool add, ref int index, int input, ref List<int> inputList)
        {
            if (add)
            {
                inputList.Insert(index - 1, input);
                return inputList;
            }
            else
            {
                index = index - 1;
                return null;
            }

        }

        public static void PrintResults(ref bool retry, ref bool print, int index, int input, List<int> inputList, string userName)
        {
            if (print)
            {
                string header1 = "Number";
                string header2 = "Squared";
                string header3 = "Cubed";
                int maxWidth = 15;
                bool printHeader = true;

                int padding = 10;//padding between columns

                double numberMax = inputList.Max();//get largest number entered (for spacing)

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

                int placeValueHeader1 = header1.ToString().Length;//place value for headers
                int placeValueHeader2 = header2.ToString().Length;
                int placeValueHeader3 = header3.ToString().Length;

                string headerSpacer1 = new string(' ', placeValueMax1 + (placeValueMax1 - placeValueHeader1) + padding);//spacing for headers
                string headerSpacer2 = new string(' ', placeValueMax2 + (placeValueMax2 - placeValueHeader2) + padding);
                string headerSpacer3 = new string(' ', placeValueMax3 + (placeValueMax3 - placeValueHeader3) + padding);

                for (int i = 0; i < index; i++)
                {
                    double numberCurrent = inputList[i];//current number
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
                        Console.WriteLine(header1 + headerSpacer1 + header2 + headerSpacer2 + header3 + headerSpacer3);
                        Console.WriteLine(divideColumn1 + divideColumn2 + divideColumn3);
                        printHeader = false;
                    }

                    Console.WriteLine(number + columnSpacer1 + squared + columnSpacer2 + cubed);//print input rows
                }
            }
        }

        public static void Invalid(ref bool retry, ref bool print, string userName, ref List<int> inputList, ref int index)
        {
            Console.WriteLine("\nERROR - Sorry {0}, your input is invalid...", userName);
            Retry(ref retry, ref print, userName, ref inputList, ref index);
        }

        public static Boolean Retry(ref bool retry, ref bool print, string userName, ref List<int> inputList, ref int index)
        {
            if (print)
            {
                Console.WriteLine("\nWould you like to add a new number, {0}? (y/n): ", userName);
                char answer = Console.ReadKey().KeyChar;
                if (answer == 'Y' || answer == 'y')
                {
                    retry = true;
                    print = false;
                }
                else if (answer == 'N' || answer == 'n')
                {
                    ClearData(ref retry, ref print, userName, ref inputList, ref index);
                }
                else
                {
                    Invalid(ref retry, ref print, userName, ref inputList, ref index);
                }
            }
            return retry;
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

        public static void ClearData(ref bool retry, ref bool print, string userName, ref List<int> inputList, ref int index)
        {
            Console.WriteLine("Would you like to clear the data? (y/n)");
            ConsoleKeyInfo answer = Console.ReadKey(true);
            string strAnswer = answer.KeyChar.ToString();
            if (strAnswer == "Y" || strAnswer == "y")
            {
                inputList.Clear();
                inputList.TrimExcess();
                index = 0;
                Retry(ref retry, ref print, userName, ref inputList, ref index);
            }
            else if (strAnswer == "N" || strAnswer == "n")
            {
                retry = false;
            }
            else
            {
                Invalid(ref retry, ref print, userName, ref inputList, ref index);
            }
        }
    }
}//October 10, 2018
