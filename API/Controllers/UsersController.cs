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
        List<Users> user = new List<Users>();

        public UsersController()
        {
            user.Add(new Users { id = 1, username = "Reid" });
            user.Add(new Users { id = 2, username = "James" });
            user.Add(new Users { id = 3, username = "Rick" });
        }

        public List<Users> Get()
        {
            return user;
        }

        public void Post(Users val)
        {
            user.Add(val);
        }

    public Users Get(int id)
    {
        return user.Where(x => x.Id == id).FirstOrDefault();
    }
}

