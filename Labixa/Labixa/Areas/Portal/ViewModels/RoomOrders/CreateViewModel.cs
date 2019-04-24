using Outsourcing.Data.Models;

namespace Labixa.Areas.Portal.ViewModels.RoomOrders
{
    public class CreateViewModel
    {
        public Customer Customer { get; set; }
        public Outsourcing.Data.Models.HMS.RoomOrder RoomOrders { get; set; }
    }
}