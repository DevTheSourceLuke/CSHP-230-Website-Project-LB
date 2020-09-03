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

        public UserModel(int id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}