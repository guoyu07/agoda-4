
using System;

namespace AgodaDrawings
{
    /// <summary>
    /// Base class for shapes
    /// </summary>
    public abstract class BaseShape
    {
        protected const string Def_Drawing_Item = "+ ";
        public BaseShape(IOutput output, int number)
        {
            if (number < 1)
                throw new ArgumentException("Argument should be positive number only");

            _output = output;
            _number = number;
        }

        public abstract void Print();

        protected IOutput _output;
        protected int _number;
    }
}
