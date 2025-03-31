using CsharpTestApp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace CsharpTestApp.Dtos.Mappers;

public static class UserDtoMapper
{
    


    public static List<UserDto> MapToUserDtos(List<User> users)
    {
        var userDtos = new List<UserDto>();
        foreach(User user in users)
        {
            var userDto = MapToUserDto(user);
            userDtos.Add(userDto);
        };
        return userDtos;
    }
    
    public static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }

}