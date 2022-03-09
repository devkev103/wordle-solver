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
            _solver.AddRule(new Rules("m", 0, Rule.Absent, "mouse"));
            _solver.AddRule(new Rules("o", 1, Rule.Present, "mouse"));
            _solver.AddRule(new Rules("u", 2, Rule.Absent, "mouse"));
            _solver.AddRule(new Rules("s", 3, Rule.Absent, "mouse"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "mouse"));

            // adding erode
            _solver.AddRule(new Rules("e", 0, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("r", 1, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("o", 2, Rule.Correct, "erode"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "erode"));

            // adding adobe
            _solver.AddRule(new Rules("a", 0, Rule.Absent, "adobe"));
            _solver.AddRule(new Rules("d", 1, Rule.Absent, "adobe"));
            _solver.AddRule(new Rules("o", 2, Rule.Correct, "adobe"));
            _solver.AddRule(new Rules("b", 3, Rule.Present, "adobe"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "adobe"));
            _solver.ApplyRules() ;

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForDodge_ReturnFalse()
        {
            // KEY: DODGE -- optimization for list efficiency (should not exist)
            var key = "dodge";

            // adding alert
            _solver.AddRule(new Rules("a", 0, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("l", 1, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("e", 2, Rule.Present, "alert"));
            _solver.AddRule(new Rules("r", 3, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("t", 4, Rule.Absent, "alert"));

            // adding snide
            _solver.AddRule(new Rules("s", 0, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("n", 1, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("i", 2, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "snide"));

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
            _solver.AddRule(new Rules("a", 0, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("l", 1, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("e", 2, Rule.Present, "alert"));
            _solver.AddRule(new Rules("r", 3, Rule.Absent, "alert"));
            _solver.AddRule(new Rules("t", 4, Rule.Absent, "alert"));

            // adding snide
            _solver.AddRule(new Rules("s", 0, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("n", 1, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("i", 2, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "snide"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "snide"));

            _solver.ApplyRules();

            // choke should exist in list -- optimization
            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForErode_ReturnTrue()
        {
            // KEY: CHOKE #254 -- ERODE rule
            var key = "choke";

            // adding choke
            _solver.AddRule(new Rules("e", 0, Rule.Absent, "choke"));
            _solver.AddRule(new Rules("r", 1, Rule.Absent, "choke"));
            _solver.AddRule(new Rules("o", 2, Rule.Correct, "choke"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "choke"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "choke"));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForSweet_ReturnTrue()
        {
            // KEY: SWEET #262 -- DoubleLetter PresentCorrect rule
            var key = "sweet";

            // adding melee
            _solver.AddRule(new Rules("m", 0, Rule.Absent, "melee"));
            _solver.AddRule(new Rules("e", 1, Rule.Present, "melee"));
            _solver.AddRule(new Rules("l", 2, Rule.Absent, "melee"));
            _solver.AddRule(new Rules("e", 3, Rule.Correct, "melee"));
            _solver.AddRule(new Rules("e", 4, Rule.Absent, "melee"));

            // adding steed
            _solver.AddRule(new Rules("s", 0, Rule.Correct, "steed"));
            _solver.AddRule(new Rules("t", 1, Rule.Present, "steed"));
            _solver.AddRule(new Rules("e", 2, Rule.Correct, "steed"));
            _solver.AddRule(new Rules("e", 3, Rule.Correct, "steed"));
            _solver.AddRule(new Rules("d", 4, Rule.Absent, "steed"));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForMelee_ReturnTrue()
        {
            // KEY: MELEE #??? -- Triple Letter PresentCorrect rule
            var key = "melee";

            // adding melee
            _solver.AddRule(new Rules("g", 0, Rule.Absent, "geese"));
            _solver.AddRule(new Rules("e", 1, Rule.Correct, "geese"));
            _solver.AddRule(new Rules("e", 2, Rule.Present, "geese"));
            _solver.AddRule(new Rules("s", 3, Rule.Absent, "geese"));
            _solver.AddRule(new Rules("e", 4, Rule.Correct, "geese"));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForSweet2_ReturnTrue()
        {
            // KEY: SWEET #262 -- DoubleLetter PresentCorrect rule
            var key = "sweet";

            // adding erode
            _solver.AddRule(new Rules("e", 0, Rule.Present, "erode"));
            _solver.AddRule(new Rules("r", 1, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("o", 2, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("e", 4, Rule.Present, "erode"));

            // adding level
            _solver.AddRule(new Rules("l", 0, Rule.Absent, "level"));
            _solver.AddRule(new Rules("e", 1, Rule.Present, "level"));
            _solver.AddRule(new Rules("v", 2, Rule.Absent, "level"));
            _solver.AddRule(new Rules("e", 3, Rule.Correct, "level"));
            _solver.AddRule(new Rules("l", 4, Rule.Absent, "level"));

            // adding melee
            _solver.AddRule(new Rules("m", 0, Rule.Absent, "melee"));
            _solver.AddRule(new Rules("e", 1, Rule.Present, "melee"));
            _solver.AddRule(new Rules("l", 2, Rule.Absent, "melee"));
            _solver.AddRule(new Rules("e", 3, Rule.Correct, "melee"));
            _solver.AddRule(new Rules("e", 4, Rule.Absent, "melee"));

            // adding tweet
            _solver.AddRule(new Rules("t", 0, Rule.Absent, "tweet"));
            _solver.AddRule(new Rules("w", 1, Rule.Correct, "tweet"));
            _solver.AddRule(new Rules("e", 2, Rule.Correct, "tweet"));
            _solver.AddRule(new Rules("e", 3, Rule.Correct, "tweet"));
            _solver.AddRule(new Rules("t", 4, Rule.Correct, "tweet"));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForSweetSameWordTwice_ReturnTrue()
        {
            // KEY: SWEET #262 -- adding same word twice
            var key = "sweet";

            // adding erode
            _solver.AddRule(new Rules("e", 0, Rule.Present, "erode"));
            _solver.AddRule(new Rules("r", 1, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("o", 2, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("e", 4, Rule.Present, "erode"));

            // adding erode
            _solver.AddRule(new Rules("e", 0, Rule.Present, "erode"));
            _solver.AddRule(new Rules("r", 1, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("o", 2, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("d", 3, Rule.Absent, "erode"));
            _solver.AddRule(new Rules("e", 4, Rule.Present, "erode"));


            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForMonthSameWordTwice_ReturnTrue()
        {
            // KEY: MONTH #263 -- adding same word twice
            var key = "month";

            // adding later twice
            _solver.AddRule(new Rules("l", 0, Rule.Absent, "later"));
            _solver.AddRule(new Rules("a", 1, Rule.Absent, "later"));
            _solver.AddRule(new Rules("t", 2, Rule.Present, "later"));
            _solver.AddRule(new Rules("e", 3, Rule.Absent, "later"));
            _solver.AddRule(new Rules("r", 4, Rule.Absent, "later"));

            _solver.AddRule(new Rules("l", 0, Rule.Absent, "later"));
            _solver.AddRule(new Rules("a", 1, Rule.Absent, "later"));
            _solver.AddRule(new Rules("t", 2, Rule.Present, "later"));
            _solver.AddRule(new Rules("e", 3, Rule.Absent, "later"));
            _solver.AddRule(new Rules("r", 4, Rule.Absent, "later"));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }
    }
}