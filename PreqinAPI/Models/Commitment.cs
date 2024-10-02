namespace PreqinAPI.Models
{
    public class Commitment
    {
        public int Id { get; set; }
        public int InvestorId { get; set; }
        public int AssetClassId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        public Investor Investor { get; set; }
        public AssetClass AssetClass { get; set; }
    }
}
