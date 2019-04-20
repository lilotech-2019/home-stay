using Outsourcing.Data.Models.HMS;

namespace Outsourcing.Data.Models
{
    public class Asset
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public string Quantity { get; set; }

        public virtual Room Room { get; set; }
    }
}