using System;

namespace ccc2010
{
    public class SequenceIndexGenerator : IndexGenerator
    {
        private int start;
        private int length;
        private int currentIndex;
        private bool upwards;

        public SequenceIndexGenerator(int length, int start)
        {
            if(start != 0 && start != length - 1)
                throw new ArgumentOutOfRangeException("start");

            this.start = start;
            this.length = length;
            upwards = start == 0 ? true : false;
        }

        public override int StartValue
        {
            get { return start; }
        }

        public override bool HasNext
        {
            get { return currentIndex < length; }
        }

        public override int Next()
        {
            return upwards ? currentIndex++ : length - currentIndex++ - 1;
        }

        public override IndexGenerator CreateSwaped()
        {
            return upwards ? new SequenceIndexGenerator(length, length - 1) : new SequenceIndexGenerator(length, 0);
        }
    }
}
