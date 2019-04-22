namespace Outsourcing.Data.Models
{
    public class Asset:BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public string Quantity { get; set; }

        public virtual string RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}