namespace ccc2010
{
    public class SequenceIndexGenerator : IndexGenerator
    {
        private readonly int length, start, breadth;
        private readonly Order startSubOrder;

        private int currentIndex;
        private readonly Order mainOrder;
        private Order currentSubOrder;

        public SequenceIndexGenerator(int length, int start)
            : this(length, start, 1, default(Order)){}

        public SequenceIndexGenerator(int length, int start, int breadth, Order startSubOrder)
        {
            this.start = start;
            this.length = length;
            this.breadth = breadth;
            this.startSubOrder = startSubOrder;

            mainOrder = DetermineMainOrder(length, start, breadth, startSubOrder);
            currentSubOrder = startSubOrder;
        }

        public override bool HasNext
        {
            get { return currentIndex < length; }
        }

        public override int[] NextValues()
        {
            var nextBase = mainOrder == Order.Upwards ? currentIndex : length - currentIndex - (breadth - 1) - 1;
            if (nextBase < 0)
                nextBase = 0;

            int subLength = currentIndex + breadth > length ? length - currentIndex : breadth;

            var nextArray = new int[subLength];
            for(int i = 0; i < nextArray.Length; i++)
                nextArray[i] = nextBase + i;

            nextArray = currentSubOrder == Order.Upwards ? nextArray : Reverse(nextArray);
            currentSubOrder = InvertOrder(currentSubOrder);

            currentIndex += breadth;
            return nextArray;
        }

        public override IndexGenerator CreateInverse()
        {
            return mainOrder == Order.Upwards ? new SequenceIndexGenerator(length, length - 1, breadth, startSubOrder) : new SequenceIndexGenerator(length, 0, breadth, startSubOrder);
        }
    }
}
