using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.conc;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccesLayer.EntityFramework
{
    public class EfTeacherDal : GenericRepository<Teacher>,ITeacherDal
    {
        public List<Teacher> GetTeacherDetails()
        {
            using (var context = new Cont())
            {
                return context.Teachers
                    .Include(x => x.Lesson)
                    .ToList();
            }
        }
    }
}
