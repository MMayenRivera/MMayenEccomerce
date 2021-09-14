using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class ProductoController : ApiController
    {
        // GET api/producto
        [HttpGet]
        [Route("api/producto")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/producto/5
        [HttpGet]
        [Route("api/producto/{IdProducto}")]
        public IHttpActionResult GetById(int IdProducto)
        {
            ML.Result result = BL.Producto.GetByIdEF(IdProducto);
            
            if(result.Correct)
            {
                return Ok(result);
            }
            else 
            {
                return NotFound();
            }
        }

        // POST api/producto
        [HttpPost]
        [Route("api/producto")]
        public IHttpActionResult Add([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.AddEF(producto);

            if(result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/producto/5
        [HttpPut]
        [Route("api/producto")]
        public IHttpActionResult Update([FromBody]ML.Producto producto)
        {
            ML.Result result = BL.Producto.UpdateEF(producto);

            if(result.Correct)
            {
                return Ok(producto);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE api/producto/5
        [HttpDelete]
        [Route("api/producto/{IdProducto}")]
        public IHttpActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.DeleteEF(IdProducto);

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
