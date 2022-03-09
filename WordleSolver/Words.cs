using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WordleSolver
{
    public class Words
    {
        private List<Rules> _rules = new List<Rules>();
        private string _word;

        public Words(string word)
        {
            _word = word;
        }

        public List<Rules> Rules
        {
            get { return _rules; }
        }
        public string Word
        {
            get { return _word; }
        }

        public void AddRule(Rules rule)
        {
            _rules.Add(rule);
            
            // QA step to see if too many rules have been added? 5 letter word can only have 5 rules
        }

        public int PresentCorrectForLetterCount(string letter)
        {
            return _rules.Where(x => (x.Rule == Rule.Present || x.Rule == Rule.Correct) && x.Letter == letter).Count();
        }

        public bool PresentCorrectForLetter(string letter)
        {
            return PresentCorrectForLetterCount(letter) > 0 ? true : false;
        }

        public string GetCharacterInPosition(int position)
        {
            return _word.Substring(position, 1);
        }
    }
}
