using BusinessLayer.Abstract;
using BusinessLayer.DTO;
using DataAccesLayer.Abstract;
using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLayer.Concrate
{
    public class ExamManager : IExamService
    {
        private readonly IExamDal _examDal;

        public ExamManager(IExamDal examDal)
        {
            _examDal = examDal;
        }

        public void TInsert(Exam entity)
        {
            if (entity.Ağırlık <= 0)
            {
                throw new Exception("Sınav ağırlığı 0 veya negatif olamaz!");
            }
            var mevcutSinavlar = _examDal.TGetAll().Where(x => x.LessonId == entity.LessonId).ToList();
            int toplamAgirlik = mevcutSinavlar.Sum(x => x.Ağırlık);
            if ((toplamAgirlik + entity.Ağırlık) > 100)
            {
                int kalanLimit = 100 - toplamAgirlik;
                throw new Exception($"Bu dersin sınav ağırlıkları toplamı 100'ü geçemez! Ekleyebileceğiniz maksimum ağırlık: {kalanLimit}");
            }
            _examDal.Insert(entity);
        }
        public void TUpdate(Exam entity)
        {
            if (entity.Ağırlık <= 0)
            {
                throw new Exception("Sınav ağırlığı 0 veya negatif olamaz!");
            }
            var digerSinavlar = _examDal.TGetAll()
                .Where(x => x.LessonId == entity.LessonId && x.ExamId != entity.ExamId)
                .ToList();
            int digerToplamAgirlik = digerSinavlar.Sum(x => x.Ağırlık);
            if ((digerToplamAgirlik + entity.Ağırlık) > 100)
            {
                int kalanLimit = 100 - digerToplamAgirlik;
                throw new Exception($"Güncelleme başarısız! Bu dersin diğer sınavlarının toplamı {digerToplamAgirlik}. Bu sınava en fazla {kalanLimit} ağırlık verebilirsiniz.");
            }
            _examDal.Update(entity);
        }
        public void TDelete(Exam entity)
        {
            _examDal.Delete(entity);
        }
        public List<Exam> TGetAll(params Expression<Func<Exam, object>>[] includes)
        {
            return _examDal.TGetAll(includes);
        }
        public Exam TGetById(int id)
        {
            return _examDal.GetById(id);
        }

        public List<ExamDto> GetExamDetails()
        {
            return _examDal.TGetAll(x => x.Lesson).Select(x => new ExamDto
            {
                ExamId = x.ExamId,
                DersAdi = x.Lesson != null ? x.Lesson.LessonName : "Atanmadı",
                SinavTuru = x.Type.ToString(),
                Agirlik = x.Ağırlık
            }).ToList();
        }
        public List<ExamDto> GetExamDetailsByLesson(int lessonId)
        {
            return _examDal.TGetAll(x => x.Lesson)
                .Where(x => x.LessonId == lessonId)
                .Select(x => new ExamDto
                {
                    ExamId = x.ExamId,
                    DersAdi = x.Lesson != null ? x.Lesson.LessonName : "Atanmadı",
                    SinavTuru = x.Type.ToString(),
                    Agirlik = x.Ağırlık
                }).ToList();
        }
    }
}