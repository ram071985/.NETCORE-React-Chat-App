using System;
namespace API
{
    public class UserModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string NewUsername { get; set; }
        public string Password { get; set; }
        public int LastActiveAt { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
