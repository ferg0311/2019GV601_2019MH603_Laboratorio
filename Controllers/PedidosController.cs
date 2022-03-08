using _2019GV601_2019MH603_Laboratorio.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace _2019GV601_2019MH603_Laboratorio.Controllers
{
    //[Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ventasContext _context;
        public PedidosController(ventasContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/pedidos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoPedidos = (from e in _context.Pedidos
                                      join m in _context.Clientes.DefaultIfEmpty() on e.id_cliente equals m.id

                                      select new
                                      {
                                          e.id,
                                          e.id_cliente,
                                          e.fecha_pedido
                                      }).OrderBy(m => m.id);
                if (listadoPedidos.Count() > 0)
                {
                    return Ok(listadoPedidos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/pedidos/{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                var xPedidos = (from e in _context.Pedidos
                                join m in _context.Clientes on e.id_cliente equals m.id
                                where e.id == Id
                                select new
                                {
                                    e.id,
                                    e.id_cliente,
                                    e.fecha_pedido
                                }).FirstOrDefault();
                if (xPedidos != null)
                {
                    return Ok(xPedidos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/pedidos")]
        public IActionResult guardarPedido([FromBody] pedidos pedidoNuevo)
        {
            try
            {
                _context.Pedidos.Add(pedidoNuevo);
                _context.SaveChanges();
                return Ok(pedidoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/Pedidos")]
        public IActionResult updateEquipo([FromBody] pedidos equipoAModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            pedidos equipoExiste = (from e in _context.Pedidos
                                    where e.id == equipoAModificar.id
                                    select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                // Si no existe el registro retornar un NO ENCONTRADO
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos a modificar
            equipoExiste.fecha_pedido = equipoAModificar.fecha_pedido;

            //Se envia el objeto a la base de datos.
            _context.Entry(equipoExiste).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}
    
