using EntityLayer.concrate;
using System.Collections.Generic;

namespace EntityLayer.conc
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Sut_Lesson> Sut_Lessons { get; set; }

    }
}
