using Hotel1.Models;
using Hotel1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel1.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
   

    private readonly ILogger<StaffController> _logger;
     
     private readonly IStaffRepository _staff;

    private readonly IScheduleRepository _schedule;
    private readonly IRoomRepository _room;


    public StaffController(ILogger<StaffController> logger,
    IStaffRepository staff, IScheduleRepository schedule, IRoomRepository _room)
    {
        _logger = logger;
        _staff = staff;
        _schedule = schedule;
        this._room = _room;
    }
 
      
[HttpGet]
    public async Task<ActionResult<List<StaffDTO>>> GetList()
    {
        var res = await _staff.GetList();

        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var res = await _staff.GetById(id);

        if (res == null)
            return NotFound();

        var dto = res.asDto;
       


        dto.Rooms = (await _room.GetListByStaffId(id)).asDto;

        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] StaffCreateDTO Data)
    {
        var toCreateStaff = new Staff
        {

            Mobile = Data.Mobile,
            Name = Data.Name.Trim(),
            DateOfBirth = Data.DateOfBirth.UtcDateTime
        };

        var res = await _staff.Create(toCreateStaff);

        return StatusCode(StatusCodes.Status201Created, res.asDto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] StaffCreateDTO Data)
    {
        var existingGuest = await _staff.GetById(id);

        if (existingGuest == null)
            return NotFound();

        var toUpdateGuest = existingGuest with
        {

            Mobile = Data.Mobile,
            Name = Data.Name.Trim(),
        };

        var didUpdate = await _staff.Update(toUpdateGuest);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

}