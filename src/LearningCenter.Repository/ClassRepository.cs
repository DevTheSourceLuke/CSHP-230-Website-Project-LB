using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.UserClassDatabase;
using LearningCenter.UserClassDatabase.Db;

namespace LearningCenter.Repository
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        // ClassModel Class(int classId);
        ClassModel GetClass(int classId);
        ClassModel[] StudentClasses(int userId);
        //ClassModel[] AddClasses(int classId, int userId);       
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel GetClass(int classId)
        {
            return DatabaseAccessor.Instance.Class
                        .Where(t => t.ClassId == classId)
                        .Select(t => new ClassModel
                        {
                            Id = t.ClassId,
                            Name = t.ClassName,
                            Description = t.ClassDescription,
                            Price = t.ClassPrice
                        })
                        .First();
        }
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Class
                                        .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                                        .ToArray();
            }
        }

        public ClassModel[] StudentClasses(int userId)
        {
            return DatabaseAccessor.Instance.User.First(t => t.UserId == userId).UserClass
                                            .Select(t => new ClassModel { Id = t.ClassId})
                                            .ToArray();
        }

        //public ClassModel Class(int classId)
        //{
        //    var studentclass = DatabaseAccessor.Instance.Class
        //                                        .Where(t => t.ClassId == classId)
        //                                        .Select(t => new ClassModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
        //                                        .First();
        //    return studentclass;
        //}
    }
}
