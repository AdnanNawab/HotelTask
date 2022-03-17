using Dapper;
using Hotel1.Models;
using Hotel1.Utilities;

namespace Hotel1.Repositories;

public interface IStaffRepository
{
    Task<Staff> Create(Staff Item);
    Task<bool> Update(Staff Item);

    Task<List<Staff>> GetList();
    Task<Staff> GetById(int Id);
    Task<List<Staff>> GetStaffByRoomId(int StaffId);


}

public class StaffRepository : BaseRepository, IStaffRepository
{
    public StaffRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Staff> Create(Staff Item)
    {
        var query = $@"INSERT INTO {TableNames.staff} 
        (name, date_of_birth,gender, mobile, ) 
        VALUES (@Name, @DateOfBirth, @Gender, @Mobile,) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Staff>(query, Item);
    }



    public async Task<Staff> GetById(int Id)
    {
        var query = $@"SELECT * FROM {TableNames.staff} WHERE id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Staff>(query, new { Id });
    }

    public async Task<List<Staff>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.staff}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Staff>(query)).AsList();
    }

    public async Task<List<Staff>> GetStaffByRoomId(int StaffId)
    {
        var query = $@"SELECT r.* FROM {TableNames.staff} s 
        LEFT JOIN {TableNames.room} r ON r.staff_id = s.id 
        WHERE r.staff_id = @StaffId";
        using (var con = NewConnection)
        
            return (await con.QueryAsync<Staff>(query, new { StaffId })).AsList();
    }

    public async Task<bool> Update(Staff Item)
    {
        var query = $@"UPDATE {TableNames.staff} 
        SET name = @Name, mobile = @Mobile 
         WHERE id = @Id";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }


}