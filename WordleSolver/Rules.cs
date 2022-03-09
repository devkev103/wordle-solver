namespace WordleSolver
{
    public enum Rule
    {
        Correct = 1,
        Present = 2,
        Absent = 3
    }

    public class Rules
    {
        private string letter;
        public string Letter
        {
            get => letter;
        }

        private int position;
        public int Position
        {
            get => position;
        }

        private Rule rule;
        public Rule Rule
        {
            get => rule;
        }

        private string word;
        public string Word
        {
            get => word;
        }

        public Rules(string letter, int position, Rule rule, string word)
        {
            this.letter = letter;
            this.position = position;
            this.rule = rule;
            this.word = word;
        }
    }
}
