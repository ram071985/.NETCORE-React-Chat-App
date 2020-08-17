using System;
namespace Core.Entities
{
    public class Message
    {

        public int SessionId { get; set; }
        public int UserId { get; set; }
        public User User { get; internal set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
