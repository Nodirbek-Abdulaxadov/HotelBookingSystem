using BLL.DTOs.Rooms;
using BLL.Interfaces;
using Entities;
using Datalayer.Interfaces;

namespace BLL.Services
{
    internal class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public RoomService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task<Room> AddAsync(AddRoomDto room)
        {
            var model = (Room)room;
            model.ImagePath = await _imageService.SaveImageAsync(room.ImageFile);
            model = await _unitOfWork.Rooms.AddAsync(model);
            await _unitOfWork.SaveAsync();

            return model;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
            => await _unitOfWork.Rooms.GetAllAsync();

        public async Task<IEnumerable<Room>> GetEmptyRoomsAsync()
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            return rooms.Where(r => r.Status == RoomStatus.Empty);
        }

        public async Task RemoveAsync(int roomId)
        {
            Room? room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            await _unitOfWork.Rooms.RemoveAsync(room);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Room> UpdateAsync(UpdateRoomDto room)
        {
            if (room.ImageFile != null)
            {
                await _imageService.RemoveImageAsync(room.ImagePath);
                room.ImagePath = await _imageService.SaveImageAsync(room.ImageFile);
            }

            var model = await _unitOfWork.Rooms.UpdateAsync((Room)room);
            await _unitOfWork.SaveAsync();

            return model;
        }
    }
}
