using System;

namespace AgodaDrawings
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseShape butterflyShape = new ButterflyShape(4, new Output());
            butterflyShape.Print();
            Console.WriteLine();
            BaseShape diamondShape = new DiamondShape(5, new Output());
            diamondShape.Print();
            Console.ReadLine();
        }
    }
}
