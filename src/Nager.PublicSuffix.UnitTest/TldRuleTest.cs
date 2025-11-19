using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nager.PublicSuffix.Models;
using System;

namespace Nager.PublicSuffix.UnitTest
{
    [TestClass]
    public class TldRuleTest
    {
        [TestMethod]
        public void InvalidRuleTest1()
        {
            var ex = Assert.ThrowsExactly<ArgumentException>(() => new TldRule(""));
            Assert.AreEqual("RuleData is empty", ex.Message);
        }

        [TestMethod]
        public void InvalidRuleTest2()
        {
            var ex = Assert.ThrowsExactly<ArgumentException>(() => new TldRule(null));
            Assert.AreEqual("RuleData is empty", ex.Message);
        }

        [TestMethod]
        public void InvalidRuleTest3()
        {
            var ex = Assert.ThrowsExactly<FormatException>(() => new TldRule("*com"));
            Assert.AreEqual("Wildcard syntax not correct", ex.Message);
        }

        [TestMethod]
        public void InvalidRuleTest4()
        {
            var ex = Assert.ThrowsExactly<FormatException>(() => new TldRule("*bar.foo"));
            Assert.AreEqual("Wildcard syntax not correct", ex.Message);
        }

        [TestMethod]
        public void InvalidRuleTest5()
        {
            var ex = Assert.ThrowsExactly<FormatException>(() => new TldRule(".com"));
            Assert.AreEqual("Rule contains empty part", ex.Message);
        }

        [TestMethod]
        public void InvalidRuleTest6()
        {
            var ex = Assert.ThrowsExactly<FormatException>(() => new TldRule("www..com"));
            Assert.AreEqual("Rule contains empty part", ex.Message);
        }

        [TestMethod]
        public void ValidRuleTest1()
        {
            var tldRule = new TldRule("com");
            Assert.AreEqual("com", tldRule.Name);
            Assert.AreEqual(TldRuleType.Normal, tldRule.Type);
        }

        [TestMethod]
        public void ValidRuleTest2()
        {
            var tldRule = new TldRule("*.com");
            Assert.AreEqual("*.com", tldRule.Name);
            Assert.AreEqual(TldRuleType.Wildcard, tldRule.Type);
        }

        [TestMethod]
        public void ValidRuleTest3()
        {
            var tldRule = new TldRule("!com");
            Assert.AreEqual("com", tldRule.Name);
            Assert.AreEqual(TldRuleType.WildcardException, tldRule.Type);
        }

        [TestMethod]
        public void ValidRuleTest4()
        {
            var tldRule = new TldRule("co.uk");
            Assert.AreEqual("co.uk", tldRule.Name);
            Assert.AreEqual(TldRuleType.Normal, tldRule.Type);
        }

        [TestMethod]
        public void ValidRuleTest5()
        {
            var tldRule = new TldRule("*.*.foo");
            Assert.AreEqual("*.*.foo", tldRule.Name);
            Assert.AreEqual(TldRuleType.Wildcard, tldRule.Type);
        }

        [TestMethod]
        public void ValidRuleTest6()
        {
            var tldRule = new TldRule("a.b.web.*.foo", TldRuleDivision.Private);
            Assert.AreEqual("a.b.web.*.foo", tldRule.Name);
            Assert.AreEqual(TldRuleDivision.Private, tldRule.Division);
        }
    }
}
