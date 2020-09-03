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
        ClassModel GetClass(int classId);
        ClassModel[] StudentClasses(int userId);
        ClassModel[] AddClass(int classId, int userId);       
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
                                        .Where(c => c.ClassId == classId)
                                        .Select(c => new ClassModel
                                        {
                                            Id = c.ClassId,
                                            Name = c.ClassName,
                                            Description = c.ClassDescription,
                                            Price = c.ClassPrice
                                        })
                                        .First();
            }
        public ClassModel[] Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Class
                                            .Select(c => new ClassModel
                                            {
                                                Id = c.ClassId,
                                                Name = c.ClassName,
                                                Description = c.ClassDescription,
                                                Price = c.ClassPrice
                                            })
                                            .ToArray();
                }
        }

        public ClassModel[] StudentClasses(int userId)
        {
            return DatabaseAccessor.Instance.Class
                                            .Where(c => DatabaseAccessor.Instance.UserClass
                                                    .Where(uc => uc.UserId == userId)
                                                    .Select(uc => uc.ClassId)
                                                    .Contains(c.ClassId))
                                            .Select(c => new ClassModel
                                            {
                                                Id = c.ClassId,
                                                Name = c.ClassName,
                                                Description = c.ClassDescription,
                                                Price = c.ClassPrice
                                            })
                                            .ToArray();
        }
        
        public ClassModel[] AddClass(int classId, int userId)
        {
            var user = DatabaseAccessor.Instance.User.First(u => u.UserId == userId);
            var classToAdd = DatabaseAccessor.Instance.Class.First(c => c.ClassId == classId);

            DatabaseAccessor.Instance.UserClass
                                    .Add(new UserClass
                                    {
                                        ClassId = classToAdd.ClassId,
                                        UserId = user.UserId
                                    });

            DatabaseAccessor.Instance.SaveChanges();

            return (StudentClasses(userId));
        }
    }
}
