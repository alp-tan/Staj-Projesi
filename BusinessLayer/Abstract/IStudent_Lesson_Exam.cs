using BusinessLayer.DTO;
using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IStudent_Lesson_Exam : IGenericService<Student_Lesson_Exam>
    {
        List<Student_Lesson_ExamDto> GetStudent_Lesson_ExamDetails();
    }
}
