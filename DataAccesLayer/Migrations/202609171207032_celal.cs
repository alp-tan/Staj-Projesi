namespace DataAccesLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class celal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ExamId = c.Int(nullable: false, identity: true),
                        LessonId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Ağırlık = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ExamId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        LessonName = c.String(),
                    })
                .PrimaryKey(t => t.LessonId);
            
            CreateTable(
                "dbo.Sut_Lesson",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Student_Lesson_Exam",
                c => new
                    {
                        Student_Lesson_ExamId = c.Int(nullable: false, identity: true),
                        ExamId = c.Int(nullable: false),
                        Sut_LessonId = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Student_Lesson_ExamId)
                .ForeignKey("dbo.Exams", t => t.ExamId)
                .ForeignKey("dbo.Sut_Lesson", t => t.Sut_LessonId)
                .Index(t => t.ExamId)
                .Index(t => t.Sut_LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student_Lesson_Exam", "Sut_LessonId", "dbo.Sut_Lesson");
            DropForeignKey("dbo.Student_Lesson_Exam", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Teachers", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Sut_Lesson", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Sut_Lesson", "LessonId", "dbo.Lessons");
            DropIndex("dbo.Student_Lesson_Exam", new[] { "Sut_LessonId" });
            DropIndex("dbo.Student_Lesson_Exam", new[] { "ExamId" });
            DropIndex("dbo.Teachers", new[] { "LessonId" });
            DropIndex("dbo.Sut_Lesson", new[] { "LessonId" });
            DropIndex("dbo.Sut_Lesson", new[] { "StudentId" });
            DropIndex("dbo.Exams", new[] { "LessonId" });
            DropTable("dbo.Student_Lesson_Exam");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Sut_Lesson");
            DropTable("dbo.Lessons");
            DropTable("dbo.Exams");
        }
    }
}
