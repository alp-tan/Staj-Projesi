using EntityLayer.conc;
using EntityLayer.concrate;
using System.Data.Entity;

namespace DataAccesLayer.Context
{
    public class Cont : DbContext
    {
        public Cont()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Sut_Lesson> Sut_Lessons { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Student_Lesson_Exam> Student_Lesson_Exam { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>()
                .HasRequired(t => t.Lesson)
                .WithMany(l => l.Teachers)
                .HasForeignKey(x => x.LessonId);

            modelBuilder.Entity<Student_Lesson_Exam>()
                .HasRequired(sle => sle.Exam)
                .WithMany(x => x.Student_Lesson_Exam_List)
                .HasForeignKey(sle => sle.ExamId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student_Lesson_Exam>()
                .HasRequired(sle => sle.Sut_Lesson)
                .WithMany()
                .HasForeignKey(sle => sle.Sut_LessonId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
