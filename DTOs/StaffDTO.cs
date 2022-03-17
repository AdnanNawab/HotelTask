using System.Text.Json.Serialization;
using Hotel1.DTOs;

public record StaffDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }


    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

     [JsonPropertyName("rooms")]
    public RoomDTO Rooms { get; set; }

    
     [JsonPropertyName("schedules")]
    public ScheduleDTO Schedules { get; set; }


}


public record StaffCreateDTO
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date_of_birth")]
    public DateTimeOffset DateOfBirth { get; set; }


    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

}