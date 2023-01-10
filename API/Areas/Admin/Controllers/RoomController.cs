using BLL.DTOs.Rooms;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _typeService;

        public RoomController(IRoomService roomService,
                              IRoomTypeService typeService)
        {
            _roomService = roomService;
            _typeService = typeService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _roomService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var list = await _typeService.GetAllAsync();
            AddRoomDto roomDto = new();
            roomDto.RoomTypes.AddRange(list.ToList());

            return View(roomDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomDto room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.AddAsync(room);
                return RedirectToAction("index");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _roomService.GetByIdAsync(id);
            var types = await _typeService.GetAllAsync();
            UpdateRoomDto roomDto = new()
            {
                Id = model.Id,
                Number = model.Number,
                Status = model.Status,
                RoomTypeId = model.RoomType.Id,
                RoomTypes = types.ToList()
            };

            if (model != null)
            {
                return View(roomDto);
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoomDto room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.UpdateAsync(room);
                return RedirectToAction("index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _roomService.RemoveAsync(id);
            
            return RedirectToAction("index");
        }
    }
}
