using s4.Data;
using s4.Models.Entities;
using s4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace s4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBContext context;

        public UsersController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var Users = context.Users.ToList();
            return Ok(Users);
        }

        [HttpGet("UserID")]
        public IActionResult GetUserByID(long UserID)
        {
            var Users = context.Users.Where(u => u.ID == UserID).FirstOrDefault();
            return Ok(Users);
        }

        [HttpGet("userName/password")]
        public IActionResult GetUser([FromQuery]string userName,[FromQuery]string password)
        {
            var User = context.Users.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();
            return Ok(User);
        }

        [HttpPost]
        public IActionResult AddUser([FromQuery] long UserTypeID,[FromBody] UserDto UserDto)
        {
            var UserEntity = new Users() { UserTypeID = UserTypeID,UserName = UserDto.UserName, BirthDate = UserDto.BirthDate, FamilyCount = UserDto.FamilyCount, FullName = UserDto.FullName, Gender = UserDto.Gender, Password = UserDto.Password };

            context.Users.Add(UserEntity);
            context.SaveChanges();

            return Ok(UserEntity);
        }

        [HttpPut("{UserID}")]
        public IActionResult UsersType(long UserID, UserDto UserDto)
        {
            var UserEntity = context.Users.Find(UserID);

            UserEntity.UserName = UserDto.UserName;
            UserEntity.FullName = UserDto.FullName;
            UserEntity.Gender = UserDto.Gender;
            UserEntity.BirthDate = UserDto.BirthDate;
            UserEntity.FamilyCount = UserDto.FamilyCount;
            UserEntity.Password = UserDto.Password;

            context.SaveChanges();

            return Ok(UserEntity);
        }

        [HttpDelete]
        public IActionResult DeleteUsers(long UserID)
        {
            var User = context.Users.Find(UserID);

            if (User is null)
            {
                return NotFound();
            }

            context.Users.Remove(User);
            context.SaveChanges();

            return Ok(User);
        }
    }
}
