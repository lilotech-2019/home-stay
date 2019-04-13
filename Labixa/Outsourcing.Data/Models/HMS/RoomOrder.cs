using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class RoomOrder : BaseEntity
    {
        public RoomOrder()
        {
            DateCreated = DateTime.Now;
            ShipmentId = 0;
        }

        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public int CustomeNumber { get; set; }

        /// <summary>
        /// the final payment to customer pay 
        /// </summary>
        public double? TotalPayment_CheckOut { get; set; }

        /// <summary>
        /// just count total of booking room, not include sum of service and other
        /// </summary>
        public double? TotalPaymentRoom_DraftCheckIn { get; set; }

        public RoomOrderStatus Status { get; set; }
        public int? ShipmentId { get; set; }
        public double? ShipmentFee { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CheckIn { get; set; }
        public bool Book_Motobike { get; set; }
        public bool BookCar { get; set; }
        public bool Book_Tour { get; set; }
        public bool Book_BBQService { get; set; }
        public bool Book_Gift { get; set; }
        public bool Book_Laundry { get; set; }
        public bool Book_FlightTicket { get; set; }
        public bool Book_Visa { get; set; }
        public bool Book_Taxi { get; set; }
        public bool Book_SuggestionTour { get; set; }
        public bool Book_RegisterResidence { get; set; }
        public string OtherBookService { get; set; }
        public string Total_DescriptionBook_Noted { get; set; }
        public double TotalBookPrice { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoomOrderItem> RoomOrderItems { get; set; }

        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Rooms Room { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }

    public enum RoomOrderStatus
    {
        New = 0,
        Processed = 1,
        CheckIn = 3,
        CheckOut = 4
    }
}