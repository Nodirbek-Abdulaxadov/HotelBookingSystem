using BLL.Interfaces;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _roomService.GetByIdAsync(id);
            return Ok(model);
        }

        [HttpGet("check")]
        public async Task<IActionResult> Check(string startDate, string endDate, int guestsCount)
        {
            try
            {
                var result = await _roomService.CheckAsync(startDate, endDate, guestsCount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
