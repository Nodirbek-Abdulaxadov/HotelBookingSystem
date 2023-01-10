using BLL.DTOs.RoomTypes;
using BLL.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace API.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomTypesController : Controller
    {
        private readonly IRoomTypeService _typeService;

        public RoomTypesController(IRoomTypeService typeService)
        {
            _typeService = typeService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _typeService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRoomTypeDto model)
        {
            if (ModelState.IsValid)
            {
                var res = await _typeService.AddAsync(model);
                if (res != null)
                    return RedirectToAction("index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _typeService.GetByIdAsync(id);
            if (model != null)
            {
                await _typeService.RemoveAsync(id);
            }

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _typeService.GetByIdAsync(id);
            if (model != null)
            {
                UpdateRoomTypeDto dto = new()
                {
                    Id = id,
                    Name = model.Name,
                    Capacity = model.Capacity,
                    Price = model.Price,
                    Images = model.Images,
                    DescriptionEn = model.DescriptionEn,
                    DescriptionRu = model.DescriptionRu,
                    DescriptionUz = model.DescriptionUz
                };

                return View(dto);
            }

            return RedirectToAction("index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRoomTypeDto model)
        {
            if (ModelState.IsValid)
            {
                await _typeService.UpdateAsync(model);
                return RedirectToAction("index");
            }

            return View(model);
        }
    }
}
