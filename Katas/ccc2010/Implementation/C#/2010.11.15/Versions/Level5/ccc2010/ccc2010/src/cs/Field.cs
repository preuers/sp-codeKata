using System;

namespace ccc2010
{
    public class Field
    {
        private readonly DimensionDescription main;
        private DimensionDescription sub;

        private readonly int columns;

        public Field(int rows, int columns) : this(rows, columns, 1, 1)
        {
        }

        public Field(int rows, int columns, int startRow, int startColumn)
        {
            this.columns = columns;

            var mainDescriptionGenerator = new DimensionDescriptionGenerator
                                               {
                                                   Length = rows,
                                                   Assignment = DimensionAssignment.Row,
                                                   Breadth = 1,
                                                   Mode = TraverseMode.Serpentine
                                               };
            SetStartSimple(mainDescriptionGenerator, startRow, rows);

            var subDescriptionGenerator = new DimensionDescriptionGenerator
                                              {
                                                  Length = columns,
                                                  Assignment = DimensionAssignment.Column,
                                                  Breadth = 1,
                                                  Mode = TraverseMode.Serpentine
                                              };
            SetStartSimple(subDescriptionGenerator, startColumn, columns);

            main = mainDescriptionGenerator.Generate();
            sub = subDescriptionGenerator.Generate();
        }

        private static void SetStartSimple(DimensionDescriptionGenerator generator, int startNumber, int length)
        {
            if (startNumber == 1)
                generator.Start = 0;
            else if (startNumber == length)
                generator.Start = length - 1;
            else
                throw new ArgumentException();
        }

        public Field(int rows, int columns, int startRow, int startColumn, char startDirection)
            : this(rows, columns, startRow, startColumn, startDirection, 'S')
        {
        }

        public Field(int rows, int columns, int startRow, int startColumn, char startDirection, char mode)
            : this(rows, columns, startRow, startColumn, startDirection, mode, 1)
        {
        }

        public Field(int rows, int columns, int startRow, int startColumn, char startDirection, char mode, int breadth)
        {
            this.columns = columns;

            TraverseMode mainMode;
            switch (mode)
            {
                case 'S':
                    mainMode = TraverseMode.Serpentine;
                    break;
                case 'Z':
                    mainMode = TraverseMode.Circular;
                    break;
                default:
                    throw new ArgumentException();
            }

            Order startSubOrder;
            switch (startDirection)
            {
                case 'N':
                    startSubOrder = Order.Upwards;
                    break;
                case 'S':
                    startSubOrder = Order.Downwards;
                    break;
                case 'O':
                    startSubOrder = Order.Upwards;
                    break;
                case 'W':
                    startSubOrder = Order.Downwards;
                    break;
                default:
                    throw new ArgumentException();
            }

            const TraverseMode subMode = TraverseMode.Serpentine;

            var rowDescriptionGenerator = new DimensionDescriptionGenerator {Length = rows, Start = startRow - 1, Assignment = DimensionAssignment.Row};
            var columnDescriptionGenerator = new DimensionDescriptionGenerator {Length = columns, Start = startColumn - 1, Assignment = DimensionAssignment.Column};

            DimensionDescriptionGenerator mainDescriptionGenerator, subDescriptionGenerator;
            if(startDirection == 'N' || startDirection == 'S')
            {
                mainDescriptionGenerator = columnDescriptionGenerator;
                subDescriptionGenerator = rowDescriptionGenerator;
            } else if(startDirection == 'O' || startDirection == 'W')
            {
                mainDescriptionGenerator = rowDescriptionGenerator;
                subDescriptionGenerator = columnDescriptionGenerator;
            } else
            {
                throw new ArgumentException();
            }

            mainDescriptionGenerator.Breadth = breadth;
            mainDescriptionGenerator.Mode = mainMode;
            mainDescriptionGenerator.StartSubOrder = startSubOrder;
            subDescriptionGenerator.Breadth = 1;
            subDescriptionGenerator.Mode = subMode;

            main = mainDescriptionGenerator.Generate();
            sub = subDescriptionGenerator.Generate();
        }

        private class DimensionDescriptionGenerator
        {
            public int Length { private get; set; }
            public int Start { private get; set; }
            public DimensionAssignment Assignment { private get; set; }
            public TraverseMode Mode { private get; set; }
            public int Breadth { private get; set; }
            public Order StartSubOrder { private get; set; }

            public DimensionDescription Generate()
            {
                IndexGenerator indexGenerator;
                if (Mode == TraverseMode.Serpentine)
                    indexGenerator = new SequenceIndexGenerator(Length, Start, Breadth, StartSubOrder);
                else if (Mode == TraverseMode.Circular)
                    indexGenerator = new CircularIndexGenerator(Length, Start, Breadth, StartSubOrder);
                else
                    throw new InvalidOperationException();

                return new DimensionDescription(Length, Assignment, indexGenerator);
            }
        }

        public int[] GetSequence()
        {
            var result = new int[main.Max * sub.Max];
            int index = 0;

            do
            {
                int[] iArray = main.Next();
                do
                {
                    int[] jArray = sub.Next();
                    if(jArray.Length != 1)
                        throw new InvalidOperationException();
                    int j = jArray[0];

                    foreach (var i in iArray)
                    {
                        int rowIndex = main.Assignment == DimensionAssignment.Row ? i : j;
                        int columnIndex = main.Assignment == DimensionAssignment.Column ? i : j;

                        int value = rowIndex*columns + columnIndex;
                        result[index] = value + 1;
                        index++;
                    }

                } while (sub.HasNext);
                sub = sub.CreateInverseDescription();
            } while (main.HasNext);

            return result;
        }

        private enum DimensionAssignment { Row, Column }
        private enum TraverseMode { Serpentine, Circular }

        private class DimensionDescription
        {
            private readonly IndexGenerator indexGenerator;

            public int Max { get; private set; }

            public DimensionAssignment Assignment { get; private set; }

            public bool HasNext
            {
                get { return indexGenerator.HasNext; }
            }

            public int[] Next()
            {
                return indexGenerator.NextValues();
            }

            internal DimensionDescription(int max, DimensionAssignment assignment, IndexGenerator indexGenerator)
            {
                Max = max;
                Assignment = assignment;
                this.indexGenerator = indexGenerator;
            }

            public static DimensionDescription CreateIncrementDescription(int max, DimensionAssignment assignment, TraverseMode mode)
            {
                return CreateIncrementDescription(max, assignment, mode, 1, default(Order));
            }

            public static DimensionDescription CreateIncrementDescription(int max, DimensionAssignment assignment, TraverseMode mode, int breadth, Order startSubOrder)
            {
                switch (mode)
                {
                    case TraverseMode.Serpentine:
                        return CreateIncrementDescriptionSerpentine(max, assignment, breadth, startSubOrder);
                    case TraverseMode.Circular:
                        return CreateIncrementDescriptionCircular(max, assignment);
                    default:
                        throw new ArgumentException();
                }
            }

            private static DimensionDescription CreateIncrementDescriptionSerpentine(int max, DimensionAssignment assignment, int breadth, Order startSubOrder)
            {
                var generator = new SequenceIndexGenerator(max, 0, breadth, startSubOrder);
                return new DimensionDescription(max, assignment, generator);
            }

            private static DimensionDescription CreateIncrementDescriptionCircular(int max, DimensionAssignment assignment)
            {
                var generator = new CircularIndexGenerator(max, 0);
                return new DimensionDescription(max, assignment, generator);
            }
            
            public static DimensionDescription CreateDecrementDescription(int max, DimensionAssignment assignment, TraverseMode mode)
            {
                return CreateDecrementDescription(max, assignment, mode, 1, default(Order));
            }

            public static DimensionDescription CreateDecrementDescription(int max, DimensionAssignment assignment, TraverseMode mode, int breadth, Order startSubOrder)
            {
                switch (mode)
                {
                    case TraverseMode.Serpentine:
                        return CreateDecrementDescriptionSerpentine(max, assignment, breadth, startSubOrder);
                    case TraverseMode.Circular:
                        return CreateDecrementDescriptionCircular(max, assignment);
                    default:
                        throw new ArgumentException();
                }
            }

            private static DimensionDescription CreateDecrementDescriptionSerpentine(int max, DimensionAssignment assignment, int breadth, Order startSubOrder)
            {
                var generator = new SequenceIndexGenerator(max, max - 1, breadth, startSubOrder);
                return new DimensionDescription(max, assignment, generator);
            }

            private static DimensionDescription CreateDecrementDescriptionCircular(int max, DimensionAssignment assignment)
            {
                var generator = new CircularIndexGenerator(max, max - 1);
                return new DimensionDescription(max, assignment, generator);
            }

            public DimensionDescription CreateInverseDescription()
            {
                var inverseGenerator = indexGenerator.CreateInverse();
                return new DimensionDescription(Max, Assignment, inverseGenerator);
            }
        }
    }
}
