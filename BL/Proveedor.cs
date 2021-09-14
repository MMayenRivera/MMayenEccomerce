using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Proveedor
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var proveedores = context.ProveedorGetAll().ToList();
                    result.Objects = new List<object>();

                    if(proveedores != null)
                    {
                        foreach(var obj in proveedores)
                        {
                            ML.Proveedor proveedor = new ML.Proveedor();

                            proveedor.IdProveedor = obj.IdProveedor;
                            proveedor.Nombre = obj.Nombre;
                            proveedor.Telefono = obj.Telefono.Value;

                            result.Objects.Add(proveedor);
                            result.Correct = true;
                        }                                            
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los proveedores";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
