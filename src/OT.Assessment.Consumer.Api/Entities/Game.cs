namespace OT.Assessment.Consumer.Api.Entities
{
    public class Game
    {
        public Guid GameId { get; set; }
        public Guid ExternalReferenceId { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
