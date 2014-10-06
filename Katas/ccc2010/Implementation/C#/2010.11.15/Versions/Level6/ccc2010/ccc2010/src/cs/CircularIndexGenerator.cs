using System;

namespace ccc2010
{
    public class CircularIndexGenerator : IndexGenerator
    {
        private readonly int breadth;
        private int currentLowerBound, currentUpperBound;
        private bool nextFromLower;

        private readonly Order mainOrder;
        private Order currentSubOrder;

        public CircularIndexGenerator(int length, int start)
            : this(length, start, 1, default(Order)) {}

        public CircularIndexGenerator(int length, int start, int breadth, Order startSubOrder)
        {
            this.breadth = breadth;

            mainOrder = DetermineMainOrder(length, start, breadth, startSubOrder);
            currentSubOrder = startSubOrder;

            currentLowerBound = 0;
            currentUpperBound = length - 1 - (breadth - 1);

            nextFromLower = mainOrder == Order.Upwards ? true : false;
        }

        public override bool HasNext
        {
            get
            {
                return GetNonVisitedGap() > 0;
            }
        }

        public override int?[] Next()
        {
            var nextBase = nextFromLower ? currentLowerBound : currentUpperBound;

            int effectiveBreadth = GetNonVisitedGap() >= breadth ? breadth : GetNonVisitedGap();
            int correction = breadth - effectiveBreadth;

            nextBase = nextFromLower ? nextBase - correction : nextBase + correction;

            var nextArray = new int?[breadth];
            for (int i = 0; i < nextArray.Length; i++)
            {
                var nextValue = nextBase + i;
                if(nextValue < currentLowerBound || nextValue >= currentUpperBound + breadth)
                    nextArray[i] = null;
                else
                    nextArray[i] = nextValue;
            }

            nextArray = currentSubOrder == Order.Upwards ? nextArray : Reverse(nextArray);
            currentSubOrder = InvertOrder(currentSubOrder);

            if (nextFromLower)
                currentLowerBound += breadth;
            else
                currentUpperBound -= breadth;
            nextFromLower = !nextFromLower;

            return nextArray;
        }

        private int GetNonVisitedGap()
        {
            return currentUpperBound - currentLowerBound + breadth;
        }

        public override IndexGenerator CreateInverse()
        {
            throw new NotImplementedException();
        }
    }
}
