using System;
namespace API
{
    public class UserModel
    {

        public int Id { get; internal set; }

        public string UserName { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
