namespace AgodaDrawings
{
    /// <summary>
    /// Used to decouple from Console in order to unit test
    /// </summary>
    public interface IOutput
    {
        void WriteLine(string line);
        void Write(string content);
        int BufferWidth { get; }
    }
}
