using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.UserClassDatabase;

namespace LearningCenter.UserClassDatabase
{
    public class DatabaseAccessor
    {
        static DatabaseAccessor()
        {
            Instance = new Db.minicstructorContext();
        }

        public static Db.minicstructorContext Instance { get; private set; }
    }

}

