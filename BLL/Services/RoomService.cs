using BLL.DTOs.Rooms;
using BLL.Interfaces;
using Entities;
using Datalayer.Interfaces;

namespace BLL.Services
{
    public class RoomService : IRoomService
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

        public async Task<bool> CheckAsync(string type, decimal price)
        {
            var list = await _unitOfWork.Rooms.GetAllAsync();
            return list.Any(r => r.Status == RoomStatus.Empty &&
                                 r.Type == type &&
                                 r.Price == price);
        }

        public async Task<IEnumerable<ViewRoomDto>> GetAllAsync(string language)
        {
            var list = await _unitOfWork.Rooms.GetAllAsync();
            return list.Select(i => i.ConvertTo(language));
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
            => await _unitOfWork.Rooms.GetAllAsync();


        public async Task<ViewRoomDto?> GetByIdAsync(int roomId, string language)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            return room.ConvertTo(language);
        }

        public async Task<Room?> GetByIdAsync(int roomId)
            => await _unitOfWork.Rooms.GetByIdAsync(roomId);


        public async Task<ViewRoomDto?> GetByNumberAsync(int roomNumber, string language)
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            var room = rooms.FirstOrDefault(r => r.Number == roomNumber);
            return room.ConvertTo(language);
        }

        public async Task<Room?> GetByNumberAsync(int roomNumber)
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            var room = rooms.FirstOrDefault(r => r.Number == roomNumber);
            return room;
        }


        public async Task<IEnumerable<ViewRoomDto>> GetEmptyRoomsAsync(string language)
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            return rooms.Where(r => r.Status == RoomStatus.Empty)
                        .Select(i => i.ConvertTo(language));
        }

        public async Task<IEnumerable<Room>> GetEmptyRoomsAsync()
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync();
            return rooms.Where(r => r.Status == RoomStatus.Empty);
        }

        public async Task RemoveAsync(int roomId)
        {
            Room? room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            await _imageService.RemoveImageAsync(room.ImagePath);
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

    internal static class Extend
    {
        public static ViewRoomDto ConvertTo(this Room room, string language)
        {
            var description = language switch
            {
                "uz" => room.DescriptionUz,
                "en" => room.DescriptionEn,
                "ru" => room.DescriptionRu,
                _ => ""
            };

            return new ViewRoomDto()
            {
                Id = room.Id,
                Type = room.Type,
                Capacity = room.Capacity,
                Description = description,
                Status = room.Status,
                Price = room.Price,
                Number = room.Number,
                ImagePath = room.ImagePath
            };
        }
    }

}