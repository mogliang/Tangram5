using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangram5
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            var chips = new List<Chip>{new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 1, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 1, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 0, 1, 0, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
                ,new Chip(new int[,] {
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 } })
            };
            var flags = new bool[chips.Count];

            PutChip(board, chips, flags);
            Console.WriteLine($"Found {totalSolution} solutons.");

        }

        static int totalSolution=0;
        static void PutChip(Board board, List<Chip> chips, bool[] flags)
        {
            for(int i = 0; i < flags.Length; i++)
            {
                if (!flags[i])
                {
                    var chip = chips[i];
                    for(int j = 0; j < chip.ChipStates.Count; j++)
                    {
                        ChipPlace chipPlace;
                        var test = board.TryPut(chip.ChipStates[j], out chipPlace);
                        if (test)
                        {
                            flags[i] = true;
                            if (flags.All(f => f))
                            {
                                // 找到
                                board.Print();
                                totalSolution++;
                            }
                            else
                            {
                                PutChip(board, chips, flags);
                            }

                            board.Remove(chipPlace);
                            flags[i] = false;
                        }
                    }
                }
            }
        }
    }

    public class Board
    {
        const int XSize = 5;
        const int YSize = 12;
        int chipMark = 1;
        public Position CurrentTestPosition { set; get; }
        public int[,] Matrix { set; get; }

        public Board()
        {
            CurrentTestPosition = new Position { X = 0, Y = 0 };
            Matrix = new int[XSize, YSize];
        }

        bool MoveToNextPosition()
        {
            while (CurrentTestPosition.X <= XSize - 1 && CurrentTestPosition.Y <= YSize - 1)
            {
                if (CurrentTestPosition.X < XSize - 1)
                {
                    CurrentTestPosition.X += 1;
                }
                else
                {
                    CurrentTestPosition.X = 0;
                    CurrentTestPosition.Y += 1;
                }

                if (CurrentTestPosition.Y >= YSize)
                {
                    return true;
                }
                if ( Matrix[CurrentTestPosition.X, CurrentTestPosition.Y] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Remove(ChipPlace chipPlace)
        {
            for (int y = 0; y < YSize; y++)
            {
                for (int x = 0; x < XSize; x++)
                {
                    if (Matrix[x, y] == chipPlace.Mark)
                    {
                        Matrix[x, y] = 0;
                    }
                }
            }
            CurrentTestPosition = chipPlace.ChipPosition;
            chipMark--;
        }

        public bool TryPut(ChipState chip, out ChipPlace chipPlace)
        {
            chipPlace = null;
            var footPos = chip.Foot;
            var xOffset = CurrentTestPosition.X - footPos.X;
            var yOffset = CurrentTestPosition.Y - footPos.Y;
            bool test = true;
            for (int x = 0; x < Chip.MatrixSize; x++)
                for (int y = 0; y < Chip.MatrixSize; y++)
                {
                    var posx = xOffset + x;
                    var posy = yOffset + y;
                    if (chip.Matrix[x, y] != 0 &&
                        (posx < 0 || posx >= XSize || posy < 0 || posy >= YSize || Matrix[xOffset + x, yOffset + y] != 0))
                    {
                        test = false;
                        break;
                    }
                }

            if (test)
            {
                chipPlace = new ChipPlace
                {
                    ChipPosition = new Position
                    {
                        X = CurrentTestPosition.X,
                        Y = CurrentTestPosition.Y
                    },
                    Mark = chipMark
                };
                for (int x = 0; x < Chip.MatrixSize; x++)
                    for (int y = 0; y < Chip.MatrixSize; y++)
                    {
                        if (chip.Matrix[x, y] != 0)
                        {
                            Matrix[xOffset + x, yOffset + y] = chipMark;
                        }
                    }
                chipMark++;
                MoveToNextPosition();
            }
            return test;
        }

        static ConsoleColor[] colors = new ConsoleColor[]
        {
            ConsoleColor.Blue,
            ConsoleColor.Green,
            ConsoleColor.White,
            ConsoleColor.Yellow,
            ConsoleColor.Red,
            ConsoleColor.Cyan,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkGreen,
            ConsoleColor.Magenta,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkCyan,
        };
        public void Print()
        {
            for (int y = 0; y < YSize; y++)
            {
                for (int x = 0; x < XSize; x++)
                {
                    Console.BackgroundColor = colors[Matrix[x, y] % colors.Length];
                    Console.ForegroundColor = colors[Matrix[x, y] % colors.Length];
                    Console.Write(Matrix[x, y].ToString("00"));
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }
    }

    public class ChipPlace
    {
        public int Mark { set; get; }
        public Position ChipPosition { set; get; }
    }

    public class Chip
    {
        public const int MatrixSize = 5;
        public Chip(int[,] chipMatrix)
        {
            if (chipMatrix.GetLength(0) != MatrixSize || chipMatrix.GetLength(1) != MatrixSize)
            {
                throw new Exception($"not {MatrixSize}x{MatrixSize} matrix");
            }
            this._matrix = chipMatrix;

            ChipStates = new List<ChipState>();
            for (int j = 0; j < 2; j++)
            {
                YMirror();
                for (int k = 0; k < 4; k++)
                {
                    Rotate90();
                    Normalize();
                    var newState = new ChipState(_matrix, GetFoot());
                    if (!ChipStates.Exists(c => c.AreEqual(newState)))
                    {
                        ChipStates.Add(newState);
                        newState.Print();
                    }
                }
            }
        }

        Position GetFoot()
        {
            for (int y = 0; y < MatrixSize; y++)
                for (int x = 0; x < MatrixSize; x++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        return new Position { X = x, Y = y };
                    }
                }
            return null;
        }

        void Normalize()
        {
            var newMatrix = new int[5, 5];
            int minx = GetMinX();
            int miny = GetMinY();
            for (int x = 0; x < MatrixSize; x++)
                for (int y = 0; y < MatrixSize; y++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        newMatrix[x - minx, y - miny] = _matrix[x, y];
                    }
                }

            _matrix = newMatrix;
        }

        int GetMinX()
        {
            for (int x = 0; x < MatrixSize; x++)
                for (int y = 0; y < MatrixSize; y++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        return x;
                    }
                }
            return 0;
        }

        int GetMinY()
        {
            for (int y = 0; y < MatrixSize; y++)
                for (int x = 0; x < MatrixSize; x++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        return y;
                    }
                }
            return 0;
        }

        void Rotate90()
        {
            var newMatrix = new int[5, 5];
            for (int x = 0; x < MatrixSize; x++)
                for (int y = 0; y < MatrixSize; y++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        newMatrix[y, MatrixSize - 1 - x] = _matrix[x, y];
                    }
                }
            _matrix = newMatrix;
        }

        void YMirror()
        {
            var newMatrix = new int[5, 5];
            for (int x = 0; x < MatrixSize; x++)
                for (int y = 0; y < MatrixSize; y++)
                {
                    if (_matrix[x, y] != 0)
                    {
                        newMatrix[MatrixSize - 1 - x, y] = _matrix[x, y];
                    }
                }
            _matrix = newMatrix;
        }
        public List<ChipState> ChipStates { get; }
        int[,] _matrix;
    }

    public class ChipState
    {
        public ChipState(int[,] matrix, Position foot)
        {
            Matrix = matrix;
            Foot = foot;
        }

        public void Print()
        {
            for (int y = 0; y < Chip.MatrixSize; y++)
            {
                for (int x = 0; x < Chip.MatrixSize; x++)
                {
                    if (Matrix[x, y] != 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("00");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("00");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int[,] Matrix { get; }
        public Position Foot { get; }

        public bool AreEqual(ChipState b)
        {
            for(int x=0;x< Chip.MatrixSize;x++)
                for(int y = 0; y < Chip.MatrixSize; y++)
                {
                    if (this.Matrix[x, y] != b.Matrix[x, y])
                    {
                        return false;
                    }
                }
            return true;
        }
    }

    public class Position
    {
        public int X { set; get; }
        public int Y { set; get; }
    }
}
