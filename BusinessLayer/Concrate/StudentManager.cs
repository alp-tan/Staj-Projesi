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
    public class StudentManager : IStudentService
    {
        
        private readonly IStudentDal _studentDal;

       
        public StudentManager(IStudentDal studentDal)
        {
            _studentDal = studentDal;
        }
        public List<StudentDto> GetStudentDetails()
        {
            var students = _studentDal.TGetAll();
            return students.Select(x => new StudentDto
            {
                OgrenciID = x.StudentId,
                OgrenciAdi = x.StudentName
            }).ToList();
        }

        public void TInsert(Student entity)
        {
            
            if (!string.IsNullOrWhiteSpace(entity.StudentName) && entity.StudentName.Length >= 2 && entity.StudentName.Length <= 30)
            {
                
                _studentDal.Insert(entity);
            }
            else
            {
                throw new Exception("Öğrenci adı geçersiz! Boş bırakılamaz ve 2-30 karakter arasında olmalıdır.");
            }
        }

        public void TDelete(Student entity)
        {
            _studentDal.Delete(entity);
        }

        public List<Student> TGetAll(params Expression<Func<Student, object>>[] includes)
        {
            return _studentDal.TGetAll(includes);
        }

        public void TUpdate(Student entity)
        {
            _studentDal.Update(entity);
        }

        public Student TGetById(int id)
        {
            return _studentDal.GetById(id);
        }
    }
}