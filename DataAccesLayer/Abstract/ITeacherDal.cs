using EntityLayer.conc;
using System.Collections.Generic;

namespace DataAccesLayer.Abstract
{
    public interface ITeacherDal : IGenericDal<Teacher>
    {
        List<Teacher> GetTeacherDetails();
    }
}
