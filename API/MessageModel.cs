using System;
namespace API
{
    public class MessageModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
