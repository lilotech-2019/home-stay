using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class RoomOrder : BaseEntity, IValidatableObject
    {
        public RoomOrder()
        {
            DateCreated = CheckInDate = CheckOutDate = DateTime.Now;
            CheckInTime = CheckOutTime = DateCreated.TimeOfDay;
            ShipmentId = 0;
        }


        /// <summary>
        /// the final payment to customer pay 
        /// </summary>
        public double Total { get; set; }

        public double Draff => TotalDraff();

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH-mm}")]
        public DateTime CheckIn => CheckInDate + CheckInTime;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH-mm}")]
        public DateTime CheckOut => CheckOutDate + CheckOutTime;

        private double TotalDraff()
        {
            if (Room == null)
            {
                return 0.1;
            }
            var calculateDay = (DateTime.Now - CheckIn).TotalDays;

            return Room.Price * calculateDay;
        }

        private double Discount()
        {
            if (Room == null)
            {
                return 0;
            }
            return Room.Price * Room.DiscountPercent * (CheckOut - CheckIn).TotalDays;
        }
        public double Price { get; set; }
        public RoomOrderStatus Status { get; set; }
        public int? ShipmentId { get; set; }
        public double? ShipmentFee { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }

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

        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("RoomId")]
        public virtual Rooms Room { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (CheckIn < CheckOut)
            {
                yield return new ValidationResult("CheckInDate must be greater than CheckOutDate");
            }
        }
    }

    public enum RoomOrderStatus
    {
        New = 0,
        Processed = 1,
        CheckIn = 3,
        CheckOut = 4
    }
}