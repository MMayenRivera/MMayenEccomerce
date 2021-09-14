using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProducto" in both code and config file together.
    [ServiceContract]
    public interface IProducto
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        Result Add(ML.Producto producto);

        [OperationContract]
        Result Update(ML.Producto producto);

        [OperationContract]
        Result Delete(int IdProducto);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Departamento))]
        Result GetAll();

        [OperationContract]
        [ServiceKnownType(typeof(ML.Departamento))]
        Result GetById(int IdProducto);
    }
}
