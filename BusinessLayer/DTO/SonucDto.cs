using System;
using System.Collections.Generic;

namespace BusinessLayer.DTO
{
    public class SonucDto
    {
        public int OgrenciId { get; set; }
        public string OgrenciAdi { get; set; }
        public string DersAdi { get; set; }
        public int? Vize { get; set; }
        public int? Vize2 { get; set; }
        public int? Lab { get; set; }
        public int? Final { get; set; }
        public double? Ortalama { get; set; }
        public string Durum { get; set; }
    }
}