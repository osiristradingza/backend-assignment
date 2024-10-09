namespace OT.Assessment.App.Models.DTOs
{
    public class TopSpenderResponse
    {
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public decimal TotalAmountSpend { get; set; }
    }

}
