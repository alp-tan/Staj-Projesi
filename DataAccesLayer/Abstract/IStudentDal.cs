using EntityLayer.conc;
using System.Collections.Generic;

namespace DataAccesLayer.Abstract
{
   public interface IStudentDal : IGenericDal<Student>
    {
        List<Student> GetStudentDetails();
    }
}
