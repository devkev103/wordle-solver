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
            _solver.AddRule(new Rules("m", 0, Rule.Absent));
            _solver.AddRule(new Rules("o", 1, Rule.Present));
            _solver.AddRule(new Rules("u", 2, Rule.Absent));
            _solver.AddRule(new Rules("s", 3, Rule.Absent));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));

            // adding erode
            _solver.AddRule(new Rules("e", 0, Rule.Absent));
            _solver.AddRule(new Rules("r", 1, Rule.Absent));
            _solver.AddRule(new Rules("o", 2, Rule.Correct));
            _solver.AddRule(new Rules("d", 3, Rule.Absent));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));

            // adding adobe
            _solver.AddRule(new Rules("a", 0, Rule.Absent));
            _solver.AddRule(new Rules("d", 1, Rule.Absent));
            _solver.AddRule(new Rules("o", 2, Rule.Correct));
            _solver.AddRule(new Rules("b", 3, Rule.Present));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));
            _solver.ApplyRules() ;

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForDodge_ReturnFalse()
        {
            // KEY: DODGE -- optimization for list efficiency (should not exist)
            var key = "dodge";

            // adding alert
            _solver.AddRule(new Rules("a", 0, Rule.Absent));
            _solver.AddRule(new Rules("l", 1, Rule.Absent));
            _solver.AddRule(new Rules("e", 2, Rule.Present));
            _solver.AddRule(new Rules("r", 3, Rule.Absent));
            _solver.AddRule(new Rules("t", 4, Rule.Absent));

            // adding snide
            _solver.AddRule(new Rules("s", 0, Rule.Absent));
            _solver.AddRule(new Rules("n", 1, Rule.Absent));
            _solver.AddRule(new Rules("i", 2, Rule.Absent));
            _solver.AddRule(new Rules("d", 3, Rule.Absent));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));

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
            _solver.AddRule(new Rules("a", 0, Rule.Absent));
            _solver.AddRule(new Rules("l", 1, Rule.Absent));
            _solver.AddRule(new Rules("e", 2, Rule.Present));
            _solver.AddRule(new Rules("r", 3, Rule.Absent));
            _solver.AddRule(new Rules("t", 4, Rule.Absent));

            // adding snide
            _solver.AddRule(new Rules("s", 0, Rule.Absent));
            _solver.AddRule(new Rules("n", 1, Rule.Absent));
            _solver.AddRule(new Rules("i", 2, Rule.Absent));
            _solver.AddRule(new Rules("d", 3, Rule.Absent));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));

            _solver.ApplyRules();

            // choke should exist in list -- optimization
            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }

        [Test]
        public void ApplyingRules_ForErode_ReturnTrue()
        {
            // KEY: CHOKE #254 -- ERODE rule
            var key = "choke";

            _solver.AddRule(new Rules("e", 0, Rule.Absent));
            _solver.AddRule(new Rules("r", 1, Rule.Absent));
            _solver.AddRule(new Rules("o", 2, Rule.Correct));
            _solver.AddRule(new Rules("d", 3, Rule.Absent));
            _solver.AddRule(new Rules("e", 4, Rule.Correct));

            _solver.ApplyRules();

            Assert.IsTrue(_solver.Dictionary.Exists(e => e.StartsWith(key)), $"'{key}' does not exist in dictionary.");
        }
    }
}