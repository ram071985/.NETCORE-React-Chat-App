using System;
namespace API
{
    public class Users
    {
        public int id { get; set; } = 0;
        public int Id { get; internal set; }
        public string username { get; set; } = "";
    }
}
