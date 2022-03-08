using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace _2019GV601_2019MH603_Laboratorio.Modelos
{
    public class departamentos
    {
        [Key]
        public int id { get; set; }
        public String departamento { get; set; }

    }
}
