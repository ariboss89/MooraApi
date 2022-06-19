using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooraMethodApi.Models
{
    public class tb_keputusan
    {
        public int Id { get; set; }
        public string idkeputusan { get; set; }
        public string nama { get; set; }
        public double hasil_akhir { get; set; }
        public string ket { get; set; }
        public DateTime tanggal { get; set; }
    }
}