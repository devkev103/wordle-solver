using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleSolver
{
    public class Solver 
    {
        List<Rules> rules = new List<Rules>();
        List<string> words = new List<string>();
        List<string> dictionary = new List<string>();

        public Solver(List<string> dictionary)
        {
            this.dictionary = dictionary;
        }

        public List<string> Dictionary
        {
            get { return dictionary; }
            set { dictionary = value; }
        }

        public void PrintTop(int number)
        {
            int TopX = number < dictionary.Count ? number : dictionary.Count;
            for (int i = 0; i < TopX; i++)
            {
                Console.WriteLine(dictionary[i]);
            }
        }

        public void AddRule(Rules rule)
        {
            rules.Add(rule);
        }

        public void AddWord(string word)
        {
            words.Add(word);
        }

        public void ApplyRules()
        {
            List<string> removals = new List<string>();

            foreach (var rule in rules)
            {
                foreach (string word in dictionary)
                {
                    var wordLetter = word.Substring(rule.Position, 1);

                    // special case when words contains a double letter, but one is in the correct position; you can blantently remove words that contain the letter
                    // does any current rule have rule.Present or rule.Correct for this letter?
                    var hasWordLetterPresentRule = false;
                    foreach (Rules checkRule in rules)
                    {
                        if ((checkRule.Rule == Rule.Present || checkRule.Rule == Rule.Correct) && checkRule.Letter == rule.Letter)
                            hasWordLetterPresentRule = true;
                    }

                    // correct
                    if (rule.Rule == Rule.Correct)
                    {
                        if(rule.Letter != wordLetter)
                        {
                            removals.Add(word);
                        }
                    }

                    // absent
                    if (rule.Rule == Rule.Absent)
                    {
                        if (word.Contains(rule.Letter) && !(hasWordLetterPresentRule))
                        {
                            // remove words that include the ruleLetter, but doesn't have the Rule.Present
                            removals.Add(word);
                        }
                        else if (wordLetter == rule.Letter)
                        {
                            // remove words that has letter in wrong position
                            removals.Add(word);
                        }
                    }

                    // present
                    if (rule.Rule == Rule.Present)
                    {
                        // contains a letter, but not in rule.position
                        if (!(word.Contains(rule.Letter) && wordLetter != rule.Letter))
                        {
                            removals.Add(word);
                        }
                    }
                }
            }

            foreach(var word in removals)
            {
                dictionary.Remove(word);
            }
        }
    }
}
