using BusinessLayer.DTO;
using EntityLayer.conc;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface ITeacherServise : IGenericService<Teacher>
    {
        List<TeacherDto> GetTeacherDetails();
    }
}
