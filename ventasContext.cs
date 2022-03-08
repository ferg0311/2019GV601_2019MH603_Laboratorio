using _2019GV601_2019MH603_Laboratorio.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019GV601_2019MH603_Laboratorio
{
    public class ventasContext :DbContext
    {
        public ventasContext(DbContextOptions<ventasContext> options) : base(options)
        {

        }

        public DbSet<clientes> Clientes { get; set; }
        public DbSet<departamentos> Departamentos { get; set; }
        public DbSet<detalle_pedidos> DetallePedidos { get; set; }
        public DbSet<pedidos> Pedidos { get; set; }
        public DbSet<productos> Productos { get; set; }

    }
}
