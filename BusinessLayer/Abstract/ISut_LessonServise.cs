using BusinessLayer.DTO;
using EntityLayer.concrate;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ISut_LessonServise : IGenericService<Sut_Lesson>
    {
        List<SutLessonDto> GetListWithDetails();
    }
}