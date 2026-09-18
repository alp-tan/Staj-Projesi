using System;
using System.Collections.Generic;

namespace BusinessLayer.DTO
{
    public class DersRaporDto
    {
        public List<SonucDto> OgrenciListesi { get; set; }
        public double GecmeNotu { get; set; }
        public int GecenKisi { get; set; }
        public int KalanKisi { get; set; }
    }
}