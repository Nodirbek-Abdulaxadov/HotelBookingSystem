namespace BLL.DTOs.Orders;

public class OrderDto
{
    public int Id { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int NumberOfAdults { get; set; }
    public int NumberOfChildren { get; set; }
    public decimal TotalPrice { get; set; }
    public string BookedDate { get; set; }
    public string Status { get; set; }
    public string RoomType { get; set; }
}
