namespace Outsourcing.Data.Models
{
    public class RoomAsset : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public string Quantity { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }

        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }
    }
}