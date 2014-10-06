using System;

namespace Fractal
{
    public class Calculator
    {
        public static double CalculateFor(FractalType type, double length, int iterations)
        {
            var initialEdges = DetermineInitialEdgesFor(type);
            var bumpFactor = DeterminePumpFactorFor(type);
            var calculator = new SingleFractalCalculator(bumpFactor);
            double singleResult = calculator.CalculateFor(length, iterations);
            return singleResult*initialEdges;
        }

        private static int DeterminePumpFactorFor(FractalType type)
        {
            return DetermineInitialEdgesFor(type) + 1;
        }

        private static int DetermineInitialEdgesFor(FractalType type)
        {
            switch (type)
            {
                case FractalType.Triangle:
                    return 3;
                case FractalType.Square:
                    return 4;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        private class SingleFractalCalculator
        {
            private readonly int bumpFactor;

            internal SingleFractalCalculator(int bumpFactor)
            {
                this.bumpFactor = bumpFactor;
            }

            internal double CalculateFor(double length, int iterations)
            {
                int numberOfEdges = 1;
                double edgeLength = length;

                for (int i = 0; i < iterations; i++)
                {
                    numberOfEdges = numberOfEdges*bumpFactor;
                    edgeLength = edgeLength/3;
                }

                return numberOfEdges*edgeLength;
            }

            // alternative recursive solution
            private double RecusivelyCalculateFor(double length, int iterationsLeft)
            {
                if (iterationsLeft <= 0)
                    return length;

                return bumpFactor*RecusivelyCalculateFor(length/3, iterationsLeft - 1);
            }

        }
    }
}
