using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MooraMethodApi.Models
{
    public class tb_hasil
    {
        public int Id { get; set; }
        public string nama { get; set; }
        public string kriteria { get; set; }
        public double nilai { get; set; }
        public double? hasil { get; set; }
        public string ket { get; set; }
    }
}