using System;

namespace BusinessLayer.DTO
{
    public class Student_Lesson_ExamDto
    {
        public int KayitId { get; set; }
        public string OgrenciAdi { get; set; }
        public string DersAdi { get; set; }
        public string SinavTuru { get; set; }
        public int Agirlik { get; set; }
        public int Notu { get; set; }
    }
}