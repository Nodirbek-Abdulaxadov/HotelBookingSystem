using BLL.DTOs.RoomTypes;
using BLL.Interfaces;
using Entities;
using Datalayer.Interfaces;

namespace BLL.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public RoomTypeService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        public async Task<RoomType> AddAsync(AddRoomTypeDto room)
        {
            var model = (RoomType)room;
            foreach (var file in room.ImageFiles)
            {
                model.Images.Add(await _imageService.SaveImageAsync(file));
                await _unitOfWork.SaveAsync();
            }

            model = await _unitOfWork.RoomTypes.AddAsync(model);
            await _unitOfWork.SaveAsync();
            
            return model;
        }

        public async Task<IEnumerable<ViewRoomTypeDto>> GetAllAsync(string language)
        {
            var list = await _unitOfWork.RoomTypes.GetAllAsync();
            return list.Select(i => i.ConvertTo(language));
        }

        public async Task<IEnumerable<RoomType>> GetAllAsync()
            => await _unitOfWork.RoomTypes.GetAllAsync();


        public async Task<ViewRoomTypeDto> GetByIdAsync(int roomId, string language)
        {
            var room = await _unitOfWork.RoomTypes.GetByIdAsync(roomId);
            return room.ConvertTo(language);
        }

        public async Task<RoomType> GetByIdAsync(int roomId)
            => await _unitOfWork.RoomTypes.GetByIdAsync(roomId);

        
        public async Task RemoveAsync(int roomId)
        {
            RoomType room = await _unitOfWork.RoomTypes.GetByIdAsync(roomId);
            foreach (var path in room.Images)
            {
                await _imageService.RemoveImageAsync(path);
                await _unitOfWork.SaveAsync();
            }
            await _unitOfWork.RoomTypes.RemoveAsync(room);
            await _unitOfWork.SaveAsync();
        }

        public async Task<RoomType> UpdateAsync(UpdateRoomTypeDto room)
        {
            if (room.NewImageFiles.Any())
            {
                foreach (var path in room.Images)
                {
                    await _imageService.RemoveImageAsync(path);
                    await _unitOfWork.SaveAsync();
                }
                foreach (var file in room.NewImageFiles)
                {
                    room.Images.Add(await _imageService.SaveImageAsync(file));
                    await _unitOfWork.SaveAsync();
                }
            }

            var model = await _unitOfWork.RoomTypes.UpdateAsync((RoomType)room);
            await _unitOfWork.SaveAsync();

            return model;
        }
    }

    internal static class Extend
    {
        public static ViewRoomTypeDto ConvertTo(this RoomType room, string language)
        {
            var description = language switch
            {
                "uz" => room.DescriptionUz,
                "en" => room.DescriptionEn,
                "ru" => room.DescriptionRu,
                _ => ""
            };

            return new ViewRoomTypeDto()
            {
                Id = room.Id,
                Name = room.Name,
                Capacity = room.Capacity,
                Description = description,
                Price = room.Price,
                Images = room.Images
            };
        }
    }

}