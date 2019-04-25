using System.Collections.Generic;

namespace Outsourcing.Data.Models
{
    public class Asset : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<RoomAsset> RoomAssets { get; set; }
    }
}