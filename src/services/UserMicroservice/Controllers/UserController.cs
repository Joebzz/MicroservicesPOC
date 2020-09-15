using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Model;

namespace UserMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private static List<User> _users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "Test Name 1", Email = "testemail1@testmail.com" },
                new User { Id = Guid.NewGuid(), Username = "Test Name 2", Email = "testemail2@testmail.com" },
                new User { Id = Guid.NewGuid(), Username = "Test Name 3", Email = "testemail3@testmail.com" },
                new User { Id = Guid.NewGuid(), Username = "Test Name 4", Email = "testemail4@testmail.com" },
                new User { Id = Guid.NewGuid(), Username = "Test Name 5", Email = "testemail5@testmail.com" },
            };

        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_users);
        }

        // GET <UserController>/110ec627-2f05-4a7e-9a95-7a91e8005da8
        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            var user = _users.FirstOrDefault(i => i.Id == id);
            return Ok(user);
        }

    }
}
