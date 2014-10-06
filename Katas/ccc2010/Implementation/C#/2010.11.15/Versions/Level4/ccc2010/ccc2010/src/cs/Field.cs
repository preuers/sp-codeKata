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

            if (startRow == 1)
                main = DimensionDescription.CreateIncrementDescription(rows, DimensionAssignment.Row, TraverseMode.Serpentine);
            else if(startRow == rows)
                main = DimensionDescription.CreateDecrementDescription(rows, DimensionAssignment.Row, TraverseMode.Serpentine);
            else
                throw new ArgumentException();

            if (startColumn == 1)
                sub = DimensionDescription.CreateIncrementDescription(columns, DimensionAssignment.Column, TraverseMode.Serpentine);
            else if (startColumn == columns)
                sub = DimensionDescription.CreateDecrementDescription(columns, DimensionAssignment.Column, TraverseMode.Serpentine);
            else
                throw new ArgumentException();
        }

        public Field(int rows, int columns, int startRow, int startColumn, char startDirection)
            : this(rows, columns, startRow, startColumn, startDirection, 'S')
        {
        }

        public Field(int rows, int columns, int startRow, int startColumn, char startDirection, char mode)
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

            const TraverseMode subMode = TraverseMode.Serpentine;

            if(startRow == 1 && startColumn == 1)
            {
                switch (startDirection)
                {
                    case 'O':
                        main = DimensionDescription.CreateIncrementDescription(rows, DimensionAssignment.Row, mainMode);
                        sub = DimensionDescription.CreateIncrementDescription(columns, DimensionAssignment.Column, subMode);
                        return;
                    case 'S':
                        main = DimensionDescription.CreateIncrementDescription(columns, DimensionAssignment.Column, mainMode);
                        sub = DimensionDescription.CreateIncrementDescription(rows, DimensionAssignment.Row, subMode);
                        return;
                    default:
                        throw new ArgumentException();
                }
            } else if(startRow == 1 && startColumn == columns)
            {
                switch (startDirection)
                {
                    case 'W':
                        main = DimensionDescription.CreateIncrementDescription(rows, DimensionAssignment.Row, mainMode);
                        sub = DimensionDescription.CreateDecrementDescription(columns, DimensionAssignment.Column, subMode);
                        return;
                    case 'S':
                        main = DimensionDescription.CreateDecrementDescription(columns, DimensionAssignment.Column, mainMode);
                        sub = DimensionDescription.CreateIncrementDescription(rows, DimensionAssignment.Row, subMode);
                        return;
                    default:
                        throw new ArgumentException();
                }
            } else if(startRow == rows && startColumn == 1)
            {
                switch (startDirection)
                {
                    case 'O':
                        main = DimensionDescription.CreateDecrementDescription(rows, DimensionAssignment.Row, mainMode);
                        sub = DimensionDescription.CreateIncrementDescription(columns, DimensionAssignment.Column, subMode);
                        return;
                    case 'N':
                        main = DimensionDescription.CreateIncrementDescription(columns, DimensionAssignment.Column, mainMode);
                        sub = DimensionDescription.CreateDecrementDescription(rows, DimensionAssignment.Row, subMode);
                        return;
                    default:
                        throw new ArgumentException();
                }
            } else if(startRow == rows && startColumn == columns)
            {
                switch (startDirection)
                {
                    case 'W':
                        main = DimensionDescription.CreateDecrementDescription(rows, DimensionAssignment.Row, mainMode);
                        sub = DimensionDescription.CreateDecrementDescription(columns, DimensionAssignment.Column, subMode);
                        return;
                    case 'N':
                        main = DimensionDescription.CreateDecrementDescription(columns, DimensionAssignment.Column, mainMode);
                        sub = DimensionDescription.CreateDecrementDescription(rows, DimensionAssignment.Row, subMode);
                        return;
                    default:
                        throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public int[] GetSequence()
        {
            var result = new int[main.Max * sub.Max];
            int index = 0;

            do
            {
                int i = main.Next();
                do
                {
                    int j = sub.Next();

                    int rowIndex = main.Assignment == DimensionAssignment.Row ? i : j;
                    int columnIndex = main.Assignment == DimensionAssignment.Column ? i : j;

                    int value = rowIndex*columns + columnIndex;
                    result[index] = value + 1;
                    index++;

                } while (sub.HasNext);
                sub = sub.CreateSwapedDescription();
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

            public int Next()
            {
                return indexGenerator.Next();
            }

            private DimensionDescription(int max, DimensionAssignment assignment, IndexGenerator indexGenerator)
            {
                Max = max;
                Assignment = assignment;
                this.indexGenerator = indexGenerator;
            }

            public static DimensionDescription CreateIncrementDescription(int max, DimensionAssignment assignment, TraverseMode mode)
            {
                switch (mode)
                {
                    case TraverseMode.Serpentine:
                        return CreateIncrementDescriptionSerpentine(max, assignment);
                    case TraverseMode.Circular:
                        return CreateIncrementDescriptionCircular(max, assignment);
                    default:
                        throw new ArgumentException();
                }
            }

            private static DimensionDescription CreateIncrementDescriptionSerpentine(int max, DimensionAssignment assignment)
            {
                var generator = new SequenceIndexGenerator(max, 0);
                return new DimensionDescription(max, assignment, generator);
            }

            private static DimensionDescription CreateIncrementDescriptionCircular(int max, DimensionAssignment assignment)
            {
                var generator = new CircularIndexGenerator(max, 0);
                return new DimensionDescription(max, assignment, generator);
            }
            
            public static DimensionDescription CreateDecrementDescription(int max, DimensionAssignment assignment, TraverseMode mode)
            {
                switch (mode)
                {
                    case TraverseMode.Serpentine:
                        return CreateDecrementDescriptionSerpentine(max, assignment);
                    case TraverseMode.Circular:
                        return CreateDecrementDescriptionCircular(max, assignment);
                    default:
                        throw new ArgumentException();
                }
            }

            private static DimensionDescription CreateDecrementDescriptionSerpentine(int max, DimensionAssignment assignment)
            {
                var generator = new SequenceIndexGenerator(max, max - 1);
                return new DimensionDescription(max, assignment, generator);
            }

            private static DimensionDescription CreateDecrementDescriptionCircular(int max, DimensionAssignment assignment)
            {
                var generator = new CircularIndexGenerator(max, max - 1);
                return new DimensionDescription(max, assignment, generator);
            }

            public DimensionDescription CreateSwapedDescription()
            {
                var swapedGenerator = indexGenerator.CreateSwaped();
                return new DimensionDescription(Max, Assignment, swapedGenerator);
            }
        }
    }
}
