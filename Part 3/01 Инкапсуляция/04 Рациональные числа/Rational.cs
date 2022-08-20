using System;
using System.Numerics;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public int Numerator { get; }
        public int Denominator { get; }
        public bool IsNan { get; }

        public Rational(int numerator, int denominator = 1)
        {
            if (denominator == 0)
            {
                IsNan = true;
                return;
            }
            var gcd = (int)BigInteger.GreatestCommonDivisor(denominator, numerator);
            Numerator = denominator < 0 ? -1 * numerator / gcd : numerator / gcd;
            Denominator = denominator < 0 ? -1 * denominator / gcd : denominator / gcd;
        }

        public static Rational operator +(Rational r1, Rational r2)
        {
            return new Rational(r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator,
                r1.Denominator * r2.Denominator);
        }

        public static Rational operator -(Rational r1, Rational r2)
        {
            return new Rational(r1.Numerator * r2.Denominator - r2.Numerator * r1.Denominator,
                r1.Denominator * r2.Denominator);
        }

        public static Rational operator *(Rational r1, Rational r2)
        {
            return new Rational(r1.Numerator * r2.Numerator,
                r1.Denominator * r2.Denominator);
        }

        public static Rational operator /(Rational r1, Rational r2)
        {
            if (r2.IsNan) return r2;
            return new Rational(r1.Numerator * r2.Denominator,
                r1.Denominator * r2.Numerator);
        }

        public static implicit operator double(Rational r)
        {
            if (r.IsNan) return double.NaN;
            return (double)r.Numerator / r.Denominator;
        }

        public static explicit operator int(Rational r)
        {
            if (r.Numerator % r.Denominator == 0) return r.Numerator / r.Denominator;
            throw new ArgumentException();
        }

        public static implicit operator Rational(int v)
        {
            return new Rational(v);
        }
    }
}
