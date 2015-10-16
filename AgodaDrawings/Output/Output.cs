using System;

namespace AgodaDrawings
{
    /// <summary>
    /// Console implementation of output
    /// </summary>
    public class Output : IOutput
    {
        public int BufferWidth
        {
            get
            {
                return Console.BufferWidth;
            }
        }

        public void Write(string content)
        {
            Console.Write(content);
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
