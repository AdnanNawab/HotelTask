using Dapper;
using Hotel1.Models;
using Hotel1.Utilities;

namespace Hotel1.Repositories;

public interface IRoomRepository
{
 Task<bool> Update(Room Item);

 Task<bool> Delete(int Id);
 Task<List<Room>> GetList();
 Task<Room> GetById(int Id);

 Task<List<Room>> GetListByGuestId(int GuestId);

  Task<Room> GetListByStaffId(int StaffId);

  Task<Room> GetScheduleRoomId(int ScheduleId);



}


public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration config) : base(config)
    {

    }

    
    

    public async Task<List<Room>> GetListByGuestId(int GuestId)
    {
        var query = $@"SELECT r.* FROM {TableNames.schedule} s 
        LEFT JOIN {TableNames.room} r ON r.id = s.room_id 
        WHERE s.guest_id = @GuestId";

        

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query, new { GuestId })).AsList();
        
    }

    

    public async Task<Room> GetById(int Id)
    {
        var query = $@"SELECT r.*, s.name AS staff_name FROM {TableNames.room} r
        LEFT JOIN {TableNames.staff} s ON s.id = r.staff_id 
        WHERE r.id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Room>(query, new { Id });
    }

    public async Task<bool> Update(Room Item)
    {
        var query = $@"UPDATE {TableNames.room} 
        SET type = @Type, Size = @Size, Price = @Price
         WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }

    public async Task<bool> Delete(int Id)
    {
        var query = $@"DELETE FROM {TableNames.room} WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { Id }) > 0;
    }

    public async Task<List<Room>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.room}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Room>(query)).AsList();
    }

   

    public async Task<Room> GetListByStaffId(int StaffId)
    {
         var query = $@"SELECT r.* FROM {TableNames.staff} s 
        LEFT JOIN {TableNames.room} r ON r.staff_id = s.id 
        WHERE r.staff_id = @StaffId";
         

        using (var con = NewConnection)
        
            return await con.QueryFirstOrDefaultAsync<Room>(query, new { StaffId });
    }

    public async Task<Room> GetScheduleRoomId(int ScheduleId)
    {
        var query = $@"SELECT r.* FROM {TableNames.schedule} s 
        LEFT JOIN {TableNames.room} r ON r.id = s.room_id 
        WHERE s.id = @ScheduleId";
        using (var con = NewConnection)
        
            return await con.QueryFirstOrDefaultAsync<Room>(query, new { ScheduleId });
    }

  }

   