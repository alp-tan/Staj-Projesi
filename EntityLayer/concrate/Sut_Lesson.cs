using EntityLayer.conc;

namespace EntityLayer.concrate
{
    public class Sut_Lesson
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
