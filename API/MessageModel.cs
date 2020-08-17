﻿using System;
using Core.Entities;

namespace API
{
    public class MessageModel
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
