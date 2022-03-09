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
                    
                    foreach (Rules checkRule in rules) // sort by word
                    {
                        if ((checkRule.Rule == Rule.Present || checkRule.Rule == Rule.Correct) && checkRule.Letter == rule.Letter)
                        {
                            hasWordLetterPresentRule = true;
                        }
                    }

                    // count the number of occurances of Present or Correct among a single word for current rule.Letter; take the highest count
                    // TODO! should be moved into its own class with a property to return this number
                    var wordLetterGroups = rules.GroupBy(u => new { u.Word, u.Letter }).Select(grp => grp.ToList());
                    var containsLettersCountMax = 0;
                    foreach (var wordLetterGroup in wordLetterGroups)
                    {
                        foreach (var wordLetterPair in wordLetterGroup)
                        {
                            if (wordLetterPair.Letter == rule.Letter && wordLetterGroup.Count() >= 2)
                            {
                                var containsLettersCount = wordLetterGroup.Where(x => x.Rule == Rule.Present || x.Rule == Rule.Correct).Count();
                                if (containsLettersCount > containsLettersCountMax)
                                {
                                    containsLettersCountMax = containsLettersCount;
                                }
                            }
                        }
                    }

                    var shouldRemoveWord = false;
                    var frequencyOfLetter = word.Count(f => (f == rule.Letter.ToCharArray()[0]));

                    // special rule. when a word has a double (or more) of the same letter and has this letter has Rules for Present or Correct multiple X times for the *same* Rule word. This means the word NEEDS to contain this Letter X amount of times
                    if (frequencyOfLetter < containsLettersCountMax)
                    {
                        shouldRemoveWord = true;
                    }

                    // correct
                    if (rule.Rule == Rule.Correct)
                    {
                        if(rule.Letter != wordLetter)
                        {
                            shouldRemoveWord = true;
                        }
                    }
                    // absent
                    else if (rule.Rule == Rule.Absent)
                    {
                        if (word.Contains(rule.Letter) && !(hasWordLetterPresentRule))
                        {
                            // remove words that include the ruleLetter, but doesn't have the Rule.Present
                            shouldRemoveWord = true;
                        }
                        else if (wordLetter == rule.Letter)
                        {
                            // remove words that has letter in wrong position
                            shouldRemoveWord = true;
                        }
                    }
                    // present
                    else if (rule.Rule == Rule.Present)
                    {
                        // contains a letter, but not in rule.position
                        if (!(word.Contains(rule.Letter) && wordLetter != rule.Letter))
                        {
                            shouldRemoveWord = true;
                        }
                    }

                    // this is to prevent the word from being removed multiple times
                    if (shouldRemoveWord)
                    {
                        removals.Add(word);
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
