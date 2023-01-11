using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MP1.Tests
{
    [TestClass()]
    public class ArithmeticTests
    {
        [TestMethod()]
        public void OutputExpressionTest()
        {
            string expression = "56^  .4--3 +  -6*(4.8-9+77.9)";
            Assert.IsTrue(Arithmetic.OutputExpression(expression).Equals("56 ^ .4 + 3 - 6 * (4.8 - 9 + 77.9)"));
        }

        [TestMethod()]
        public void OutputExpressionTest2()
        {
            string expression = "4+4*-9.76+0-8*-7+-3.8^(2*-3--9)";
            Assert.IsTrue(Arithmetic.OutputExpression(expression).Equals("4 + 4 * -9.76 + 0 - 8 * -7 - 3.8 ^ (2 * -3 + 9)"));
        }

        [TestMethod()]
        public void OutputExpressionTest3()
        {
            string expression = "29.2     +           -76 - 93   * 2*3*(  3 -   - 25.258   )";
            Assert.IsTrue(Arithmetic.OutputExpression(expression).Equals("29.2 - 76 - 93 * 2 * 3 * (3 + 25.258)"));
        }

        [TestMethod()]
        public void EvaluateExpressionTest1()
        {
            string expression = "(8 * 9 + 77.22";
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void EvaluateExpressionTest2()
        {
            string expression = "11  *  -      77.22";
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void EvaluateExpressionTest3()
        {
            string expression = "9 -- 7";
            double result = Arithmetic.EvaluateExpression(expression);
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void EvaluateExpressionTest4()
        {
            string expression = "102.88 * / 9";
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void EvaluateExpressionTest5()
        {
            string expression = "8 * .  22";
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void EvaluateExpressionTest6()
        {
            string expression = "(8 * 9 + 77.22";
            Assert.IsTrue(Arithmetic.EvaluateExpression(expression).Equals(double.NaN));
        }

        [TestMethod()]
        public void ShuntingYardTest1()
        {
            var expression = new List<string> { "3.98", "-", "55", "*", "+", "4.2" };
            var expectedOut = new List<string>();
            var actualOut = new List<string>(Arithmetic.ShuntingYard(expression));

            if (expectedOut.Count == actualOut.Count)
            {
                for (int x = 0; x < actualOut.Count; x++)
                {
                    if (actualOut[x] != expectedOut[x])
                    {
                        Assert.Fail();
                    }
                }
            }
            else
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ShuntingYardTest2()
        {
            var expression = new List<string> { "5", "+", "2", "/", "(", "3", "-", "8", ")", "^", "5" };
            var expectedOut = new List<string> { "5", "2", "3", "8", "-", "5", "^", "/", "+" };
            var actualOut = new List<string>(Arithmetic.ShuntingYard(expression));

            if (expectedOut.Count == actualOut.Count)
            {
                for (int x = 0; x < actualOut.Count; x++)
                {
                    if (actualOut[x] != expectedOut[x])
                    {
                        Assert.Fail();
                    }
                }
            }
            else
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void ShuntingYardTest3()
        {
            var expression = new List<string> { "11", "77.22" };
            var expectedOut = new List<string>();
            var actualOut = new List<string>(Arithmetic.ShuntingYard(expression));

            if (expectedOut.Count == actualOut.Count)
            {
                for (int x = 0; x < actualOut.Count; x++)
                {
                    if (actualOut[x] != expectedOut[x])
                    {
                        Assert.Fail();
                    }
                }
            }
            else
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }
    }
}