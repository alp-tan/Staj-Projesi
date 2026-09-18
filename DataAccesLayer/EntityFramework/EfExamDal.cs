using DataAccesLayer.Abstract;
using DataAccesLayer.Context;
using DataAccesLayer.Repositories;
using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace DataAccesLayer.EntityFramework
{
    public class EfExamDal : GenericRepository<Exam>, IExamDal
    {
        public List<Exam> GetExamDetails()
        {
            using (var context = new Cont())
            {
                return context.Set<Exam>()
                    .Include(x => x.Lesson)
                    .ToList();

            }
        }
    }
}
