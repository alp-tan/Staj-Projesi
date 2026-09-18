using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccesLayer.EntityFramework
{
    public class EfStudent_Lesson_ExamDal : GenericRepository<Student_Lesson_Exam>,IStudent_Lesson_ExamDal
    {
        public List<Student_Lesson_Exam> GetStudentLessonExamDetails()
        {
            using (var context = new Cont())
            {
                return context.Set<Student_Lesson_Exam>()
                    .Include(x => x.Exam)
                    .Include(x => x.Exam.Lesson)
                    .Include(x => x.Sut_Lesson)
                    .Include(x => x.Sut_Lesson.Student)
                    .ToList();

            }
        }
    }
}
