using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PreqinAPI.Models
{
    public class AssetClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Commitment> Commitments { get; set; }
    }
}
