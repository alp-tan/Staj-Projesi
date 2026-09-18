using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Abstract
{
    public interface IStudent_Lesson_ExamDal : IGenericDal<Student_Lesson_Exam>
    {
        List<Student_Lesson_Exam> GetStudentLessonExamDetails();
    }
}
