using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class RoomOrder : BaseEntity
    {
        public RoomOrder()
        {
            DateCreated =  DateTime.Now;
            CheckIn = DateTime.Now;
            CheckOut = CheckIn.AddDays(1);
            AmountOfPeople = 1;
        }


        /// <summary>
        /// the final payment to customer pay 
        /// </summary>
        public double Total { get; set; }

        public double Price { get; set; }
        public RoomOrderStatus OrderStatus { get; set; }


        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan CheckOutTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan CheckInTime { get; set; }

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
        public int AmountOfPeople { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoomOrderItem> RoomOrderItems { get; set; }

        public int RoomId { get; set; }
        public int CustomerId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }

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