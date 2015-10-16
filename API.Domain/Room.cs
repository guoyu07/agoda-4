namespace API.Domain
{
    /// <summary>
    /// Anemic room model, in more complex scenariouses
    /// we need to move business logic from services to models
    /// </summary>
    public class Room
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
