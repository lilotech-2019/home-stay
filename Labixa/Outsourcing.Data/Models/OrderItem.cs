namespace Outsourcing.Data.Models
{
    public  class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; } 

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }

    }
}
