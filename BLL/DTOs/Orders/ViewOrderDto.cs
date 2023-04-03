using Entities;

namespace BLL.DTOs.Orders
{
    public class OrdersViewModel
    {
        public OrdersViewModel()
        {
            Orders = new List<ViewOrderDto>();
        }
        public IEnumerable<ViewOrderDto> Orders { get; set; }
        public string SearchText { get; set; } = string.Empty;
    }

    public class ViewOrderDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public RoomType RoomType { get; set; }
    }
}
