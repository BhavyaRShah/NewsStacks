using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsStacks.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using Microsoft.AspNetCore.Authorization;

namespace NewsStacks.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserdetailController : ControllerBase
    {
        private readonly IUserDetailService _usersService;

        public UserdetailController(IUserDetailService userService)
        {
            _usersService = userService;
        }

        [HttpGet]
        [Route("{userId}")]
        public Task<Userdetail> GetById(int userId)
        {
            var result = _usersService.GetUserById(userId: userId).Result;
            return Task.FromResult(result);
        }

        [HttpGet]
        [Route("")]
        public Task<List<Userdetail>> GetUsers(string search = "")
        {
            var result = _usersService.GetUsers(search: search).Result;
            return Task.FromResult(result);
        }

        [HttpPost]
        [Route("")]
        public Task<IActionResult> PostCreate([FromBody] Userdetail user)
        {
            var result =  _usersService.Create(user: user).Result;
            return Task.FromResult(result);
        }

        [HttpPut]
        [Route("{userId}")]
        public Task<dynamic> UpdateUser([FromBody] Userdetail user)
        {
            var result = _usersService.Update(user: user).Result;
            return Task.FromResult(result);
        }

        [HttpDelete]
        [Route("{userId}")]
        public Task<int> DeleteUser([FromBody] DeleteRequest objUser)
        {
            var result =  _usersService.Delete(objUser: objUser).Result;
            return Task.FromResult(result);
        }

        [HttpGet]
        [Route("GetPublishers")]
        public Task<List<Userdetail>> GetPublishers(string search = "")
        {
            var result = _usersService.GetPublishers(search: search).Result;
            return Task.FromResult(result);
        }

        [HttpGet]
        [Route("GetWriters")]
        public Task<List<Userdetail>> GetWriters(string search = "")
        {
            var result = _usersService.GetWriters(search: search).Result;
            return Task.FromResult(result);
        }
    }
}
