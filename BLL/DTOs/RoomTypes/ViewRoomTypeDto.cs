namespace BLL.DTOs.RoomTypes
{
    public class ViewRoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
