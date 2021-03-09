using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsStacks.IService;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("")]
        public dynamic Login(LoginRequest request)
        {
            return _loginService.Login(request: request);
        }
    }
}
