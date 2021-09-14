using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class DepartamentoController : ApiController
    {
        // GET api/departamento
        [HttpGet]
        [Route("api/departamento")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Departamento.GetAllEF();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/departamento/5
        [HttpGet]
        [Route("api/departamento/{IdDepartamento}")]
        public IHttpActionResult GetById(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.GetByIdEF(IdDepartamento);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/departamento
        [HttpPost]
        [Route("api/departamento")]
        public IHttpActionResult Add([FromBody]ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.AddEF(departamento);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        

        // DELETE api/departamento/5
        [HttpDelete]
        [Route("api/departamento/{IdDepartamento}")]
        public IHttpActionResult Delete(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/departamento/5
        [HttpPut]
        [Route("api/departamento")]
        public IHttpActionResult Update([FromBody]ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.UpdateEF(departamento);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
