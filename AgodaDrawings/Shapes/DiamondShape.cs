using System;
using System.Linq;

namespace AgodaDrawings
{
    /// <summary>
    /// Draw diamond to output
    /// </summary>
    public class DiamondShape : BaseShape
    {
        public DiamondShape(int number, IOutput output) : base(output, number)
        {
            if (number % 2 == 0)
                throw new ArgumentException("Argument should be odd number only");

            if (number > output.BufferWidth / Def_Drawing_Item.Length)
                throw new ArgumentException("Diamond exceeds console window length");

            _sectorLenght = (number + 1) / 2;
            _output = output;
        }

        public override void Print()
        {
            for (int i = 1; i <= _number; i++)
                WriteLine(i);
        }

        private void WriteLine(int currentPosition)
        {
            var offset = Math.Abs(_sectorLenght - currentPosition);

            _output.Write(new string(' ', offset * Def_Drawing_Item.Length));
            _output.Write(string.Concat(Enumerable.Repeat(Def_Drawing_Item, _number - 2 * offset)));
            _output.Write(Environment.NewLine);
        }

        private int _sectorLenght;
    }
}
