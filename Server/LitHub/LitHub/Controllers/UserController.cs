using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LitHub.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("[action]")]
        public RegisterResponse Register([FromBody] RegisterRequest request)
        {
            if (request.Username == "test" && request.Password == "test")
            {
                return new RegisterResponse() { Registered = true };
            }
            return new RegisterResponse() { Registered = false };
        }

        [HttpPost("[action]")]
        public LoginResponse Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "admin")
            {
                return new LoginResponse() { Signed = true };
            }
            return new LoginResponse() { Signed = false };
        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RegisterResponse
    {
        public bool Registered { get; set; }
    }

    public class LoginResponse
    {
        public bool Signed { get; set; }
    }
}