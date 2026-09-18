using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public string DersAdi { get; set; }
        public string SinavTuru { get; set; }
        public int Agirlik { get; set; }
    }
}
