using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _roomService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("empty")]
        public async Task<IActionResult> GetEmptRooms()
        {
            var list = await _roomService.GetEmptyRoomsAsync();
            return Ok(list);
        }
    }
}
