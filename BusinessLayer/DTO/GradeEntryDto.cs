using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTO
{
    public class GradeEntryDto
    {
        [Browsable(false)]
        public int StudentExamId { get; set; }
        public int OgrenciId { get; set; }
        public string OgrenciAdi { get; set; }
        public int? Vize { get; set; }
        public int? Vize2 { get; set; }
        public int? Lab { get; set; }
        public int? Final { get; set; }
    }
}
