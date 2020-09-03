using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningCenter.WebSite.Models
{
    public class EnrollInClassModel
    {
       public ClassModel[] Classes { get; set; }
       public int ClassId { get; set; } 

    }
}