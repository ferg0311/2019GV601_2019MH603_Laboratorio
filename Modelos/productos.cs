using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019GV601_2019MH603_Laboratorio.Modelos
{
    public class productos
    {
        [Key]
        public int id { get; set; }
        public String producto { get; set; }
        public decimal precio { get; set; }
    }
}
