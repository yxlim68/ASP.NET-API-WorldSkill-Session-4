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
    public class UserTypesController : ControllerBase
    {
        private readonly DBContext context;

        public UserTypesController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllUserTypes()
        {
            var UserTypes = context.UserTypes.ToList();
            return Ok(UserTypes);
        }

        [HttpGet("userTypeID")]
        public IActionResult GetUserType(long userTypeID)
        {
            var UserType = context.UserTypes.Find(userTypeID);
            return Ok(UserType);
        }

        [HttpPost]
        public IActionResult AddUserType(UserTypeDto UserTypeDto)
        {
            var UserTypeEntity = new UserTypes() { Name = UserTypeDto.Name};

            context.UserTypes.Add(UserTypeEntity);
            context.SaveChanges();

            return Ok(UserTypeEntity);
        }

        [HttpPut("{UserTypeID}")]
        public IActionResult UpdateUserType(int UserTypeID, UserTypeDto UserTypeDto)
        {
            var UserTypeEntity = context.UserTypes.Find(UserTypeID);

            UserTypeEntity.Name = UserTypeDto.Name;

            context.SaveChanges();

            return Ok(UserTypeEntity);
        }

        [HttpDelete]
        public IActionResult DeleteUserTypes(int UserTypeID)
        {
            var UserType = context.UserTypes.Find(UserTypeID);

            if (UserType is null)
            {
                return NotFound();
            }

            context.UserTypes.Remove(UserType);
            context.SaveChanges();

            return Ok(UserType);
        }
    }
}
