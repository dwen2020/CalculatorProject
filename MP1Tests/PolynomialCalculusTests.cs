using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP1.Tests
{
    [TestClass()]
    public class PolynomialCalculusTests
    {

        [TestMethod()]
        public void IsValidPolynomialTest1()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test1 = "-2 -3.457 0 0";
            bool validity = pol.IsValidPolynomial(test1);

            Assert.IsTrue(validity);
        }

        [TestMethod()]
        public void IsValidPolynomialTest2()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test2 = "0 .1 -1";
            bool validity = pol.IsValidPolynomial(test2);

            Assert.IsTrue(validity);
        }

        [TestMethod()]
        public void IsValidPolynomialTest3()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test3 = "2      3.5 0";
            bool validity = pol.IsValidPolynomial(test3);

            Assert.IsTrue(validity);
        }

        [TestMethod()]
        public void IsValidPolynomialTest4()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test4 = "3 . 5";
            bool validity = pol.IsValidPolynomial(test4);

            Assert.IsFalse(validity);
        }

        [TestMethod()]
        public void IsValidPolynomialTest5()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test5 = "2x^2+1";
            bool validity = pol.IsValidPolynomial(test5);

            Assert.IsFalse(validity);
        }

        [TestMethod()]
        public void IsValidPolynomialTest6()
        {
            PolynomialCalculus pol = new PolynomialCalculus();
            string test6 = "1/2 2";
            bool validity = pol.IsValidPolynomial(test6);

            Assert.IsFalse(validity);
        }

        [TestMethod()]
        public void GetPolynomial1()
        {
            PolynomialCalculus pol = new PolynomialCalculus("0 -3 2 8 0");

            Assert.AreEqual(pol.GetPolynomial(), "-3x^3 + 2x^2 + 8x");
        }

        [TestMethod()]
        public void GetPolynomial2()
        {
            PolynomialCalculus pol = new PolynomialCalculus("3 2 1");

            Assert.AreEqual(pol.GetPolynomial(), "3x^2 + 2x + 1");
        }

        [TestMethod()]
        public void GetPolynomial3()
        {
            PolynomialCalculus pol = new PolynomialCalculus("1 0 -2.1 -1 5 12");

            Assert.AreEqual(pol.GetPolynomial(), "x^5 - 2.1x^3 - x^2 + 5x + 12");
        }

        [TestMethod()]
        public void EvaluatePolynomialTest1()
        {
            PolynomialCalculus pol = new PolynomialCalculus("3 2 1");

            double result = pol.EvaluatePolynomial(5);
            Assert.AreEqual(result, 86, 0.001);
        }

        [TestMethod()]
        public void EvaluatePolynomialTest2()
        {
            PolynomialCalculus pol = new PolynomialCalculus("3 0 0");    

            double result = pol.EvaluatePolynomial(5);
            Assert.AreEqual(result, 75, 0.001);
        }


        [TestMethod()]
        public void EvaulateDerivativeTest1()
        {
            PolynomialCalculus pol = new PolynomialCalculus("4, 3, 0, 5");
            double derivative = pol.EvaluatePolynomialDerivative(2);

            Assert.AreEqual(derivative, 60);
        }

        [TestMethod()]
        public void EvaulateDerivativeTest2()
        {
            PolynomialCalculus pol = new PolynomialCalculus("-3, 3, 1, 0");
            double derivative = pol.EvaluatePolynomialDerivative(2);

            Assert.AreEqual(derivative, -23);
        }

        [TestMethod()]
        public void EvaluatePolynomialIntegral1()
        {
            PolynomialCalculus pol = new PolynomialCalculus("3 2 1");

            Assert.AreEqual(pol.EvaluatePolynomialIntegral(0, 2), 14, 0.001);
        }


        [TestMethod()]
        public void EvaluatePolynomialIntegral2()
        {
            PolynomialCalculus pol = new PolynomialCalculus("-3 0 2 1");

            Assert.AreEqual(pol.EvaluatePolynomialIntegral(0,2), -6, 0.001);
        }


        [TestMethod()]
        public void GetAllRoots1()
        {
            PolynomialCalculus pol = new PolynomialCalculus("3 2 1");

            Assert.AreEqual(pol.GetAllRoots(0.00001).Count, 0);
        }

    }
}