using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.WebSite.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }

        public UserModel(int id, string email)//, string password)
        {
            Id = id;
            Email = email;
            //Password = password;
        }
    }
}