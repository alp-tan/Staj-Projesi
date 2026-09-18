using EntityLayer.conc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.concrate
{
    public enum ExamType
    {
        Vize = 1,
        Vize2,
        Lab,
        Final

    }
    public class Exam
    {
        public int ExamId { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public ExamType Type{ get; set; }
        public int Ağırlık { get; set; }
        public virtual List<Student_Lesson_Exam> Student_Lesson_Exam_List { get; set; }

    }
}
