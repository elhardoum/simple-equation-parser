using System;
using Xunit;
using Assignment1;

namespace Assignment1.Test
{
    /**
     *  Test cases provided by Alex Brown
     *  @link https://bitbucket.org/AlexBrownBVC/sodv2202-assignment-1-starter/src/5ae54715f63826b08272accff6e7324be8d77889/Assignment1Tests/ProgramTests.cs
     */
    public class UnitTest1
    {
        [Fact]
        public void TestAddition()
        {
            {
                var command = "1+2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(3, result);
            }
            {
                var command = "755 + 86";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(841, result);
            }
        }

        [Fact]
        public void TestSubtraction()
        {
            {
                var command = "6-4";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(2, result);
            }
            {
                var command = "7890 - 987";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(6903, result);
            }
            {
                var command = "17 - 98";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(-81, result);
            }
        }

        [Fact]
        public void TestAdditionSubtraction()
        {
            {
                var command = "2+2+7";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(11, result);
            }
            {
                var command = "1000 - 200 - 75";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(725, result);
            }
            {
                var command = "90 + 75 - 56 + 7 - 2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(114, result);
            }
        }

        [Fact]
        public void TestMultiplication()
        {
            {
                var command = "3*7";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(21, result);
            }
            {
                var command = "17 * 12";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(204, result);
            }
        }

        [Fact]
        public void TestDivision()
        {
            {
                var command = "6/2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(3, result);
            }
            {
                var command = "750 / 25";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(30, result);
            }
            {
                var command = "9 / 7";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(9.0 / 7.0, result);
            }
        }

        [Fact]
        public void TestMultiplicationDivision()
        {
            {
                var command = "1*2*3";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(6, result);
            }
            {
                var command = "700 / 7 / 40";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(2.5, result);
            }
            {
                var command = "12 * 3 / 4 * 6 / 4";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(13.5, result);
            }
        }

        [Fact]
        public void TestMultipleOperations()
        {
            {
                var command = "2 * 3 + 4";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(10, result);
            }
            {
                var command = "12 / 3 - 8";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(-4, result);
            }
            {
                var command = "18 * 3 / 9 + 100 - 97";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(9, result);
            }
        }

        [Fact]
        public void TestOrderOfOperations()
        {
            {
                var command = "6/2+3*4";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(15, result);
            }
            {
                var command = "11 - 5/2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(8.5, result);
            }
            {
                var command = "12-3 * 3 -8";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(-5, result);
            }
        }

        [Fact]
        public void TestWhitespace()
        {
            {
                var command = "     10     +12    ";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(22, result);
            }
            {
                var command = " 5  *3   +2   -5*2 ";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(7, result);
            }
        }

        [Fact]
        public void TestNegativeNumbers()
        {
            {
                var command = "-2 + 3";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(1, result);
            }
            {
                var command = "12 - -2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(14, result);
            }
            {
                var command = "12 + -2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(10, result);
            }
        }

        [Fact]
        public void TestDecimals()
        {
            {
                var command = "2.1+3.3";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(5.4, result);
            }
            {
                var command = "2.891*21.67+10.0";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(72.64797, Math.Round(result, 5));
            }
        }

        [Fact]
        public void TestBrackets()
        {
            {
                var command = "(1+1)*2";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(4, result);
            }
            {
                var command = "(10+2) / (5-3)";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(6, result);
            }
        }

        [Fact]
        public void TestNestedBrackets()
        {
            {
                var command = "((1+1))";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(2, result);
            }
            {
                var command = "((9-3)*2)+1";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(13, result);
            }
        }

        [Fact]
        public void TestIntegrated()
        {
            {
                var command = "(2 *     (3+4.5)- 1.2 * (4 - 3.5)) /-3";
                var result = Double.Parse((new EquationParser(command)).evaluate());
                Assert.Equal(-4.8, result);
            }
        }
    }
}
