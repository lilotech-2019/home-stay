namespace Outsourcing.Data.Models.HMS
{
    public class RoomOrderItem : BaseEntity
    {
        public int RoomOrderId { get; set; }
        public int HMSProductId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public string Noted { get; set; }
        public string Description { get; set; }


        public virtual HMSProduct HMSProduct { get; set; }
        public virtual RoomOrder RoomOrder { get; set; }

    }
}
