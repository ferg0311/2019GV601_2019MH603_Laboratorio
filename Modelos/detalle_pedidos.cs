using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2019GV601_2019MH603_Laboratorio.Modelos
{
    public class detalle_pedidos
    {
        [Key]
        public int id { get; set; }
        public int id_pedido { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
    }
}
