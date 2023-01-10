using BLL.DTOs.RoomTypes;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService _roomService;

        public RoomTypesController(IRoomTypeService roomService)
        {
            _roomService = roomService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _roomService.GetAllAsync();
        //    return Ok(list);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int roomId)
        //{
        //    if (!(await ExistRoom(roomId)))
        //    {
        //        return NotFound("This room could not match!");
        //    }

        //    var model = await _roomService.GetByIdAsync(roomId);
        //    return Ok(model);
        //}

        [HttpGet("{language}")]
        public async Task<IActionResult> GetByLanguage(string language)
        {
            var list = await _roomService.GetAllAsync(language);
            return Ok(list);
        }

        [HttpGet("{language}/{id}")]
        public async Task<IActionResult> GetById(string language, int id)
        {
            if (!(await ExistRoom(id)))
            {
                return NotFound("This room could not match!");
            }

            var model = await _roomService.GetByIdAsync(id, language);
            return Ok(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Add([FromForm] AddRoomTypeDto room)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var model = await _roomService.AddAsync(room);
        //    return Ok(model);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update([FromForm] UpdateRoomTypeDto room)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var model = await _roomService.UpdateAsync(room);
        //    return Ok(model);
        //}

        //[HttpDelete("{roomId}")]
        //public async Task<IActionResult> Delete(int roomId)
        //{
        //    var room = await _roomService.GetByIdAsync(roomId);
        //    if (room == null)
        //    {
        //        return NotFound();
        //    }

        //    await _roomService.RemoveAsync(roomId);
        //    return Ok();
        //}



        private async Task<bool> ExistRoom(int roomId)
        {
            var model = await _roomService.GetByIdAsync(roomId);
            return model != null;
        }
    }
}
