using System;
using System.Collections.Generic;

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
            var result = new List<int>();

            do
            {
                int?[] iArray = main.Next();
                do
                {
                    int?[] jArray = sub.Next();
                    if(jArray.Length != 1 || !jArray[0].HasValue)
                        throw new Exception();
                    int j = jArray[0].Value;

                    foreach (var iOption in iArray)
                    {
                        int value = 0;
                        if (iOption.HasValue)
                        {
                            int i = iOption.Value;
                            int rowIndex = main.Assignment == DimensionAssignment.Row ? i : j;
                            int columnIndex = main.Assignment == DimensionAssignment.Column ? i : j;

                            value = rowIndex*columns + columnIndex + 1;
                        }
                        result.Add(value);
                    }

                } while (sub.HasNext);
                sub = sub.CreateInverseDescription();
            } while (main.HasNext);

            return result.ToArray();
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

            public int?[] Next()
            {
                return indexGenerator.Next();
            }

            internal DimensionDescription(int max, DimensionAssignment assignment, IndexGenerator indexGenerator)
            {
                Max = max;
                Assignment = assignment;
                this.indexGenerator = indexGenerator;
            }

            public DimensionDescription CreateInverseDescription()
            {
                var inverseGenerator = indexGenerator.CreateInverse();
                return new DimensionDescription(Max, Assignment, inverseGenerator);
            }
        }
    }
}
