using System;
using System.Collections.Generic;
using System.Linq;

namespace WordleSolver
{
    public class Solver 
    {
        List<Words> _words = new List<Words>();
        List<string> _dictionary = new List<string>();

        public Solver(List<string> dictionary)
        {
            _dictionary = dictionary;
        }

        public List<string> Dictionary
        {
            get { return _dictionary; }
            set { _dictionary = value; }
        }

        public void AddWord(Words word)
        {
            _words.Add(word);
        }

        public void ApplyRules()
        {
            List<string> removals = new List<string>();

            foreach (var ruleWord in _words)
            {
                foreach (var rule in ruleWord.Rules)
                {
                    foreach (var dictWord in _dictionary)
                    {
                        var shouldRemoveWord = false;
                        var frequencyOfDictWordLetter = dictWord.Count(f => (f == rule.Letter.ToCharArray()[0]));
                        var dictWordLetter = dictWord.Substring(rule.Position, 1);

                        // special rule. when a ruleWord has a double (or more) of the same letter and has this letter has Rules for Present or Correct multiple X times for the *same* Rule word. This means the word NEEDS to contain this Letter X amount of times
                        // Example. Key: melee. RuleWord: erode. The ruleWord (erode) will have a count of two for PresentCorrectForLetterCount with E. This means that any word that doesn't have at least two E's, can be eliminated.
                        var presentCorrectForRuleWordLetterCount = ruleWord.PresentCorrectForLetterCount(rule.Letter);
                        if (frequencyOfDictWordLetter < presentCorrectForRuleWordLetterCount)
                        {
                            shouldRemoveWord = true;
                        }

                        // correct
                        if (rule.Rule == Rule.Correct)
                        {
                            if (rule.Letter != dictWordLetter)
                            {
                                shouldRemoveWord = true;
                            }
                        }
                        // absent
                        else if (rule.Rule == Rule.Absent)
                        {
                            if (dictWord.Contains(rule.Letter) && !(ruleWord.PresentCorrectForLetter(rule.Letter)))
                            {
                                // remove words that include the ruleLetter, but doesn't have the Rule.Present
                                shouldRemoveWord = true;
                            }
                            else if (dictWordLetter == rule.Letter)
                            {
                                // remove words that has letter in wrong position
                                shouldRemoveWord = true;
                            }
                        }
                        // present
                        else if (rule.Rule == Rule.Present)
                        {
                            // contains a letter, but not in rule.position
                            if (!(dictWord.Contains(rule.Letter) && dictWordLetter != rule.Letter))
                            {
                                shouldRemoveWord = true;
                            }
                        }

                        // this is to prevent the word from being removed multiple times
                        if (shouldRemoveWord)
                        {
                            removals.Add(dictWord);
                        }
                    }
                }
            }

            foreach (var word in removals)
            {
                _dictionary.Remove(word);
            }
        }
    }
}
