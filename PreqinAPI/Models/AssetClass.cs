namespace PreqinAPI.Models
{
    public class AssetClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Commitment> Commitments { get; set; }
    }
}
