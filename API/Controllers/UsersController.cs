using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Collections.Generic;
using API;

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

        [HttpGet]
        public List<Users> Get()
        {
            return user;
        }

        [HttpPost]
        public void Post(Users val)
        {
            user.Add(val);
        }

    [HttpGet]
    public Users Get()
    {
        return user.Where(x => x.Id == IDesignTimeMvcBuilderConfiguration).FirstOrDefault();
    }



}

