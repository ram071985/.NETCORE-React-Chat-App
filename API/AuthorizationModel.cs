using System;
namespace API
{
    public class AuthorizationModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
