using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.conc;
using EntityLayer.concrate;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccesLayer.EntityFramework
{
    public class EfLessonDal : GenericRepository<Lesson>,ILessonDal
    {
        public List<Lesson> GetLessonDetails()
        {
            using (var context = new Cont())
            {
                return context.Lessons.ToList();
            }
        }
    }
}
