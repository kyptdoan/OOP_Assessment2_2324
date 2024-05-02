using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int playerChoice;
            SevenOut sevenOut = new SevenOut();
            ThreeOrMore threeOrMore = new ThreeOrMore();

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("WELCOME TO DICE GAME PROGRAM.");
                Console.WriteLine("Please Press A Number (1-5).");
                Console.WriteLine("1. Play Seven Out Game.");
                Console.WriteLine("2. Play Three Or More Game.");
                Console.WriteLine("3. View Statistics of Seven Out Game.");
                Console.WriteLine("4. View Statistics of Three Or More Game.");
                Console.WriteLine("5. Exit.");

                while (true)
                {
                    try
                    {
                        playerChoice = int.Parse(Console.ReadLine());
                        if (playerChoice == 1 || playerChoice == 2 || playerChoice == 3 || playerChoice == 4 || playerChoice == 5)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Input 1 to 4 Integer Only.");
                            continue;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please Input 1 to 4 Integer Only.");
                        continue;
                    }
                }

                if (playerChoice == 1)
                {
                    sevenOut.Control();

                    Console.WriteLine("\n");
                    Console.WriteLine("What Would You Like To Do Next? Please Input An Input.");
                    continue;
                }
                else if (playerChoice == 2)
                {
                    threeOrMore.Control();

                    Console.WriteLine("\n");
                    Console.WriteLine("What Would You Like To Do Next? Please Input An Input.");
                    continue;
                }
                else if (playerChoice == 3)
                {
                    sevenOut.GetStatistic.ReportTimes();
                    sevenOut.GetStatistic.ReportPoints();
                    sevenOut.GetStatistic.ReportWinners();

                    Console.WriteLine("\n");
                    Console.WriteLine("What Would You Like To Do Next? Please Input An Input.");
                    continue;
                }
                else if (playerChoice == 4)
                {
                    threeOrMore.GetStatistics.ReportTimes();
                    threeOrMore.GetStatistics.ReportPoints();
                    threeOrMore.GetStatistics.ReportWinners();

                    Console.WriteLine("\n");
                    Console.WriteLine("What Would You Like To Do Next? Please Input An Input.");
                    continue;
                }
                else if (playerChoice == 5)
                {
                    Console.WriteLine("The Program Exits.");
                    break;
                }
            }
        }
    }
}
