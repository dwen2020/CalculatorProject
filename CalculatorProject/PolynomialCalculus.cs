//MP1  
//This file contains the PolynomialCalculus class.

//You should implement the requesed methods.


using System;
using System.Collections.Generic;
using System.Text;

namespace MP1
{
    public class PolynomialCalculus
    {
        List<double> coefficientList = new List<double>(); //the only field of this class

        // The following two constructors are used for unit testing.
        public PolynomialCalculus() //Needed for unit testing and for Main(). Do not remove or modify.
        {
            // Default constructor has an empty body
        }
        public PolynomialCalculus(string testInput) //Needed for unit testing. Do not remove or modify.
        {
            string[] coefficients = testInput.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in coefficients)
            {
                coefficientList.Add(Convert.ToDouble(item));
            }
        }

        /// <summary>
        /// Prompts the user for the coefficients of a polynomial, and sets the 
        /// the coefficientList field of the object.
        /// The isValidPolynomial method is used to check for the validity
        /// of the polynomial entered by the user, otherwise the field must 
        /// not change.
        /// The acceptable format of the coefficients received from the user is 
        /// a series of numbers (one for each coefficient) separated by spaces.
        /// All coefficients values must be entered even those that are zero.
        /// </summary>
        /// <returns>True if the polynomial is succeffully set, false otherwise.</returns>
        public bool SetPolynomial()
        {
            //get user input for coefficients
            Console.WriteLine("\nEnter the coeficients for the polynomial, separated by a space (descending order).");
            Console.WriteLine("Example: Enter 1.13 0 -3 1 0 for the polynomial 1.13x^4 - 3x^2 + x");

            //read through string input and remove spaces
            string coefficientEntry = Console.ReadLine().Trim();

            //check if the input is a valid polynomial
            if (IsValidPolynomial(coefficientEntry)==true)
            {
                // remove empty coefficient terms and convert to double to go in coefficientlist
                // then returns true when it is set
                string[] coefficients = coefficientEntry.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string coefficient in coefficients)
                {
                    coefficientList.Add(Double.Parse(coefficient));
                }
                return true;
            }
            
            else
                return false;
        }

        /// <summary>
        /// Checks if the passed polynomial string is valid.
        /// The acceptable format of the coefficient string is a series of 
        /// numbers (one for each coefficient) separated by spaces. 
        /// Any number of extra spaces is allowed.
        /// </summary>
        /// <example>
        /// Examples of valid strings: 
        ///       "1 2 3", or " 2   3.5 0  ", or "-2 -3.547 0 0", or "0 .1 -1"
        /// Examples of invalid strings: 
        ///       "3 . 5", or "2x^2+1", or "a b c", or "3 - 5", or "1/2 2", or ""
        /// </example>
        /// <param name="polynomial">
        /// A string containing the coefficient of a polynomial. The first value is the
        /// highest order, and all coefficients exist (even 0's).
        /// </param>
        /// <returns>True if a valid polynomial, false otherwise.</returns>
        public bool IsValidPolynomial(string polynomial)
        {
            // if string is null or empty false is returned
            if (String.IsNullOrEmpty(polynomial) == true)
            {
                return false;
            }

            int len = polynomial.Length;

            // if the last element is not a digit false is returned
            if (char.IsDigit(polynomial, len - 1) == false && polynomial[len - 1] != ' ')
            {
                return false;
            }

            // iterates through string
            for (int i = 0; i < len - 1; i++)
            {
                // if element is not a digit
                if (char.IsDigit(polynomial, i) == false && polynomial[i] != ' ')
                {
                    // if element is not a negtive sign or a decimal sign false is returned
                    if (polynomial[i] == '-' || polynomial[i] == '.')
                    {
                        // if the element on the right side of the sign is not a digit false is returned
                        if (char.IsDigit(polynomial, i + 1) == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;

        }

        /// <summary>
        /// Returns a string representing this polynomial.
        /// </summary>
        /// <returns>
        /// A string containing the polynomial in the format:
        /// a_nx^n +- a_n_1x^n_1 +- ... +- a1x +- a0
        /// Formatting rules:
        /// The +- operator will be + if the associated coefficient is positive, and - if negative.
        /// There is a space on either side of the binary operator.
        /// Do not display the associated term of a coefficient if it is 0.
        /// Do not display a coefficient if it is 1, except for a0.
        /// Do not display the power of x, if it is 1.
        /// If all coefficients are 0, then it returns "0".
        /// <example>
        /// For a user input "1 1 1", the method returns "x^2 + x + 1"
        /// For a user input "-1 -1 -1", the method returns "-x^2 - x - 1"
        /// For a user input "3 2 1", the method returns "3x^2 + 2x + 1"
        /// For a user input "0", the method returns "0"
        /// For a user input "-123.456", the method returns "-123.456"
        /// For a user input "-1.3 0 -5", the method returns "-1.3x^2 - 5"
        /// For a user input "0 0 -.55 13 0", the method returns "-0.55x^2 + 13x"
        /// For a user input "1 0 -2.1 -1 5 12", the method returns "x^5 - 2.1x^3 - x^2 + 5x + 12"
        /// </example>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public string GetPolynomial()
        {
            StringBuilder polyBuild = new StringBuilder();
            int count = 0;
            int length = coefficientList.Count;

            // if user input is "0" then "0" is returned
            if (length == 1 && coefficientList[0] == 0)
            {
                string inputZero = "0";
                return inputZero;
            }

            // iterates through coefficient list
            for (int i = length - 1; i >= 0; i--)
            {
                // if the coefficient is 0 it will be skipped to the next coefficient
                if (coefficientList[count] != 0)
                {
                    // if the size of the list is 1, that number is appended to polyBuild
                    if (length == 1)
                    {
                        polyBuild.Append($"{coefficientList[count]}");
                    }
                    // appends the last number of list if it is positive  
                    else if (i == 0 && coefficientList[count] >= 0)
                    {
                        polyBuild.Append($" + {coefficientList[count]}");
                    }
                    // appends the last number of list if it is negative
                    else if (i == 0 && coefficientList[count] < 0)
                    {
                        polyBuild.Append($" - {coefficientList[count] * -1}");
                    }
                    // positive numbers that have a power of 1 and for which the list size is greater than 2
                    else if (i == 1 && i != length - 1 && coefficientList[count] >= 0)
                    {
                        // if the coefficient is 1 only x appended with + sign in front
                        if (coefficientList[count] == 1)
                        {
                            polyBuild.Append($" + x");
                        }
                        // multiplies coefficient with x
                        else
                        {
                            polyBuild.Append($" + {coefficientList[count]}x");
                        }
                    }
                    // negative numbers that have a power of 1 and for which the list size is greater than 2
                    else if (i == 1 && i != length - 1 && coefficientList[count] < 0)
                    {
                        // if the coefficient is 1 only x appended with - sign in front
                        if (coefficientList[count] == -1)
                        {
                            polyBuild.Append($" - x");
                        }
                        else
                        {
                            // multiplies coefficient with x
                            polyBuild.Append($" - {coefficientList[count] * -1}x");
                        }
                    }
                    // positive numbers that have a power of 1 for a list of size 2 
                    else if (i == 1 && i == length - 1 && coefficientList[count] >= 0)
                    {
                        //if coefficient is 1 only x appended
                        if (coefficientList[count] == 1)
                        {
                            polyBuild.Append($"x");
                        }
                        // multiplies coefficient with x and appends it
                        else
                        {
                            polyBuild.Append($"{coefficientList[count]}x");
                        }
                    }
                    // negative numbers that have a power of 1 for a list of size 2 
                    else if (i == 1 && i == length - 1 && coefficientList[count] < 0)
                    {
                        //if coefficient is 1 only x appended with - sign in front
                        if (coefficientList[count] == -1)
                        {
                            polyBuild.Append($"-x");
                        }
                        // multiplies coefficient with x and appends it
                        else
                        {
                            polyBuild.Append($"{coefficientList[count]}x");
                        }
                    }

                    // positive numbers that are not the first or last ones in polynomial
                    else if (i != length - 1 && coefficientList[count] >= 0)
                    {
                        //if coefficient is 1, x raised to ith power is appended
                        if (coefficientList[count] == 1)
                        {
                            polyBuild.Append($" + x^{i}");
                        }
                        // multiplies coefficient with x raised to ith power and appends it
                        else
                        {
                            polyBuild.Append($" + {coefficientList[count]}x^{i}");
                        }
                    }
                    // negative numbers that are not the first or last ones in polynomial
                    else if (i != length - 1 && coefficientList[count] < 0)
                    {
                        //if coefficient is -1, -x raised to ith power is appended
                        if (coefficientList[count] == -1)
                        {
                            polyBuild.Append($" - x^{i}");
                        }
                        // multiplies coefficient with x raised to ith power and appends it
                        else
                        {
                            polyBuild.Append($" - {coefficientList[count] * -1}x^{i}");
                        }

                    }
                    // first number of list that isn't 0
                    else if (i == length - 1)
                    {
                        // if coefficient is 1, x raised to ith power is appended
                        if (coefficientList[count] == 1)
                        {
                            polyBuild.Append($"x^{i}");
                        }
                        // if coefficient is -1, -x raised to ith power is appended
                        else if (coefficientList[count] == -1)
                        {
                            polyBuild.Append($"-x^{i}");
                        }
                        // coefficient multiplied by x raised to ith power is appended
                        else
                        {
                            polyBuild.Append($"{coefficientList[count]}x^{i}");
                        }

                    }
                }

                count++;
            }

            // removes positive sign if it is on the left side of first term in polynomial
            if (polyBuild[1] == '+')
            {
                polyBuild.Remove(1, 1);
                polyBuild.Remove(0, 2);
            }
            // moves negative sign of first term to correct spot if necessary
            if (polyBuild[1] == '-')
            {
                polyBuild.Remove(1, 1);             
                polyBuild.Insert(2, '-');
                polyBuild.Remove(0, 2);
            }

            return polyBuild.ToString();

        }

        /// <summary>
        /// Evaluates this polynomial at the x passed to the method.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial.</param>
        /// <returns>The result of the polynomial evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomial(double x)
        {
            // if list is empty exception is thrown
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }

            double evaluatedPoly = 0;
            int count = 0;
            int length = coefficientList.Count;

            // iterates through list
            for (int i = length - 1; i >= 0; i--)
            {
                // multiplies coefficient by x raised to the ith power
                evaluatedPoly += coefficientList[count] * (Math.Pow(x, i));
                count++;
            }

            return evaluatedPoly;
        }

        /// <summary>
        /// Finds a root of this polynomial using the provided guess.
        /// </summary>
        /// <param name="guess">The initial value for the Newton method.</param>
        /// <param name="epsilon">The desired accuracy: stops when |f(result)| is
        /// less than or equal epsilon.</param>
        /// <param name="iterationMax">A max cap on the number of iterations in the
        /// Newton-Raphson method. This is to also guarantee no infinite loops.
        /// If this iterationMax is reached, a double.NaN is returned.</param>
        /// <returns>
        /// The root found using the Netwon-Raphson method. 
        /// A double.NaN is returned if a root cannot be found.
        /// The return value is rounded to have 4 digits after the decimal point.
        /// </returns>
        public double NewtonRaphson(double guess, double epsilon, int iterationMax)
        {
            int count;
            double x = guess;

            for (count = 0; Math.Abs(EvaluatePolynomial(x)) > epsilon && count < iterationMax; count++)
            {
                x -= EvaluatePolynomial(x) / EvaluatePolynomialDerivative(x);
            }

            if (count == iterationMax)
            {
                return double.NaN;
            }

            return Math.Round(x, 4); //4 decimal places
        }

        /// <summary>
        /// Calculates and returns all unique real roots of this polynomial 
        /// that can be found using the NewtonRaphson method. 
        /// The method uses all initial guesses between -50 and 50 (inclusive) with 
        /// steps of 0.5 to find all unique roots it can find. 
        /// A root is considered unique, if there is no root already found 
        /// that is within an accuracy level of 0.001 (since we rounded the roots).
        /// Uses 10 as the max number of iterations used by Newton-Raphson method.
        /// </summary>
        /// <param name="epsilon">The desired accuracy used for NewtonRaphson.</param>
        /// <returns>A list containing all the unique roots that the method finds.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public List<double> GetAllRoots(double epsilon)
        {
            // if list is empty exception is thrown
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }

            List<double> rootList = new List<double>();
            double root;
            int maxIt = 10;

            //
            for (double guess = -50; guess <= 50; guess += 0.5)
            {
                root = NewtonRaphson(guess, epsilon, maxIt);
                
                // if NewtRaphson doesn't return double.Nan 
                if (double.IsNaN(root) == false)
                {
                    // checks to see if list already contains the roots
                    if (rootList.Contains(root) == false)
                    {
                        rootList.Add(root);
                    }
                }
            }
            return rootList;
        }

        /// <summary>
        /// Evaluates the 1st derivative of this polynomial at x (passed to the method).
        /// The method uses the exact numerical technique, since it is easy to obtain the 
        /// derivative of a polynomial.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial derivative.</param>
        /// <returns>The result of the polynomial derivative evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialDerivative(double x)
        {
            
            double result=0.0;
            //count the number of coefficients to get the order of the polynomial
            double power = coefficientList.Count - 1 ;

            //if coefficientlist is empty throws an exception
            if (coefficientList==null)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }

            //calculate the derivative of polynomial using order
            
            for (int i=0; i<power; i++)
            {
                result += (power - i) * coefficientList[i] * Math.Pow(x, power - i - 1);
            }

            //return the derivative
            return result; 
        }

        /// <summary>
        /// Evaluates the definite integral of this polynomial from a to b.
        /// The method uses the exact numerical technique, since it is easy to obtain the 
        /// indefinite integral of a polynomial.
        /// </summary>
        /// <param name="a">The lower limit of the integral.</param>
        /// <param name="b">The upper limit of the integral.</param>
        /// <returns>The result of the integral evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the coefficientList field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialIntegral(double a, double b)
        {
            //count the number of coefficients to get the order of the polynomial
            double power = coefficientList.Count - 1;
            double sumA = 0.0;
            double sumB = 0.0;


            //if coefficientlist is empty throws an exception
            if (coefficientList == null)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }


            //calculate the integral of polynomial with upper and lower bounds

            for (int i = 0; i <= power; i++)
            {
                sumA += coefficientList[i] / (power - i + 1) * Math.Pow(a, power - i + 1);
                sumB += coefficientList[i] / (power - i + 1) * Math.Pow(b, power - i + 1);
            }
            // returns the definite integral
            return sumB - sumA;
        }
    }
}
