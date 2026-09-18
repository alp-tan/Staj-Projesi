using BusinessLayer.Abstract;
using BusinessLayer.DTO;
using DataAccesLayer.Abstract;
using EntityLayer.conc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace BusinessLayer.Concrate
{
    public class LessonManager : ILessonService
    {
        private readonly ILessonDal _lessonDal;
       
        public LessonManager(ILessonDal lessonDal)
        {
            _lessonDal = lessonDal;
        }
        public List<LessonDto> GetLessonDetails()
        {
            var lessons = _lessonDal.TGetAll();

            var lessonDtoList = lessons.Select(x => new LessonDto
            {

                DersID = x.LessonId,
                DersAdi = x.LessonName,
               
            }).ToList();

            return lessonDtoList;
        }

        public void TDelete(Lesson entity)
        {
            _lessonDal.Delete(entity);
        }
        public List<Lesson> TGetAll()
        {
            return _lessonDal.TGetAll();
        }

        public List<Lesson> TGetAll(params Expression<Func<Lesson, object>>[] includes)
        {
            return _lessonDal.TGetAll(includes);
        }

        public Lesson TGetById(int id)
        {
            return _lessonDal.GetById(id);
        }

        public void TInsert(Lesson entity)
        {
            if (!string.IsNullOrWhiteSpace(entity.LessonName) && entity.LessonName.Length >= 3)
            {
                _lessonDal.Insert(entity);
            }
            else
            {
                throw new Exception("Ders adı boş olamaz ve en az 3 karakterden oluşmalıdır.");
            }
        }

        public void TUpdate(Lesson entity)
        {
            _lessonDal.Update(entity);
        }
    }
}