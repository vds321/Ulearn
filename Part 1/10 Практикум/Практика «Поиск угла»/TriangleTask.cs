using NUnit.Framework;
using System;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
        /// </summary>
        public static double GetABAngle(double a, double b, double c)
        {
            if (a == 0 || b == 0) return double.NaN;
            if (c == 0) return 0.0;
            if (a + b > c && a + c > b && b + c > a)
            {
                return Math.Acos((a * a + b * b - c * c) / (2.0 * a * b));
            }
            return double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(1, 1, 0, 0)]
        [TestCase(1, 2, 1, double.NaN)]
        [TestCase(-1, 2, 3, double.NaN)]
        // добавьте ещё тестовых случаев!
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            //Assert.Fail("Not implemented yet");
            Assert.AreEqual(expectedAngle, TriangleTask.GetABAngle(a, b, c), 1e-5);
        }
    }
}