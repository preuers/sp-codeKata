using System;

namespace ccc2010
{
    public class CircularIndexGenerator : IndexGenerator
    {
        private int start;
        private int lowerBound, upperBound;
        private bool nextFromLower;

        public CircularIndexGenerator(int length, int start)
        {
            if(start != 0 && start != length - 1)
                throw new ArgumentOutOfRangeException("start");

            this.start = start;
            lowerBound = 0;
            upperBound = length - 1;

            nextFromLower = start == 0 ? true : false;
        }

        public override int StartValue
        {
            get { return start; }
        }

        public override bool HasNext
        {
            get { return lowerBound <= upperBound; }
        }

        public override int Next()
        {
            var next = nextFromLower ? lowerBound++ : upperBound--;
            nextFromLower = !nextFromLower;
            return next;
        }

        public override IndexGenerator CreateSwaped()
        {
            throw new NotImplementedException();
        }
    }
}
