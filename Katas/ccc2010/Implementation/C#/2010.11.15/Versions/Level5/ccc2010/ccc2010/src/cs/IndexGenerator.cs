using System;

namespace ccc2010
{
    public abstract class IndexGenerator
    {
        public abstract bool HasNext { get; }
        public abstract int[] NextValues();

        public abstract IndexGenerator CreateInverse();

        protected static Order DetermineMainOrder(int length, int start, int breadth, Order startSubOrder)
        {
            Order mainOrder;
            if (start == 0)
                mainOrder = Order.Upwards;
            else if (start == length - 2 && startSubOrder == Order.Upwards)
                mainOrder = Order.Downwards;
            else if (start == (breadth - 1) && startSubOrder == Order.Downwards)
                mainOrder = Order.Upwards;
            else if (start == length - 1)
                mainOrder = Order.Downwards;
            else
                throw new ArgumentException();

            return mainOrder;
        }

        protected static int[] Reverse(int[] array)
        {
            var result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                result[i] = array[array.Length - i - 1];
            }
            return result;
        }

        protected static Order InvertOrder(Order order)
        {
            if (order == Order.Upwards)
                return Order.Downwards;
            if (order == Order.Downwards)
                return Order.Upwards;
            throw new ArgumentException();
        }
    }
}
