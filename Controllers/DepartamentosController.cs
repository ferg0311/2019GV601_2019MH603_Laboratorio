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
    public class DepartamentosController : ControllerBase
    {
        private readonly ventasContext _contexto;

        public DepartamentosController(ventasContext miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/departamentos/")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<departamentos> departamentosList = from de in _contexto.Departamentos
                                                               select de;
                if (departamentosList.Count() > 0)
                {
                    return Ok((departamentos)departamentosList);
                }
                return BadRequest();
            }
            catch (Exception )
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/departamentos/{idDepartamento}")]
        public IActionResult Get(int id_departamento)
        {
            try
            {
                departamentos departamento = (from de in _contexto.Departamentos
                                              where de.id == id_departamento
                                              select de).FirstOrDefault();
                if (departamento != null)
                {
                    return Ok(departamento);
                }
                return BadRequest();
                
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private IActionResult Ok(departamentos departamento)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("api/departamentos/")]
        public IActionResult GuardarDepartamento([FromBody] departamentos deptoNuevo)
        {
            try
            {
                _contexto.Add(deptoNuevo);
                _contexto.SaveChanges();
                return Ok(deptoNuevo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/departamentos/")]
        public IActionResult UpdateDepartamento([FromBody] departamentos deptoAModificar)
        {
            try
            {
                departamentos deptoExiste = (from de in _contexto.Departamentos
                                             where de.id == deptoAModificar.id
                                             select de).FirstOrDefault();
                if (deptoAModificar is null)
                {
                    
                }

                deptoExiste.departamento = deptoAModificar.departamento;

                _contexto.Entry(deptoExiste).State = EntityState.Modified;
                _contexto.SaveChanges();
                return OkResult(deptoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private IActionResult BadRequest()
        {
            throw new NotImplementedException();
        }

        private IActionResult OkResult(departamentos deptoExiste)
        {
            throw new NotImplementedException();
        }
    }

}

