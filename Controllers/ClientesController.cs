using _2019GV601_2019MH603_Laboratorio.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace _2019GV601_2019MH603_Laboratorio.Controllers
{
    public class ClientesController :ControllerBase
    {
        private readonly ventasContext _contexto;

        public ClientesController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/clientes/")]
        public IActionResult Get()
        {
            try
            {
                var listadoClientes = (from c in _contexto.Clientes
                                       join d in _contexto.Departamentos on c.id_departamento equals d.id
                                       select new
                                       {
                                           c.id,
                                           c.nombre,
                                           c.id_departamento,
                                           departamento = d.departamento,
                                           c.fecha_nac
                                       }).OrderBy(c => c.nombre);
                if (listadoClientes.Count() > 0)
                {
                    return Ok(listadoClientes);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("api/clientes/{idCliente}")]
        public IActionResult Get(int idCliente)
        {
            try
            {
                var unEquipo = (from c in _contexto.Clientes
                                join d in _contexto.Departamentos on c.id_departamento equals d.id
                                where c.id == idCliente
                                select new
                                {
                                    c.id,
                                    c.nombre,
                                    c.id_departamento,
                                    departamento = d.departamento,
                                    c.fecha_nac
                                }).FirstOrDefault();
                if (unEquipo != null)
                {
                    return Ok(unEquipo);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/clientes")]
        public IActionResult guardarCliente([FromBody] clientes clienteNuevo)
        {
            try
            {
                _contexto.Clientes.Add(clienteNuevo);
                _contexto.SaveChanges();
                return Ok(clienteNuevo);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/clientes")]
        public IActionResult updateCliente([FromBody] clientes clienteAModificar)
        {
            try
            {
                clientes clienteExiste = (from c in _contexto.Clientes
                                          where c.id == clienteAModificar.id
                                          select c).FirstOrDefault();

                if (clienteExiste is null)
                {
                    return NotFound();
                }

                clienteExiste.nombre = clienteAModificar.nombre;
                clienteExiste.id_departamento = clienteAModificar.id_departamento;

                _contexto.Entry(clienteExiste).State = EntityState.Modified;
                _contexto.SaveChanges();

                return Ok(clienteExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

}

