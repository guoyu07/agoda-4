
using System;
using System.Linq;

namespace AgodaDrawings
{
    /// <summary>
    /// Butterfly shape
    /// </summary>
    public class ButterflyShape : BaseShape
    {
        public ButterflyShape(int number, IOutput output) : base(output, number)
        {
            if (number % 2 == 1)
                 throw new ArgumentException("Argument should be even number only");

            if (number >= Console.BufferWidth / (2 * Def_Drawing_Item.Length))
                throw new ArgumentException("Butterfly exceeds console window length");
        }
        public override void Print()
        {
            for (int i = 1; i < _number * 2; i++)
                WriteLine(i);
        }
        private void WriteLine(int currentPosition)
        {
            var offset = _number - Math.Abs(_number - currentPosition);

            _output.Write(string.Concat(Enumerable.Repeat(Def_Drawing_Item, offset)));

            var spaceCount = (_number - offset) * 2 + 1;
            _output.Write(spaceCount == 1 ? "- " : new string(' ', spaceCount * Def_Drawing_Item.Length));
            _output.Write(string.Concat(Enumerable.Repeat(Def_Drawing_Item, offset)));
            _output.Write(Environment.NewLine);
        }
    }
}
