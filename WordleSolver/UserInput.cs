using System;
using System.Collections.Generic;
using System.Text;

namespace WordleSolver
{
    public class UserInput
    {
        private Dictionary _dict;
        private Solver _solver;

        public UserInput() 
        {
            _dict = new Dictionary();
            _solver = new Solver(_dict.PossibleWords());
        }

        public void Start()
        {
            MenuSelection();
        }
        
        private void MenuSelection()
        {
            Console.WriteLine("Welcome to Wordle Solver!");

            while (1 == 1)
            {
                Console.Write("Menu selection, type add, reset, or exit: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "exit":
                        return;
                    case "add":
                        AddWord(_solver);
                        break;
                    case "reset":
                        _dict.Reload();
                        _solver = new Solver(_dict.PossibleWords());
                        Console.Clear();
                        Console.WriteLine($"Successfully resetted environment! Total words {_solver.Dictionary.Count}");
                        break;
                }
            }
        }

        private void AddWord(Solver solver)
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
            PrintResults(5);
        }

        private void PrintResults(int number)
        {
            var dictCount = _solver.Dictionary.Count;
            Console.WriteLine($"\nAfter applying all rules here are the top {number} words out of {dictCount} remaining ... ");

            int TopX = number < dictCount ? number : dictCount;
            for (int i = 0; i < TopX; i++)
            {
                Console.WriteLine(_solver.Dictionary[i]);
            }
            Console.WriteLine(""); // for spacing
        }
    }
}
