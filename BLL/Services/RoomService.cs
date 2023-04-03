using BLL.DTOs.Rooms;
using BLL.Interfaces;
using Datalayer.Interfaces;
using Entities;

namespace BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AddRoomDto room)
        {
            if (room.RoomTypeId == 0)
            {
                room.RoomTypeId = (await _unitOfWork.RoomTypes.GetAllAsync()).First().Id;
            }
            var rom = (Room)room;
            rom.OrderId = (await _unitOfWork.Orders.GetAllAsync())
                           .First().Id;
            var model = await _unitOfWork.Rooms.AddAsync(rom);
            await _unitOfWork.SaveAsync();
            var type = await _unitOfWork.RoomTypes.GetByIdAsync(room.RoomTypeId);
            model.RoomType = type;
            model = await _unitOfWork.Rooms.UpdateAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> CheckAsync(int roomTypeId)
        {
            var rooms = await _unitOfWork.Rooms.GetAllWithRoomTypesAsync();
            var exist = rooms.Any(r => r.RoomType.Id == roomTypeId);
            return exist;
        }

        public async Task<bool> CheckAsync(string startDate, string endDate, int guestsCount)
        {
            var start = DateOnly.Parse(startDate);
            var end = DateOnly.Parse(endDate);
            if (start < DateOnly.Parse(DateTime.Now.ToShortDateString())
                || start > end)
            {
                return false;
            }

            var roomTypes = await _unitOfWork.RoomTypes.GetAllAsync();
            var rooms = (await _unitOfWork.Rooms.GetAllWithRoomTypesAsync())
                            .Where(r => roomTypes.FirstOrDefault(rt => rt.Id == r.RoomType.Id)
                            .Capacity >= guestsCount)
                            .ToList();

            var exist = rooms.Any(r => r.Status == RoomStatus.Empty);

            var busyRooms = rooms.Where(r => r.Status is RoomStatus.Busy or RoomStatus.Ordered)
                                    .ToList();
            var orders = await _unitOfWork.Orders.GetAllAsync();
            foreach (var room in busyRooms)
            {
                if (orders.Any(o => DateOnly.Parse(o.EndDate) < DateOnly.Parse(startDate) &&
                                    o.OrderStatus is OrderStatus.Active or OrderStatus.Confirmed))
                {
                    return true;
                }
            }

            return exist;
        }

        public async Task<IEnumerable<ViewRoomDto>> GetAllAsync()
        {
            var list = await _unitOfWork.Rooms.GetAllWithRoomTypesAsync();
            var res = list.Select(i => (ViewRoomDto)i);
            return res;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
            => await _unitOfWork.Rooms.GetAllWithRoomTypesAsync();

        public async Task<Room> GetByIdAsync(int RoomId)
        {
            var model = await _unitOfWork.Rooms.GetByIdWithRoomTypesAsync(RoomId);
            return model;
        }

        public async Task<ViewRoomDto> GetByIdForViewAsync(int RoomId)
        {
            var model = await _unitOfWork.Rooms.GetByIdWithRoomTypesAsync(RoomId);
            return (ViewRoomDto)model;
        }

        public async Task RemoveAsync(int RoomId)
        {
            var model = await _unitOfWork.Rooms.GetByIdAsync(RoomId);
            await _unitOfWork.Rooms.RemoveAsync(model);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateRoomDto room)
        {
            var model = await _unitOfWork.Rooms.UpdateAsync((Room)room);
            await _unitOfWork.SaveAsync();
        }
    }
}
