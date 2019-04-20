using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{

    public class CostOrder : BaseEntity
    {
        public CostOrder()
        {
            DateCreated = DateTime.Now;
        }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        /// <summary>
        /// the final payment to customer pay 
        /// </summary>
        public double? TotalPayment_CheckOut { get; set; }
        /// <summary>
        /// just count total of booking room, not include sum of service and other
        /// </summary>
        public double? TotalPaymentRoom_DraftCheckIn { get; set; }
        public int? Status { get; set; }
        public int? ShipmentId { get; set; }
        public Double? ShipmentFee { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Deadline { get; set; }
        
        public string Note { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public virtual ICollection<CostOrderItem> CostOrderItems { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}
