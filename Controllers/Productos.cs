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
    public class Productos : ControllerBase
    {
        private readonly ventasContext _context;

        public Productos(ventasContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/productos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoProductos = (from e in _context.Productos
                                        select new
                                        {
                                            e.id,
                                            e.producto,
                                            e.precio
                                        }).OrderBy(m => m.id);
                if (listadoProductos.Count() > 0)
                {
                    return Ok(listadoProductos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/productos/{Id}")]
        public IActionResult Get(int Id)
        {
            try
            {
                var xProductos = (from e in _context.Productos
                                  where e.id == Id
                                  select new
                                  {
                                      e.id,
                                      e.producto,
                                      e.precio
                                  }).FirstOrDefault();
                if (xProductos != null)
                {
                    return Ok(xProductos);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        
    }

}

