using Entities;

namespace BLL.DTOs.Orders
{
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
