using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.conc;
using System.Collections.Generic;
using System.Linq;

namespace DataAccesLayer.EntityFramework
{
    public class EfStudentDal : GenericRepository<Student>,IStudentDal 
    {
        public List<Student> GetStudentDetails()
        {
            using (var context = new Cont())
            {
                return context.Students.Select(x => new Student
                {
                    StudentId = x.StudentId,
                    StudentName = x.StudentName
                }).ToList();
            }
        }
    }
}
