using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models
{
    public class RoomAsset : BaseEntity
    {
        public string Name { get; set; }
        public float Price { get; set; }

        public string Quantity { get; set; }
        public bool IsAvaiable { get; set; }

        //[ForeignKey("Room")]
        //public int RoomId { get; set; }
        //public virtual Room Room { get; set; }

        [ForeignKey("Asset")]
        public int AssetId { get; set; }
        public virtual Asset Asset { get; set; }
    }
}