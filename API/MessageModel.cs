using System;
namespace API
{
    public class MessageModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
