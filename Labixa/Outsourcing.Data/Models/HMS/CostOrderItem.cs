namespace Outsourcing.Data.Models.HMS
{
   
    public class CostOrderItem : BaseEntity
    {
        public int CostId { get; set; }
        public int CostOrderId { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string Noted { get; set; }
        public string Description { get; set; }
        public virtual Cost Cost { get; set; }
        public virtual CostOrder CostOrder { get; set; }
    }
}
