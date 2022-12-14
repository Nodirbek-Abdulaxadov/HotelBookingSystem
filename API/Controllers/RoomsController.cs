﻿using BLL.DTOs.Rooms;
using BLL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
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

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetById(int roomId)
        {
            if (!(await ExistRoom(roomId)))
            {
                return NotFound("This room could not match!");
            }

            var model = await _roomService.GetByIdAsync(roomId);
            return Ok(model);
        }

        [HttpGet("by/{roomNumber}")]
        public async Task<IActionResult> GetByNumber(int roomNumber)
        {
            if (!(await ExistRoomNumber(roomNumber)))
            {
                return NotFound("This room could not match!");
            }

            var model = await _roomService.GetByNumberAsync(roomNumber);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddRoomDto room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await ExistRoomNumber(room.Number))
            {
                return BadRequest("This room number is already exist!");
            }

            var model = await _roomService.AddAsync(room);
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateRoomDto room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await ExistOrNotUnique(room.Number, room.Id))
            {
                return BadRequest("This room number is already exist!");
            }

            var model = await _roomService.UpdateAsync(room);
            return Ok(model);
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Delete(int roomId)
        {
            var room = await _roomService.GetByIdAsync(roomId);
            if (room == null) 
            {
                return NotFound();
            }

            await _roomService.RemoveAsync(roomId);
            return Ok();
        }



        private async Task<bool> ExistRoom(int roomId)
        {
            var model = await _roomService.GetByIdAsync(roomId);
            return model != null;
        }

        private async Task<bool> ExistRoomNumber(int roomNumber)
        {
            var model = await _roomService.GetByNumberAsync(roomNumber);
            return model != null;
        }

        private async Task<bool> ExistOrNotUnique(int roomNumber, int roomId)
        {
            var rooms = await _roomService.GetAllAsync();
            var room = rooms.FirstOrDefault(r => r.Number == roomNumber);
            if (room == null)
            {
                return false;
            }

            return !(room.Number == roomNumber && room.Id == roomId);
        }
    }
}
