using EntityLayer.concrate;
using System.Collections.Generic;

namespace DataAccesLayer.Abstract
{
    public interface ISut_LessonDal : IGenericDal<Sut_Lesson>
    {
        List<Sut_Lesson> GetListWithDetails();
    }
}