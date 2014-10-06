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

        public override int[] NextValues()
        {
            var nextBase = nextFromLower ? currentLowerBound : currentUpperBound;

            int subLength = GetNonVisitedGap() >= breadth ? breadth : GetNonVisitedGap();
            int subOffset = 0;
            if(!nextFromLower)
                subOffset = breadth - subLength;

            var nextArray = new int[subLength];
            for(int i = 0; i < nextArray.Length; i++)
                nextArray[i] = nextBase + subOffset + i;

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
