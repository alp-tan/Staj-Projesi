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
    public class Sut_LessonManager : ISut_LessonServise
    {
        private readonly ISut_LessonDal _sutLessonDal;
        public Sut_LessonManager(ISut_LessonDal sutLessonDal)
        {
            _sutLessonDal = sutLessonDal;
        }
        public List<SutLessonDto> GetListWithDetails()
        {
            var sutLessons = _sutLessonDal.GetListWithDetails();
            var sutLessonDtos = sutLessons.Select(x => new SutLessonDto
            {
                KayitNo = x.Id,
                DersId = x.LessonId,
                OgrenciId = x.Student?.StudentId,
                OgrenciAdi = x.Student != null ? x.Student.StudentName : "Bilinmiyor",
                DersAdi = x.Lesson != null ? x.Lesson.LessonName : "Atanmadı",
                OgretmenAdi = (x.Lesson != null && x.Lesson.Teachers != null) ? x.Lesson.Teachers.First().TeacherName : "Atanmadı"
            }).ToList();

            return sutLessonDtos;
        }
        public void TDelete(Sut_Lesson t)
        {
            _sutLessonDal.Delete(t);
        }
        public List<Sut_Lesson> TGetAll(params Expression<Func<Sut_Lesson, object>>[] includes)
        {
            return _sutLessonDal.TGetAll(includes);
        }
        public Sut_Lesson TGetById(int id)
        {
            return _sutLessonDal.GetById(id);
        }
        public void TInsert(Sut_Lesson t)
        {
            _sutLessonDal.Insert(t);
        }
        public void TUpdate(Sut_Lesson t)
        {
            _sutLessonDal.Update(t);
        }
    }
}