using NUnit.Framework;
using WordleSolver;

namespace UnitTests.WordleSolver
{
    [TestFixture]
    public class ApplyingRules
    {
        private Dictionary _dict;
        private Solver _solver;

        [SetUp]
        public void SetUp()
        {
            _dict = new Dictionary();
            _solver = new Solver(_dict.PossibleWords());
        }

        [Test]
        public void ApplyingRules_ForBloke_ReturnTrue()
        {
            // KEY: BLOKE #250 -- ashley's rule
            var key = "bloke";

            // adding mouse
            var word1 = new Words("mouse");
            word1.AddRule(new Rules("m", 0, Rule.Absent));
            word1.AddRule(new Rules("o", 1, Rule.Present));
            word1.AddRule(new Rules("u", 2, Rule.Absent));
            word1.AddRule(new Rules("s", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word1);

            // adding erode
            var word2 = new Words("erode");
            word2.AddRule(new Rules("e", 0, Rule.Absent));
            word2.AddRule(new Rules("r", 1, Rule.Absent));
            word2.AddRule(new Rules("o", 2, Rule.Correct));
            word2.AddRule(new Rules("d", 3, Rule.Absent));
            word2.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word2);

            // adding adobe
            var word3 = new Words("adobe");
            word3.AddRule(new Rules("a", 0, Rule.Absent));
            word3.AddRule(new Rules("d", 1, Rule.Absent));
            word3.AddRule(new Rules("o", 2, Rule.Correct));
            word3.AddRule(new Rules("b", 3, Rule.Present));
            word3.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word3);

            _solver.ApplyRules() ;

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForDodge_ReturnFalse()
        {
            // KEY: DODGE -- optimization for list efficiency (should not exist)
            var key = "dodge";

            // adding alert
            var word1 = new Words("alert");
            word1.AddRule(new Rules("a", 0, Rule.Absent));
            word1.AddRule(new Rules("l", 1, Rule.Absent));
            word1.AddRule(new Rules("e", 2, Rule.Present));
            word1.AddRule(new Rules("r", 3, Rule.Absent));
            word1.AddRule(new Rules("t", 4, Rule.Absent));
            _solver.AddWord(word1);

            // adding snide
            var word2 = new Words("snide");
            word2.AddRule(new Rules("s", 0, Rule.Absent));
            word2.AddRule(new Rules("n", 1, Rule.Absent));
            word2.AddRule(new Rules("i", 2, Rule.Absent));
            word2.AddRule(new Rules("d", 3, Rule.Absent));
            word2.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word2);

            _solver.ApplyRules();

            // dodge should not exist in list -- optimization
            Assert.IsFalse(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForChoke_ReturnTrue()
        {
            // KEY: CHOKE #254 -- optimization
            var key = "choke";

            // adding alert
            var word1 = new Words("alert");
            word1.AddRule(new Rules("a", 0, Rule.Absent));
            word1.AddRule(new Rules("l", 1, Rule.Absent));
            word1.AddRule(new Rules("e", 2, Rule.Present));
            word1.AddRule(new Rules("r", 3, Rule.Absent));
            word1.AddRule(new Rules("t", 4, Rule.Absent));
            _solver.AddWord(word1);

            // adding snide
            var word2 = new Words("snide");
            word2.AddRule(new Rules("s", 0, Rule.Absent));
            word2.AddRule(new Rules("n", 1, Rule.Absent));
            word2.AddRule(new Rules("i", 2, Rule.Absent));
            word2.AddRule(new Rules("d", 3, Rule.Absent));
            word2.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word2);

            _solver.ApplyRules();

            // choke should exist in list -- optimization
            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForErode_ReturnTrue()
        {
            // KEY: CHOKE #254 -- ERODE rule
            var key = "choke";

            // adding erode
            var word1 = new Words("erode");
            word1.AddRule(new Rules("e", 0, Rule.Absent));
            word1.AddRule(new Rules("r", 1, Rule.Absent));
            word1.AddRule(new Rules("o", 2, Rule.Correct));
            word1.AddRule(new Rules("d", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word1);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForSweet_ReturnTrue()
        {
            // KEY: SWEET #262 -- DoubleLetter PresentCorrect rule
            var key = "sweet";

            // adding melee
            var word1 = new Words("melee");
            word1.AddRule(new Rules("m", 0, Rule.Absent));
            word1.AddRule(new Rules("e", 1, Rule.Present));
            word1.AddRule(new Rules("l", 2, Rule.Absent));
            word1.AddRule(new Rules("e", 3, Rule.Correct));
            word1.AddRule(new Rules("e", 4, Rule.Absent));
            _solver.AddWord(word1);

            // adding steed
            var word2 = new Words("steed");
            word2.AddRule(new Rules("s", 0, Rule.Correct));
            word2.AddRule(new Rules("t", 1, Rule.Present));
            word2.AddRule(new Rules("e", 2, Rule.Correct));
            word2.AddRule(new Rules("e", 3, Rule.Correct));
            word2.AddRule(new Rules("d", 4, Rule.Absent));
            _solver.AddWord(word2);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForMelee_ReturnTrue()
        {
            // KEY: MELEE #??? -- Triple Letter PresentCorrect rule
            var key = "melee";

            // adding geese
            var word1 = new Words("geese");
            word1.AddRule(new Rules("g", 0, Rule.Absent));
            word1.AddRule(new Rules("e", 1, Rule.Correct));
            word1.AddRule(new Rules("e", 2, Rule.Present));
            word1.AddRule(new Rules("s", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.AddWord(word1);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }
        
        [Test]
        public void ApplyingRules_ForSweet2_ReturnTrue()
        {
            // KEY: SWEET #262 -- DoubleLetter PresentCorrect rule
            var key = "sweet";

            // adding erode
            var word1 = new Words("erode");
            word1.AddRule(new Rules("e", 0, Rule.Present));
            word1.AddRule(new Rules("r", 1, Rule.Absent));
            word1.AddRule(new Rules("o", 2, Rule.Absent));
            word1.AddRule(new Rules("d", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Present));
            _solver.AddWord(word1);

            // adding level
            var word2 = new Words("level");
            word2.AddRule(new Rules("l", 0, Rule.Absent));
            word2.AddRule(new Rules("e", 1, Rule.Present));
            word2.AddRule(new Rules("v", 2, Rule.Absent));
            word2.AddRule(new Rules("e", 3, Rule.Correct));
            word2.AddRule(new Rules("l", 4, Rule.Absent));
            _solver.AddWord(word2);

            // adding melee
            var word3 = new Words("melee");
            word3.AddRule(new Rules("m", 0, Rule.Absent));
            word3.AddRule(new Rules("e", 1, Rule.Present));
            word3.AddRule(new Rules("l", 2, Rule.Absent));
            word3.AddRule(new Rules("e", 3, Rule.Correct));
            word3.AddRule(new Rules("e", 4, Rule.Absent));
            _solver.AddWord(word3);

            // adding tweet
            var word4 = new Words("tweet");
            word4.AddRule(new Rules("t", 0, Rule.Absent));
            word4.AddRule(new Rules("w", 1, Rule.Correct));
            word4.AddRule(new Rules("e", 2, Rule.Correct));
            word4.AddRule(new Rules("e", 3, Rule.Correct));
            word4.AddRule(new Rules("t", 4, Rule.Correct));
            _solver.AddWord(word4);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }
        
        [Test]
        public void ApplyingRules_ForSweetSameWordTwice_ReturnTrue()
        {
            // KEY: SWEET #262 -- adding same word twice
            var key = "sweet";

            // adding erode - twice
            var word1 = new Words("erode");
            word1.AddRule(new Rules("e", 0, Rule.Present));
            word1.AddRule(new Rules("r", 1, Rule.Absent));
            word1.AddRule(new Rules("o", 2, Rule.Absent));
            word1.AddRule(new Rules("d", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Present));
            _solver.AddWord(word1);
            _solver.AddWord(word1);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForMonthSameWordTwice_ReturnTrue()
        {
            // KEY: MONTH #263 -- adding same word twice
            var key = "month";

            // adding later - twice
            var word1 = new Words("later");
            word1.AddRule(new Rules("l", 0, Rule.Absent));
            word1.AddRule(new Rules("a", 1, Rule.Absent));
            word1.AddRule(new Rules("t", 2, Rule.Present));
            word1.AddRule(new Rules("e", 3, Rule.Absent));
            word1.AddRule(new Rules("r", 4, Rule.Absent));
            _solver.AddWord(word1);
            _solver.AddWord(word1);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForVivid_ReturnTrue()
        {
            // KEY: VIVID #???
            var key = "vivid";

            // adding mouse
            var word1 = new Words("mouse");
            word1.AddRule(new Rules("m", 0, Rule.Absent));
            word1.AddRule(new Rules("o", 1, Rule.Absent));
            word1.AddRule(new Rules("u", 2, Rule.Absent));
            word1.AddRule(new Rules("s", 3, Rule.Absent));
            word1.AddRule(new Rules("e", 4, Rule.Absent));
            _solver.AddWord(word1);

            // adding naval
            var word2 = new Words("naval");
            word2.AddRule(new Rules("n", 0, Rule.Absent));
            word2.AddRule(new Rules("a", 1, Rule.Absent));
            word2.AddRule(new Rules("v", 2, Rule.Correct));
            word2.AddRule(new Rules("a", 3, Rule.Absent));
            word2.AddRule(new Rules("l", 4, Rule.Absent));
            _solver.AddWord(word2);

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }
    }
}