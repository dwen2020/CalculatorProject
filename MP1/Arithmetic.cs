//MP1 
//This file contains the Arithmethic class.

//You should implement the requested methods.

using System;
using System.Text;

namespace MP1
{
    public class Arithmetic
    {
        /// <summary>
        /// Use this method as is.
        /// It is called by Main and is used to get an expression from console.
        /// The method calls the EvaluateExpression and OutputExpression methods. 
        /// </summary>
        /// <returns>
        /// A string formed from the formatted expression and the evaluation 
        /// result, or the error message "Invalid expression".
        /// </returns>
        public static string BasicArithmetic()
        {
            Console.WriteLine();
            Console.WriteLine("Basic arithmetic opertions with + - * / ^");
            Console.WriteLine("Enter an expression:");
            string expression = Console.ReadLine().Trim();

            double result = EvaluateExpression(expression);
            if (double.IsNaN(result))
                return "Invalid expression";
            return $"{OutputExpression(expression)} = {result}";
        }

        /// <summary>
        /// Return the numerical evaluation of the arithmetic expression passed to it.
        /// 
        /// Prcedence of the operators (from highest to lowest):
        ///    parenthesis
        ///    power
        ///    multiplication and division (equal precedence)
        ///    addition and subtraction (equal precedence)
        /// An inner parentheses has higher precedence.
        /// * and / have the same precedence.
        /// + and - have the same precedence.
        /// 
        /// All arithmetic operators are left-associative.
        /// 
        /// - can be used as the negative sign as well as subtraction.
        /// There must not be any space between the negative sign and the number.
        /// + is only used for addition (i.e. is not allowed to be used as a positive sign)
        /// </summary>
        /// <param name="expression">
        /// The user input string (with Trim() already applied)
        /// </param>
        /// <returns>
        /// Returns the result of a successful evaluation of the expression,
        /// or double.NaN if the expression is not valid.
        /// </returns>
        /// <example>
        /// If the user expression is "2.1 + 3" or "2.1+ 3" or "2.1 +3", 
        /// then the method returns 5.1
        /// If the user expression is "(8 + -3) * (2 ^ 5)" or "(8 + -3) * 2 ^ 5", 
        /// then the method returns 160 
        /// If the user expression is "2 + ((3 * 2) * 5)" it returns 32 
        /// A 0 before decimal point is not mandatory, so .52 is equivalent to 0.52
        /// Any extra spaces are fine, so if the user enters "  2   ^ 3 ", then 
        /// the method returns 8
        /// If the user input is any incorrect or unbalanced expression, for example,
        /// "4 5" or "4 +" or "+4" or " (4 + 5" or "4 + 5 * 4)", then the method 
        /// returns double.NaN
        /// </example>
        public static double EvaluateExpression(string expression)
        {
            double result;
            var expList = new List<string>();

            if (IsValid(expression))
                //If we confirm the expression is a valid one
            {
                expression = OutputExpression(expression);
                //We call OutputExpression to simplify the formatting and clean up spacing

                var expressionEdit = new StringBuilder(expression);
                expressionEdit.Replace("(", "( ");
                expressionEdit.Replace(")", " )");
                expression = expressionEdit.ToString();
                //We create a mutable stringbuilder to add spacing beside all the parentheses in the expression
                //We need this to have the parentheses be counted as individual items in the list without removing them
                //with string.split

                string[] tempArray = expression.Split(' ');
                //Splitting the string into individual list elements by spaces

                for (int x = 0; x < tempArray.Length; x++)
                {
                    expList.Add(tempArray[x]);
                }
                //Adding all the elemts from the created array to a listo so it is easier to work with

                expList = ShuntingYard(expList);

                if (expList.Count == 0)
                {
                    return double.NaN;
                }
                //If the shuntingyard method returns an empty list, we know the expression is invalid

                result = Calculation(expList); 
                //If expression ends up being invalid in Calculation method, we will end up returning double.NaN
            }
            else
            {
                return double.NaN; //Returning not a number if the method IsValid() fails
            } 
            
            return result;
        }

        /// <summary>
        /// Returns a modestly cleaned up version of the input expression 
        /// </summary>
        /// <param name="expression">
        /// The user input string (with Trim() already applied)
        /// </param>
        /// <returns>
        /// Returns a string that is rather similar to the expression the user entered, with: 
        /// All extra spaces are removed, but it is ensured that a space is on the either sides of any binary arithmetic operator.
        /// For any negative number being subtracted from another number, it is changed to the addition with its absolute value.
        /// for any negative number being added to another number, it is changed to the subtraction of its absolute value.
        /// </returns>
        /// <example>
        /// If the expression is "2.1 + 3" or "2.1+3" or "2.1+ 3" or "2.1 +3" or "2.1 +    3" 
        ///       then the method returns "2.1 + 3".
        /// If the expression is "2.1 - 3" or "2.1-3" or "2.1- 3" or "2.1 -3" or "2.1 -    3" 
        ///       then the method returns "2.1 - 3".
        /// If the expression is "2.1 + -3" or "2.1+-3" or "2.1 - 3"  
        ///       then the method returns "2.1 - 3".
        /// If the expression is "2.1 - -3" or "2.1--3" or "2.1 + 3"  
        ///       then the method returns "2.1 + 3".
        /// If the expression is "2.1 *   -3" or "2.1*-3"  
        ///       then the method returns "2.1 * -3".
        /// If the expression is "( 2 +  3 )   * 2 ^ 5"
        ///       it returns "(2 + 3) * 2 ^ 5" 
        /// If the expression is "2 + ( ( 3 * 2) *  5)" it returns "2 + ((3 * 2) * 5)" 
        /// Any extra spaces are fine, so if the original user input is "  2   ^ 3 " then 
        ///     the method returns "2 ^ 3".
        /// </example>
        public static string OutputExpression(string expression)
        {
            StringBuilder condensedExp = new StringBuilder(expression); //Create a new StringBuilder as strings are immutable
            
            condensedExp.Replace(" ", ""); //Replacing all instances of spacing with no spacing (removing all spaces)

            for (int x = 0; x < condensedExp.Length; x++) //Cycling through every char in the string
            {
                if (condensedExp[x].Equals('+') && condensedExp[x + 1].Equals('-'))
                //Checking to see if there is a + followed by a -
                {
                    condensedExp.Remove(x, 1); //If so, removing the + so it has only a - left
                }
                else if (condensedExp[x].Equals('-') && condensedExp[x + 1].Equals('-'))
                //Checking to see if there are two - following one another
                //Note we use an else if here as there should never be an instance where +-- exists
                {
                    condensedExp[x] = '+'; //If so, replacing the first - with a +
                    condensedExp.Remove(x + 1, 1); //Removing the next - at x+1
                }
            }

            for (int x = 0; x < condensedExp.Length; x++) //Cycling through all chars in the string
            {
                if (condensedExp[x].Equals('-') && !(condensedExp[x-1].Equals('*') || condensedExp[x-1].Equals('/') ||
                    condensedExp[x-1].Equals('^') || condensedExp[x-1].Equals('(')))
                //Checking to see if we find a - that is not preceded by any operand or parantheses
                //If the - is preceded by an operand/parantheses, we know that the - is being used to indicate negativity
                //and not being used as the subtraction operand. If it is the sub operand, we want to add spacing
                {
                    condensedExp[x] = ' '; //Replacing the - sign with a space at x
                    condensedExp.Insert(x+1, "- "); //Adding a - followed by a space at x+1
                    x++; //It should not check the same negative sign again (when it is in its new index of x+1)
                }
            }

            //Adding spaces to both sides of any operand in the string (excluding subtraction operand)
            condensedExp.Replace("+", " + ");
            condensedExp.Replace("/", " / ");
            condensedExp.Replace("*", " * ");
            condensedExp.Replace("^", " ^ ");

            return condensedExp.ToString(); //Returning the result as a string (converting from a StringBuilder)
        }

        /// <summary>
        /// Checks to see if a given mathematical expression is valid, that is has an equal number
        /// of parantheses and has correct spacing between the decimal point and negative sign and numbers.
        /// </summary>
        /// <example>
        /// A valid expression could be: "4 + 8 * (6 - 7)", "9 + .6", " 7 - -2"
        /// An invalid expression could be: "(8 * 5", "9 + . 6", "7 -- 2"
        /// </example>
        /// <param name="expression">The mathematical expression to evaluate the validity of 
        /// and can contain numbers, parantheses, and operators (must be one of +, -, *, /, or ^).</param>
        /// <returns>True if the expression is valid as described above, false otherwise.</returns>
        static bool IsValid(string expression)
        {
            int parantheses = 0, operatorCount = 0;

            for (int x = 0; x < expression.Length; x++)
                //We iterate through the expression's characters to check for multiple things:
            {
                if (expression[x].Equals('(')) //We are counting the number of parentheses by increasing...
                {
                    parantheses++;
                } 
                else if (expression[x].Equals(')')) //... and decreasing the number of open parentheses
                {
                    parantheses--;
                }
                else if (expression[x].Equals('.')) //If we encounter a decimal point, we need to ensure that
                {
                    if (!char.IsDigit(expression[x+1])) //it is followed by a number, otherwise it is invalid
                    {
                        return false; //Better to fail fast -> we indicate right away it's invalid
                    }
                }
                else if (IsOperator(expression[x].ToString()))
                //We count the number of operators in the expression so we can ensure there are no unnecessary spaces
                //are present between the negative sign and the number (but not the minus operator)
                {
                    operatorCount++;
                }
                else if (char.IsDigit(expression[x]) && operatorCount > 0)
                    //If we have a number and the operator count is above 0, we reset it as we are counting only for
                    //operators in a row (e.g. 5 * -9)
                {
                    operatorCount = 0;
                }

                if (operatorCount == 2 && char.IsDigit(expression[x+1]) == false)
                    //If we find that there are two operators in a row and the next term isn't a number, we determine it isn't valid
                    //E.g. 5 * - 9 would fail while 5 * -9 would pass
                {
                    return false;
                }
            }

            if (parantheses != 0 || expression == null)
            {
                return false;
            }
            //Indicating its not valid if we have an uneven # of parentheses or a null expression

            return true;
        }

        /// <summary>
        /// Uses the Shunting Yard Algorithm to simplify a given mathematical expression into Reverse
        /// Polish Notation and separates each term into a list element (for simple evaluation and calculation).
        /// If the expression is correct and balanced (does not contain numbers not separated with an operator 
        /// or vice versa, it returns an empty list.
        /// </summary>
        /// <param name="expression">The expression to simplify that contains real numbers,
        /// parantheses, and operators from the following: +, -, *, /, and ^.</param>
        /// <returns>A list of numbers and operators in Reverse Polish Notation for calculation if the expression is
        /// correct and balanced, an empty list otherwise.</returns>
        public static List<string> ShuntingYard(List<string> expression)
        {
            bool isValid = true;
            int counter = 0;
            var expQueue = new Queue<string>();
            var operatorStack = new Stack<string>();
            List<string> expRPN = new List<string>(); //RPN = Reverse Polish Notation

            while (counter < expression.Count && isValid == true)
                //Iterating through the list of strings while ensuring that it remains valid
            {
                if (double.TryParse(expression[counter], out _) && (counter < 1 || IsOperator(expression[counter - 1]) == true))
                    //If the current element is a number, it must either be the first element or be proceded by an operator
                    //Discarding the out value from TryParse since it's not needed (checked with professor)
                {
                    expQueue.Enqueue(expression[counter]); //We add the number to the queue if it is
                }
                else if (expression[counter].Equals("(") && (counter < 1 || IsOperator(expression[counter - 1]) == true))
                    //If the current element is a left parenthesis, it must either be the first element or be proceded by an operator
                {
                    operatorStack.Push(expression[counter]); //We push it to the top of the stack to indicate an open parenthesis
                }
                else if (expression[counter].Equals(")") && (double.TryParse(expression[counter - 1], out _) == true))
                    //If the current element is a right parenthesis, it must be proceded by a number (any double)
                {
                    while (!operatorStack.Peek().Equals("("))
                    {
                        expQueue.Enqueue(operatorStack.Pop());
                    }
                    //Iterate through the stack of operators, adding them to the queue until we find the matching left parenthesis
                    //Note that we don't have a scenario for when we can't find anotehr parenthesis, as we already checked previously
                    //to ensure we had no mismatched number of them

                    if (operatorStack.Peek().Equals("("))
                    {
                        operatorStack.Pop();
                    }
                    //Once we find the left parenthesis, we can discard it along with the right one as the RPN already indicates order
                }
                else if (IsOperator(expression[counter]) && (double.TryParse(expression[counter - 1], out _) == true) || expression[counter-1].Equals(")"))
                    //If the current element is an operator, it must either be proceded by a number (any double) or a right parenthesis
                {
                    while (operatorStack.Count > 0 && HasPrecedence(operatorStack.Peek(), expression[counter]) == true)
                        //As long as there is something in the stack and it has precedence or is of equal precedence to the current operator,
                    {
                        expQueue.Enqueue(operatorStack.Pop()); //we put it in the queue
                    }
                    operatorStack.Push(expression[counter]); //We finally add the new operator to the stack
                }
                else
                {
                    isValid = false; //If none of the above are true, we have an incorrect equation (a missing operator, two operators in a row, etc.)
                }
                counter++;
            }

            while (operatorStack.Count > 0)
            {
                expQueue.Enqueue(operatorStack.Pop());
            }
            //At the end, we iterate through the stack of operators and add any remaining ones to the queue

            while (expQueue.Count > 0)
            {
                expRPN.Add(expQueue.Dequeue());
            }
            //Finally, we move every element from the queue to a new list so we can return the list for calculation

            if (isValid == false)
            {
                return new List<string>(); //If the flag variable has been flagged, we return an empty list so we know it's invalid
            }

            return expRPN;
        }

        /// <summary>
        /// Determines whether a given character is an operator (+, -, *, /, or ^) or a paranthesis
        /// </summary>
        /// <param name="character">The character to check</param>
        /// <returns>True if character is a valid operator/paranthesis, false otherwise</returns>
        static bool IsOperator(string character)
        {
            //We check if the string parameter is one of the operators in our program or a left/right parenthesis
            if (character.Equals("+") || character.Equals("-") || character.Equals("*") || character.Equals("/")
                || character.Equals("(") || character.Equals(")") || character.Equals("^"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Takes in two operators and determines which one yields precedence according to order
        /// of operations rules in mathematics (also known as PEDMAS/PEMDAS).
        /// </summary>
        /// <param name="character1">Operator being compared (must be one of +, -, *, /, (, or ^)</param>
        /// <param name="character2">Operator to compare to (must be one of +, -, *, /, (, or ^)</param>
        /// <returns>True if the first operator has a higher or equal precedence to the second;
        /// false if the second has a higher precedence or either one is a left paranthesis.</returns>
        static bool HasPrecedence(string character1, string character2)
        {
            int value1 = 0, value2 = 0;

            if (character1.Equals("^"))
            {
                value1 = 1;
            }
            else if (character1.Equals("*") || character1.Equals("/"))
            {
                value1 = 2;
            }
            else if (character1.Equals("+") || character1.Equals("-"))
            {
                value1 = 3;
            }

            if (character2.Equals("^"))
            {
                value2 = 1;
            }
            else if (character2.Equals("*") || character2.Equals("/"))
            {
                value2 = 2;
            }
            else if (character2.Equals("+") || character2.Equals("-"))
            {
                value2 = 3;
            }

            if (value1 > value2 || character1.Equals("(") || character2.Equals("("))
            {
                return false;
            }
            //Determines which operator has precedence or if they have the same precedence 

            return true;
        }

        /// <summary>
        /// Takes an expression in Reverse Polish Notation and evaluates it, yielding a numerical
        /// answer to the expression. 
        /// </summary>
        /// <param name="expression">A mathematical expression in Reverse Polish Notation using
        /// numbers and operators of the following: +, -, *, /, and ^.</param>
        /// <returns>A numerical answer to the expression if it can be evaluated, else NaN.</returns>
        public static double Calculation(List<string> expression)
        {
            var numberStack = new Stack<double>();

            for (int x = 0; x < expression.Count; x++)
                //Iterating through all the strings in the list
            {
                if (double.TryParse(expression[x], out double number))
                    //Pushing numbers onto the stack
                {
                    numberStack.Push(number);
                }
                else if (IsOperator(expression[x]))
                {
                    double newNum = numberStack.Pop(), oldNum = numberStack.Pop();
                    numberStack.Push(PerformOperation(oldNum, newNum, expression[x]));
                    //Performing operations from the operator in the list on the latest 2 numbers placed in the stack
                    //We get the old and new numbers and perform an operation on them
                }
            }
            
            if (numberStack.Count == 1)
            {
                return numberStack.Pop();
            }
            //We reach an answer once the stack has one number left!
            else
            {
                return double.NaN;
            }
            //If we have more than one number/element, we have an issue and the expression is invalid
        }

        /// <summary>
        /// Performs a mathematical operation (indicated by the operator provided in the paramater) on the
        /// two doubles provided. Addition, subtracrtion, multiplication, division, and exponents are all options.
        /// </summary>
        /// <param name="digit1">The first number for the provided operation (or the base for exponents)</param>
        /// <param name="digit2">The second number for the provided operation (or the power for exponents)</param>
        /// <param name="operator1">The operator representing the operation to be performed;
        /// must be one of: +, -, *, /, or ^
        /// </param>
        /// <returns>The result of the calculation</returns>
        static double PerformOperation(double digit1, double digit2, string operator1)
        {
            double result = 0;

            if (operator1.Equals("+"))
            {
                result = digit1 + digit2;
            } 
            else if (operator1.Equals("-"))
            {
                result = digit1 - digit2;
            }
            else if (operator1.Equals("*"))
            {
                result = digit1 * digit2;
            }
            else if (operator1.Equals("/"))
            {
                result = digit1 / digit2;
            }
            else if (operator1.Equals("^"))
            {
                result = Math.Pow(digit1, digit2);
            }
            //We perform a calculation on the two numbers presented to us based on the passed operator
            //Since the operator is a string, we can't just use it with doubles directly

            return result;
        }
    }
}