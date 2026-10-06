using System;

namespace Lab3
{
    class SeriesCalculator
    {
        private double x;
        private int n;
        private double eps;

        public SeriesCalculator(double x, int n, double eps)
        {
            this.x = x;
            this.n = n;
            this.eps = eps;
        }

        public double ComputeSN()
        {
            double sum = x;
            double t = x;
            for (int j = 1; j <= n; j++)
            {
                t = -t * x * x * (2 * j - 1) / (2 * j + 1);
                sum = sum + t;
            }
            return sum;
        }

        public double ComputeSE()
        {
            double sum = 0;
            double t = x;
            int m = 1;
            while (Math.Abs(t) >= eps)
            {
                sum = sum + t;
                m = m + 1;
                t = -t * x * x * (2 * m - 1) / (2 * m + 1);
            }
            return sum;
        }

        public double ComputeY()
        {
            return Math.Atan(x);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            double a = 0.1;
            double b = 1.0;
            int k = 10;
            int n = 40;
            double eps = 0.0001;
            double h = (b - a) / k;

            Console.WriteLine("Вычисление функции arctg(x)");
            Console.WriteLine();

            for (int i = 0; i <= k; i++)
            {
                double x = a + i * h;
                SeriesCalculator calc = new SeriesCalculator(x, n, eps);

                double sn = calc.ComputeSN();
                double se = calc.ComputeSE();
                double y = calc.ComputeY();

                Console.Write("X=" + x.ToString("F2"));
                Console.Write("  SN=" + sn.ToString("F6"));
                Console.Write("  SE=" + se.ToString("F6"));
                Console.WriteLine("  Y=" + y.ToString("F6"));
            }

            Console.ReadKey();
        }
    }
}