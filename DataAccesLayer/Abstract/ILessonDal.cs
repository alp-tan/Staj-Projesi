using EntityLayer.conc;
using EntityLayer.concrate;
using System.Collections.Generic;

namespace DataAccesLayer.Abstract
{
    public interface ILessonDal : IGenericDal<Lesson>
    {
        List<Lesson> GetLessonDetails();
    }
}
