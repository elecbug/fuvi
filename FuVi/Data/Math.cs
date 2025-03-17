namespace FuVi.Data
{
    public class Math
    {
        public const decimal PI = 3.1415926535897932384626433833m;
        public const decimal E = 2.7182818284590452353602874713m;

        private static readonly decimal[] FactorialTable =
        {
            1m, 1m, 2m, 6m, 24m, 120m, 720m, 5040m, 40320m, 362880m,
            3628800m, 39916800m, 479001600m, 6227020800m, 87178291200m, 1307674368000m,
            20922789888000m, 355687428096000m, 6402373705728000m, 121645100408832000m,
            2432902008176640000m, 51090942171709440000m, 1124000727777607680000m,
            25852016738884976640000m, 620448401733239439360000m,
            15511210043330985984000000m, 403291461126605635584000000m,
            10888869450418352160768000000m
        };

        private static decimal Factorial(int n)
        {
            if (n < FactorialTable.Length)
            {
                return FactorialTable[n];
            }
            else
            {
                throw new InvalidDataException($"Factorial of {n} is too large.");
            }
        }
        private static decimal Power(decimal x, int n)
        {
            decimal result = 1;

            for (int i = 0; i < n; i++)
            {
                result *= x;
            }

            return result;
        }
        private static decimal NormalizeAngle(decimal x)
        {
            decimal twoPi = 2 * PI;

            while (x > PI) x -= twoPi;
            while (x < -PI) x += twoPi;

            return x;
        }
        private static decimal SinApprox(decimal x, int terms)
        {
            x = NormalizeAngle(x);
            decimal sum = 0;

            for (int n = 0; n < terms; n++)
            {
                decimal term = Power(x, 2 * n + 1) / Factorial(2 * n + 1);
                sum += (n % 2 == 0 ? term : -term);
            }

            return sum;
        }
        private static decimal LnDecimal(decimal x)
        {
            if (x <= 0) 
                throw new InvalidDataException($"Ln of {x} is invalid.");

            decimal y = (x - 1) / (x + 1);
            decimal y2 = y * y;
            decimal sum = 0;
            decimal term = y;
            int n = 1;

            while (term != 0)
            {
                sum += term / n;
                term *= y2;
                n += 2;
            }

            return sum * 2;
        }
        private static decimal ExpDecimal(decimal x, int terms)
        {
            decimal sum = 1, term = 1;

            for (int i = 1; i < terms; i++)
            {
                term *= x / i;
                sum += term;

                if (term == 0) 
                    break;
            }

            return sum;
        }
        private static decimal PowerDecimal(decimal x, int n)
        {
            if (n == 0) return 1;
            if (n == 1) return x;

            decimal result = 1;
            decimal baseValue = x;

            while (n > 0)
            {
                if ((n & 1) == 1)
                {
                    result *= baseValue;
                }

                baseValue *= baseValue;
                n >>= 1;
            }

            return result;
        }

        public static decimal Pow(decimal x, decimal y)
        {
            if (x <= 0)
                throw new InvalidDataException($"Pow of {x} is invalid.");

            return ExpDecimal(y * LnDecimal(x), 50);
        }
        public static decimal Pow(decimal x, int y)
        {
            return PowerDecimal(x, y);
        }
        public static decimal Sin(decimal x)
        {
            return SinApprox(x, 10);
        }
        public static decimal Cos(decimal x)
        {
            return SinApprox(x + PI / 2, 10);
        }
    }
}
