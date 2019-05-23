
namespace Labixa.Areas.Portal.ViewModels.Customers
{
    public class CustomerDetailsSubMenuViewModel
    {
        public PagedList.IPagedList<Outsourcing.Data.Models.RoomOrder> RoomOrders { get; set; }
        public Outsourcing.Data.Models.Customer Customer { get; set; }

    }
}