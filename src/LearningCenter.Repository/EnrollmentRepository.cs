using LearningCenter.UserClassDatabase;
using LearningCenter.UserClassDatabase.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCenter.Repository
{
    public interface IEnrollmentRepository
    {
        EnrolllmentModel[] UserClasses { get; }
        EnrolllmentModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        List<EnrolllmentModel> GetEnrolledClasses(int userId);
    }

    public class EnrolllmentModel
    {
        public IEnumerable<Class> ClassList { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
    }

    public class EnrollClassRepository : IEnrollmentRepository
    {
        public EnrolllmentModel[] UserClasses
        {
            get
            {
                return DatabaseAccessor.Instance.UserClass
                    .Select(t => new EnrolllmentModel
                    {
                        ClassId = t.ClassId,
                        UserId = t.UserId
                    })
                    .ToArray();
            }

        }
        public EnrolllmentModel Add(int userId, int classId)
        {

            var newClass = DatabaseAccessor.Instance.UserClass.First(t => t.ClassId == classId);
            var enrollClass = DatabaseAccessor.Instance.User.Where(t => t.UserId == userId).First();

            enrollClass.UserClass.Add(newClass);
            DatabaseAccessor.Instance.SaveChanges();

            return new EnrolllmentModel { UserId = newClass.UserId, ClassId = newClass.ClassId };
        }
        public List<EnrolllmentModel> GetEnrolledClasses(int userId)
        {
            return DatabaseAccessor.Instance.UserClass
                .Where(t => t.UserId == userId)
                .Select(t => new EnrolllmentModel
                {
                    UserId = t.UserId,
                    ClassId = t.ClassId
                })
                .ToList();
        }

        public bool Remove(int userId, int classId)
        {
            var items = DatabaseAccessor.Instance.UserClass
                .Where(t => t.UserId == userId && t.ClassId == classId);

            if (items.Count() == 0)
            {
                return false;
            }

            DatabaseAccessor.Instance.UserClass.Remove(items.First());
            DatabaseAccessor.Instance.SaveChanges();
            return true;

        }
    }
}
