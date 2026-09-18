using BusinessLayer.DTO;
using EntityLayer.concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IExamService : IGenericService<Exam>
    {
        List<ExamDto> GetExamDetails();
        List<ExamDto> GetExamDetailsByLesson(int lessonId);
    }
}
