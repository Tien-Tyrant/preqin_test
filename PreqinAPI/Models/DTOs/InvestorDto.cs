namespace PreqinAPI.DTOs
{
    public class InvestorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DateAdded { get; set; }
        public decimal TotalCommitments { get; set; }
    }
}
