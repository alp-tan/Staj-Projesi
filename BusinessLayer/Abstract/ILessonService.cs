using BusinessLayer.DTO;
using EntityLayer.conc;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ILessonService : IGenericService<Lesson>
    {
        List<LessonDto> GetLessonDetails();
    }
}
