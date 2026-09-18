using BusinessLayer.Abstract;
using BusinessLayer.DTO;
using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using EntityLayer.conc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLayer.Concrate
{
    public class TeacherManager : ITeacherServise
    {     
        private readonly ITeacherDal _teacherDal;

        public TeacherManager(ITeacherDal teacherDal)
    {
        _teacherDal = teacherDal;
    }

        public List<TeacherDto> GetTeacherDetails()
        {
            var teachers = _teacherDal.GetTeacherDetails();
            return teachers.Select(x => new TeacherDto
            {
                OgretmenID = x.TeacherId,
                OgretmenAdi = x.TeacherName,
                VerdigiDers = x.Lesson != null ? x.Lesson.LessonName : "Atanmadı"
            }).ToList();
        }

        public void TDelete(Teacher entity)
    {
        _teacherDal.Delete(entity);
    }

        public List<Teacher> TGetAll(params Expression<Func<Teacher, object>>[] includes)
        {
            return _teacherDal.TGetAll(includes);
        }

        public Teacher TGetById(int id)
    {
        return _teacherDal.GetById(id);
    }

        public void TInsert(Teacher teacher)
        {
            _teacherDal.Insert(teacher);
        }

        public void TUpdate(Teacher entity)
    {
        _teacherDal.Update(entity);
    }
}
}