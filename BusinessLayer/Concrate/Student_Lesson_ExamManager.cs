

using BusinessLayer.Abstract;
using DataAccesLayer.Abstract;
using EntityLayer.concrate;
using BusinessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLayer.Concrate
{
    public class Student_Lesson_ExamManager : IStudent_Lesson_Exam
    {
        private readonly IStudent_Lesson_ExamDal _StudentLessonExamdal;

        public Student_Lesson_ExamManager(IStudent_Lesson_ExamDal StudentLessonExamddal)
        {
            _StudentLessonExamdal = StudentLessonExamddal;
        }
        public void TInsert(Student_Lesson_Exam studentLessonExam)
        {
            if (studentLessonExam.Grade < 0 || studentLessonExam.Grade > 100)
            {
                throw new Exception("Girilen not 0 ile 100 arasında olmalıdır!");
            }
            bool notVarMi = _StudentLessonExamdal.TGetAll().Any(x => x.Sut_LessonId == studentLessonExam.Sut_LessonId && x.ExamId == studentLessonExam.ExamId);
            if (notVarMi)
            {
                throw new Exception("Bu öğrencinin seçilen sınavı için zaten bir not girilmiş! Notu değiştirmek için 'Güncelle' butonunu kullanın.");
            }
            _StudentLessonExamdal.Insert(studentLessonExam);
        }
        public void TUpdate(Student_Lesson_Exam studentLessonExam)
        {
            if (studentLessonExam.Grade < 0 || studentLessonExam.Grade > 100)
            {
                throw new Exception("Girilen not 0 ile 100 arasında olmalıdır!");
            }

            _StudentLessonExamdal.Update(studentLessonExam);
        }
        public void TDelete(Student_Lesson_Exam studentLessonExam)
        {
            _StudentLessonExamdal.Delete(studentLessonExam);
        }
        public List<Student_Lesson_Exam> TGetAll(params Expression<Func<Student_Lesson_Exam, object>>[] includes)
        {
            return _StudentLessonExamdal.TGetAll(includes);
        }
        public Student_Lesson_Exam TGetById(int id)
        {
            return _StudentLessonExamdal.GetById(id);
        }
        public List<Student_Lesson_ExamDto> GetStudent_Lesson_ExamDetails()
        {
            return _StudentLessonExamdal.GetStudentLessonExamDetails().Select(x => new Student_Lesson_ExamDto
            {
                KayitId = x.Student_Lesson_ExamId,
                OgrenciAdi = x.Sut_Lesson.Student.StudentName,
                DersAdi = x.Sut_Lesson.Lesson.LessonName,
                SinavTuru = x.Exam.Type.ToString(),
                Agirlik = x.Exam.Ağırlık,
                Notu = x.Grade
            }).ToList();
        }
        public DersRaporDto GetDersSonucu(int lessonId)
        {
            var tumNotlar = _StudentLessonExamdal.GetStudentLessonExamDetails().Where(x => x.Exam.LessonId == lessonId).ToList();
            var grupluNotlar = tumNotlar.GroupBy(x => x.Sut_LessonId).ToList();
            var sonucListesi = new List<SonucDto>();

            foreach (var grup in grupluNotlar)
            {
                var ilkKayit = grup.First();
                var dto = new SonucDto
                {
                    OgrenciId = ilkKayit.Sut_Lesson.StudentId,
                    OgrenciAdi = ilkKayit.Sut_Lesson.Student.StudentName,
                    DersAdi = ilkKayit.Exam.Lesson.LessonName
                };

                double toplamOrtalama = 0;
                int girilenAgirlikToplami = 0;

                foreach (var notKaydi in grup)
                {
                    string tur = notKaydi.Exam.Type.ToString().ToLower();
                    if(notKaydi.Exam.Type == ExamType.Vize) 
                    if (tur == "vize") dto.Vize = notKaydi.Grade;
                    else if (tur == "vize2") dto.Vize2 = notKaydi.Grade;
                    else if (tur == "lab") dto.Lab = notKaydi.Grade;
                    else if (tur == "final") dto.Final = notKaydi.Grade;

                    toplamOrtalama += (notKaydi.Grade * notKaydi.Exam.Ağırlık) / 100.0;
                    girilenAgirlikToplami += notKaydi.Exam.Ağırlık;
                }
                if (girilenAgirlikToplami < 100)
                {
                    dto.Ortalama = null;
                    dto.Durum = "Eksik Not";
                }
                else
                {
                    dto.Ortalama = Math.Round(toplamOrtalama, 2);
                    dto.Durum = "Hesaplanacak";
                }

                sonucListesi.Add(dto);
            }
            var tamNotluOgrenciler = sonucListesi.Where(x => x.Durum == "Hesaplanacak").ToList();

            double enYuksekOrtalama = tamNotluOgrenciler.Any() ? tamNotluOgrenciler.Max(x => x.Ortalama ?? 0) : 0;
            double gecmeSiniri = enYuksekOrtalama >= 5 ? (enYuksekOrtalama / 2.0) - 5 : 0;
            if (gecmeSiniri < 0) gecmeSiniri = 0;
            int gecenSayisi = 0;
            int kalanSayisi = 0;
            foreach (var sonuc in sonucListesi)
            {
                if (sonuc.Durum == "Eksik Not")
                    continue;

                if (sonuc.Ortalama >= gecmeSiniri)
                {
                    sonuc.Durum = "Geçti";
                    gecenSayisi++;
                }
                else
                {
                    sonuc.Durum = "Kaldı";
                    kalanSayisi++;
                }
            }
            return new DersRaporDto
            {
                OgrenciListesi = sonucListesi.OrderByDescending(x => x.Ortalama).ToList(),
                GecmeNotu = Math.Round(gecmeSiniri, 2),
                GecenKisi = gecenSayisi,
                KalanKisi = kalanSayisi
            };
        }
    }
}