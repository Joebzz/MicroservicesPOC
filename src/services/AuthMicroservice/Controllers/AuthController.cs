using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private IOptions<Audience> _settings;

        public AuthController(IOptions<Audience> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Get(string name, string pwd)
        {
            return new JsonResult(new
            {
                Result = false,
                Message = "Authentication Failure"
            });
        }
    }

    public class Audience
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}
