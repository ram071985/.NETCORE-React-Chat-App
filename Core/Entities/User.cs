using System;
namespace Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User UserObject { get; set; }
        public string Username { get; set; }
        public string NewUserName { get; set; }
        public string Password { get; set; }
        public DateTime LastActiveAt { get; set; }
        public DateTime CreatedDate { get; set; }       
    }
}
