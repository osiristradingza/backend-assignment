namespace OT.Assessment.App.Models.DTOs
{
    public class CasinoWagersResponse
    {
        public Guid wagerId { get; set; }
        public string gameName { get; set; }
        public string provider { get; set; }
        public decimal amount { get; set; }
        public DateTime createdDateTime { get; set; }
    }

}

