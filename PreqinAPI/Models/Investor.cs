namespace PreqinAPI.Models
{
    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Country { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdated { get; set; }

        public ICollection<Commitment> Commitments { get; set; }
    }
}
