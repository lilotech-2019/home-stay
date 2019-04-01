namespace Outsourcing.Data.Models.HMS
{
  
    public class RoomUtilityRoomMapping : BaseEntity
    {
        public RoomUtilityRoomMapping()
        {
            Value = "Default";
            IsRequired = false;
            DisplayOrder = 0;
        }

        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public int ProductId { get; set; }
        public int ProductAttributeId { get; set; }
        public virtual Room Room { get; set; }
        public virtual RoomUtility RoomUtility { get; set; }

    }
}
