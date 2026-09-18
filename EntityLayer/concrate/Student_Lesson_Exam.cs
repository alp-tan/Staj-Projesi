using EntityLayer.conc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.concrate
{
    public class Student_Lesson_Exam
    {
        public int Student_Lesson_ExamId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int Sut_LessonId { get; set; }
        public Sut_Lesson Sut_Lesson { get; set; }
        public int Grade { get; set; }

    }
}
