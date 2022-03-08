using _2019GV601_2019MH603_Laboratorio.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace _2019GV601_2019MH603_Laboratorio.Controllers
{
    public class DetallePedidosControllercs :ControllerBase
    {
        private readonly ventasContext _contexto;

        public DetallePedidosControllercs(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/detalles_pedidos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoDetalles = (from dp in _contexto.DetallePedidos
                                       join pe in _contexto.Pedidos on dp.id_pedido equals pe.id
                                       join po in _contexto.Productos on dp.id_producto equals po.id
                                       join c in _contexto.Clientes on pe.id_cliente equals c.id
                                       select new
                                       {
                                           dp.id,
                                           dp.id_pedido,
                                           cliente = c.nombre,
                                           fecha = pe.fecha_pedido,
                                           dp.id_producto,
                                           producto = po.producto,
                                           precio = po.precio,
                                           dp.cantidad
                                       }).OrderBy(pe => pe.id_pedido);
                if (listadoDetalles.Count() > 0)
                {
                    return Ok(listadoDetalles);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/detalles_pedidos/{idDetalle}")]
        public IActionResult Get(int idDetalle)
        {
            try
            {
                var unDetalle = (from dp in _contexto.DetallePedidos
                                 join pe in _contexto.Pedidos on dp.id_pedido equals pe.id
                                 join po in _contexto.Productos on dp.id_producto equals po.id
                                 join c in _contexto.Clientes on pe.id_cliente equals c.id
                                 where dp.id == idDetalle
                                 select new
                                 {
                                     dp.id,
                                     dp.id_pedido,
                                     cliente = c.nombre,
                                     fecha = pe.fecha_pedido,
                                     dp.id_producto,
                                     producto = po.producto,
                                     precio = po.precio,
                                     dp.cantidad
                                 }).FirstOrDefault();
                if (unDetalle != null)
                {
                    return Ok(unDetalle);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/detalles_pedidos")]
        public IActionResult guardarCliente([FromBody] detalle_pedidos detalleNuevo)
        {
            try
            {
                _contexto.DetallePedidos.Add(detalleNuevo);
                _contexto.SaveChanges();
                return Ok(detalleNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/detalles_pedidos")]
        public IActionResult updateDetalle([FromBody] detalle_pedidos detalleAModificar)
        {
            try
            {
                detalle_pedidos detalleExiste = (from dp in _contexto.DetallePedidos
                                                where dp.id == detalleAModificar.id
                                                select dp).FirstOrDefault();

                if (detalleExiste is null)
                {
                    return NotFound();
                }

                detalleExiste.cantidad = detalleAModificar.cantidad;
                detalleExiste.id_producto = detalleAModificar.id_producto;

                _contexto.Entry(detalleExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(detalleExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
