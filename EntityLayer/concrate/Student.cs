using EntityLayer.concrate;
using System.Collections.Generic;

namespace EntityLayer.conc
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Sut_Lesson> Sut_Lessons { get; set; }
    }
}
