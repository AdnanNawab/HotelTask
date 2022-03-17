using Hotel1.DTOs;

namespace Hotel1.Models;

public enum Gender
{
    Male = 1,
    Female = 2,
}

public record Guest
{
    public int Id { get; set; }

    public string Name { get; set; }
 
    public string Email { get; set; }
     
     public long Mobile { get; set; }

     public string Address { get; set; }

     public DateTimeOffset DateOfBirth { get; set; }

     public Gender Gender { get; set; }
      
      public GuestDTO asDto => new GuestDTO
    {

          Email = Email,
          Id = Id,
          Mobile = Mobile,
        Name = Name,
          
    };



}