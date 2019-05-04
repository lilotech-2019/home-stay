namespace Outsourcing.Data.Models
{
    public class RoomAsset : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public float Price { get; set; }

        public string Quantity { get; set; }
        public bool IsAvaiable { get; set; }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
    }
}