using System;
using System.Collections.Generic;

namespace WordleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Wordle Solver!");

            var dict = new Dictionary();
            var solver = new Solver(dict.PossibleWords());

            while (1 == 1)
            {
                Console.Write("Menu selection, type add, reset, or exit: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "exit":
                        return;
                    case "add":
                        AddWord(solver);
                        break;
                    case "reset":
                        dict.Reload();
                        solver = new Solver(dict.PossibleWords());
                        Console.WriteLine($"Successfully resetted environment! Total words {solver.Dictionary.Count}");
                        Console.Clear();
                        break;
                }
            }
        }

        static void AddWord(Solver solver)
        {
            string input;
            string inputword;
            var validinput = new List<string>
            {
                "correct", "absent", "present", "c", "a", "p"
            };
            Console.Write("Type word you want to add: ");
            input = Console.ReadLine();
            inputword = input;
            var word = new Words(inputword);

            Console.WriteLine("Foreach letter type correct, absent, or present. For shorthand: c, a, or p");

            for (int i = 0; i < inputword.Length; i++)
            {
                Console.Write($"Status of letter '{inputword.Substring(i, 1)}': ");
                input = Console.ReadLine();

                // validation input
                if (!(validinput.Contains(input)))
                {
                    Console.WriteLine($"{input} is not a valid selection. Please enter correct, absent, or present");
                    i--;
                    continue;
                }

                // convert shorthand to full word
                if (input == "c") { input = "correct"; }
                else if (input == "a") { input = "absent"; }
                else if (input == "p") { input = "present"; }

                var isEnumParsed = Enum.TryParse(input, true, out Rule parsedEnumValue);

                word.AddRule(new Rules(inputword.Substring(i, 1), i, parsedEnumValue));
            }
            solver.AddWord(word);
            solver.ApplyRules();
            Console.WriteLine("");
            Console.WriteLine($"After applying all rules here are the top five words out of {solver.Dictionary.Count} ... ");
            solver.PrintTop(5);
            Console.WriteLine("");
        }
    }
}
