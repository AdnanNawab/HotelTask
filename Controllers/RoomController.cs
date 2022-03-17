using Hotel1.DTOs;
using Hotel1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel1.Controllers;

[ApiController]
[Route("api/room")]
public class RoomController : ControllerBase
{
   

    private readonly ILogger<RoomController> _logger;
     
     private readonly IRoomRepository _room;
     private readonly IStaffRepository _staff;
    public RoomController(ILogger<RoomController> logger, IRoomRepository room,IStaffRepository staff)
    {
        _logger = logger;
        _room = room;
        _staff = staff;
    }
 
    [HttpGet]
     public async Task<ActionResult<List<RoomDTO>>> GetList()
     {
        var res = await _room.GetList();

        return Ok(res.Select(x => x.asDto));
     }

    [HttpGet("{id}")]
    
     public async Task<ActionResult> GetById([FromRoute] int id)
     {
        var res = await _room.GetById(id);

        if (res is null)
            return NotFound();

              var dto = res.asDto;
             dto.Staff = (await _staff.GetStaffByRoomId(id)).Select(x => x.asDto).ToList();

        return Ok(res.asDto);
     }

    
    

     [HttpPut("{id}")]    

     public async Task<ActionResult> Update([FromRoute] int id, [FromBody] RoomCreateDTO Data)
     {
         var existingRoom = await _room.GetById(id);

        if (existingRoom == null)
            return NotFound();

        var toUpdateRoom = existingRoom with
        {
        
            StaffName = Data.StaffName?.Trim(),

        };

        var didUpdate = await _room.Update(toUpdateRoom);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }


    [HttpDelete("{id}")]
     public async Task<ActionResult> Delete([FromRoute] int id)
     {
       var existing = await _room.GetById(id);
        if (existing is null)
            return NotFound("No schedule found with schedule id"); 

            var didDelete = await _room.Delete(id);

            return NoContent();
    
    }
    

   
}
