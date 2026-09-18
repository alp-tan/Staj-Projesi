using BusinessLayer.DTO;
using EntityLayer.conc;
using System.Collections.Generic;

namespace BusinessLayer.Abstract
{
    public interface IStudentService : IGenericService<Student>
    {
        List<StudentDto> GetStudentDetails();
    }
}
