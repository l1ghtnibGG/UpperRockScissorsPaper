using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UpperRockPapperScissors
{
    internal class Menu
    {
        public static void Main(string[] args)
        {
            new Logic().Start(args);
        }

        public void CheckArgs(string[] args)
        {
            if (args.Length <= 1 || args.Length % 2 == 0)
            {
                Console.WriteLine("Wrong amount of arguments. Their should be > 1 and odds length. " +
                    "For example: rock scissors paper; 1 2 3 4 5. Try again.");
                Environment.Exit(0);
            }
        }

        public void ShowMenu(string[] args)
        {
            Console.WriteLine("Available moves: ");

            for (var i = 0; i < args.Length; i++)
            {
                Console.WriteLine(i + 1 + " - " + $"{args[i]}");
            }

            Console.WriteLine("0 - exit");
            Console.WriteLine("100 - help");
        }

        public string UserInput(string[] args)
        {
            var userMotion = Input();

            if (userMotion == 100)
            {
                ShowHelpMenu(args);
                Console.ReadKey();

                ShowMenu(args);
                userMotion = Input();
            }

            while (userMotion > args.Length || userMotion < 0 )
            {
                Console.WriteLine($"Wrong input. Number should be >= 0 and <= {args.Length} or 100.  Try again.");
                userMotion = Input();
            }

            if (userMotion == 0)
            {
                Console.WriteLine("Exit");
                Environment.Exit(0);
            }

            return args[userMotion - 1];
        }

        private int Input()
        {
            int x;

            while (!int.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("Wrong number. Try again.");
            }

            return x;
        }

        private void ShowHelpMenu(string[] args)
        {
            var table = new ConsoleTable();
            var logic = new Logic();

            var ruleMatrix = logic.FillRuleMatrix(args.Length);

            var column = GetColumn(args);

            table.AddColumn(column);

            for (var i = 0; i < args.Length; i++)
            {
                var row = GetRow(args, ruleMatrix, logic, i);
                table.AddRow(row);
            }

            table.Write();   
        }

        private string[] GetColumn(string[] args)
        {
            var column = new string[args.Length + 1];
            Array.Copy(args, 0, column, 1, args.Length);
            column[0] = "User\\PC";

            return column;
        }

        private string[] GetRow(string[] args, int[,] ruleMatrix, Logic logic, int i)
        {
            var result = new string[args.Length + 1];
            var temp = new string[args.Length];

            for (int j = 0; j < args.Length; j++)
            {
                var t = ruleMatrix[i, j];
                temp[j] = logic.Result(t);
            }

            Array.Copy(temp, 0, result, 1, args.Length);
            result[0] = args[i];

            return result;
        }
    }
}
