using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;
using System.Linq;

[ApiController]
[Route("api/users")]

 public class UsersController : ControllerBase
 {
        List<Users> users = new List<Users>();

        public UsersController()
        {
            users.Add(new Users { Id = 1, Username = "Reid" });
            users.Add(new Users { Id = 2, Username = "James" });
            users.Add(new Users { Id = 3, Username = "Rick" });
        }

    [HttpGet]
    public List<Users> Get()
        {
            return users;
    }
    [HttpPost]

    public void Post(Users val)
        {
            users.Add(val);
        }

    [HttpGet("{id}")]
    public Users Get(int id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);
        return user;
    }
}

