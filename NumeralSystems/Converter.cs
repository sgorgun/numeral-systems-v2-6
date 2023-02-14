using System;
using System.Globalization;
using System.Text;

namespace NumeralSystems
{
    /// <summary>
    /// Converts a string representations of a numbers to its integer equivalent.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-octal alphabetic characters).
        /// Valid octal alphabetic characters: 0,1,2,3,4,5,6,7.
        /// </exception>
        public static int ParsePositiveFromOctal(this string source)
        {
            foreach (var i in source)
            {
                if (!char.IsDigit(i) || i >= '9')
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in octal numeral system.");
                }
            }

            if (source[0] == '3' && source[1] == '7')
            {
                throw new ArgumentException($"{nameof(source)} does not represent a positive number in the octal numeral system.");
            }

            return Convert(source, 8);
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-decimal alphabetic characters).
        /// Valid decimal alphabetic characters: 0,1,2,3,4,5,6,7,8,9.
        /// </exception>
        public static int ParsePositiveFromDecimal(this string source)
        {
            foreach (var i in source)
            {
                if (!char.IsDigit(i) || i > '9')
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in octal numeral system.");
                }
            }

            return Convert(source, 10);
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid symbols (non-hex alphabetic characters).
        /// Valid hex alphabetic characters: 0,1,2,3,4,5,6,7,8,9,A(or a),B(or b),C(or c),D(or d),E(or e),F(or f).
        /// </exception>
        public static int ParsePositiveFromHex(this string source)
        {
            foreach (var n in source)
            {
                if (char.ToUpper(n, CultureInfo.InvariantCulture) >= 'G' && char.IsLetter(n))
                {
                    throw new ArgumentException($"{nameof(source)} does not represent a number in hex numeral system.");
                }
            }

            if (char.ToUpper(source[0], CultureInfo.InvariantCulture) == 'F')
            {
                throw new ArgumentException($"{nameof(source)} does not represent a positive number in hex numeral system.");
            }

            return Convert(source, 16);
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source string presents a negative number
        /// - or
        /// contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParsePositiveByRadix(this string source, int radix)
        {
            if (source[^source.Length] is 'F' || source[^source.Length] is '-' ||
                (source[^source.Length] == '3' && source[^(source.Length - 1)] == '7'))
            {
                throw new ArgumentException($"{nameof(source)} does not represent a signed number in numeral system.");
            }

            if (radix == 8 || radix == 10 || radix == 16)
            {
                return Convert(source, radix);
            }

            throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A signed decimal value.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if source contains invalid for given numeral system symbols
        /// -or-
        /// the radix is not equal 8, 10 or 16.
        /// </exception>
        public static int ParseByRadix(this string source, int radix)
        {
            foreach (var n in source)
            {
                if (radix == 10 && char.IsLetter(n))
                {
                    throw new ArgumentException(
                        $"{nameof(source)} does not represent a positive number in numeral system.");
                }

                if (radix == 8 && n > '7')
                {
                    throw new ArgumentException(
                        $"{nameof(source)} does not represent a positive number in numeral system.");
                }

                if (radix == 16 && n > 'F')
                {
                    throw new ArgumentException(
                        $"{nameof(source)} does not represent a positive number in numeral system.");
                }
            }

            if (radix == 8 || radix == 10 || radix == 16)
            {
                return Convert(source, radix);
            }

            throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the octal numeral system.</param>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromOctal(this string source, out int value)
        {
            value = 0;
            foreach (var n in source)
            {
                if (!char.IsDigit(n) || n >= '9')
                {
                    return false;
                }
            }

            if (source[0] == '3' && source[1] == '7')
            {
                return false;
            }

            value = Convert(source, 8);
            return true;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the decimal numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the decimal numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromDecimal(this string source, out int value)
        {
            value = 0;
            foreach (var n in source)
            {
                if (!char.IsDigit(n) || n > '9')
                {
                    return false;
                }
            }

            value = Convert(source, 10);
            return true;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the hex numeral system.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        public static bool TryParsePositiveFromHex(this string source, out int value)
        {
            value = 0;
            foreach (var n in source)
            {
                if (char.ToUpper(n, CultureInfo.InvariantCulture) >= 'G' && char.IsLetter(n))
                {
                    return false;
                }
            }

            if (char.ToUpper(source[0], CultureInfo.InvariantCulture) == 'F')
            {
                return false;
            }

            value = Convert(source, 16);
            return true;
        }

        /// <summary>
        /// Converts the string representation of a positive number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a positive number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParsePositiveByRadix(this string source, int radix, out int value)
        {
            value = 0;
            if (source[^source.Length] is 'F' || source[^source.Length] is '-' ||
                (source[^source.Length] == '3' && source[^(source.Length - 1)] == '7'))
            {
                return false;
            }

            if (radix == 8 || radix == 10 || radix == 16)
            {
                value = Convert(source, radix);
                return true;
            }

            throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
        }

        /// <summary>
        /// Converts the string representation of a signed number in the octal, decimal or hex numeral system to its 32-bit signed integer equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="source">The string representation of a signed number in the the octal, decimal or hex numeral system.</param>
        /// <param name="radix">The radix.</param>
        /// <returns>A positive decimal value.</returns>
        /// <param name="value">A positive decimal value.</param>
        /// <returns>true if s was converted successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown the radix is not equal 8, 10 or 16.</exception>
        public static bool TryParseByRadix(this string source, int radix, out int value)
        {
            value = 0;

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (var i in source)
            {
                if (radix == 10 && char.IsLetter(i))
                {
                    return false;
                }

                if (radix == 8 && i > '7')
                {
                    return false;
                }

                if (radix == 16 && i > 'F')
                {
                    return false;
                }
            }

            if (radix == 10 || radix == 8 || radix == 16)
            {
                value = Convert(source, radix);
                return true;
            }

            throw new ArgumentException($"{nameof(radix)} is 8, 10 and 16 only.");
        }

        private static int Convert(string source, int radix)
        {
            uint result = 0;
            int power = 0;
            for (int i = 1; i <= source.Length; i++)
            {
                int simbol;

                if (char.IsLetter(source[^i]))
                {
                    simbol = char.ToUpper(source[^i], CultureInfo.InvariantCulture) switch
                    {
                        'A' => 10,
                        'B' => 11,
                        'C' => 12,
                        'D' => 13,
                        'E' => 14,
                          _ => 15
                    };
                }
                else if (source[^i] == '-')
                {
                    simbol = (int)result;
                    simbol = ~simbol;
                    simbol += 1;
                    return simbol;
                }
                else
                {
                    simbol = source[^i] - '0';
                }

                result += (uint)(simbol * Math.Pow(radix, power));
                power++;
            }

            return (int)result;
        }
    }
}
