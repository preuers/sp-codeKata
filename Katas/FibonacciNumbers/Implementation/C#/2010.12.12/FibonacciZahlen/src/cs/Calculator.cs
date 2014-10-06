using System;

namespace FibonacciZahlen
{
    public class Calculator
    {
        public static int CalculateFibunacci(int n)
        {
            if(n < 0)
                throw new ArgumentOutOfRangeException("n");
            
            if (n == 0) return 0;
            if (n == 1) return 1;
            return CalculateFibunacci(n - 1) + CalculateFibunacci(n - 2);
        }
    }
}
