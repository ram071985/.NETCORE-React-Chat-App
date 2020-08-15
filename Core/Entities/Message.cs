﻿using System;
namespace Core.Entities
{
    public class Message
    {
       public int Id { get; set; }
       public User User { get; set; }
       public string Text { get; set; }
       public DateTime CreatedDate { get; set; }
    }
}
