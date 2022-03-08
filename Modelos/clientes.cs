using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace _2019GV601_2019MH603_Laboratorio.Modelos
{
    public class clientes
    {
        [Key]
        public int id { get; set; }
        public int id_departamento { get; set; }
        public String nombre { get; set; }
        public DateOnly fecha_nac { get; set; }
    }
}
