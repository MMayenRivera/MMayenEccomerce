using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Producto" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Producto.svc or Producto.svc.cs at the Solution Explorer and start debugging.
    public class Producto : IProducto
    {
        public void DoWork()
        {
        }

        public Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.AddEF(producto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }
        public Result Update (ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.UpdateEF(producto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }

        public Result Delete (int IdProducto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.DeleteEF(IdProducto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }

        public Result GetAll ()
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.GetAllEF();

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex, Objects= result.Objects };
        }

        public Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result = BL.Producto.GetByIdEF(IdProducto);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex, Object = result.Object };
        }
    }
}
