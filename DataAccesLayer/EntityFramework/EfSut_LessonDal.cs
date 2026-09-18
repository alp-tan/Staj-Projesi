using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.concrate;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccesLayer.EntityFramework
{
    public class EfSut_LessonDal : GenericRepository<Sut_Lesson>, ISut_LessonDal
    {
        public List<Sut_Lesson> GetListWithDetails()
        {
            using (var context = new Cont())
            {
                return context.Set<Sut_Lesson>()
                    .Include(x => x.Student)
                    .Include(x => x.Lesson)
                    .Include(x => x.Lesson.Teachers)
                    .ToList();

            }
        }
    }
}