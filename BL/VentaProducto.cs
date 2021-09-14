using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class VentaProducto
    {
        public static ML.Result AddSP(ML.VentaProducto ventaproducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "VentaProductoAdd";

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdVenta", SqlDbType.Int);
                        collection[0].Value = ventaproducto.Venta.IdVenta;

                        collection[1] = new SqlParameter("Cantidad", SqlDbType.Int);
                        collection[1].Value = ventaproducto.Cantidad;

                        collection[2] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[2].Value = ventaproducto.Producto.IdProducto;

                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción de la venta";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result AddEF(ML.VentaProducto ventaproducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {

                    var query = context.VentaProductoAdd(ventaproducto.Venta.IdVenta, ventaproducto.Cantidad, ventaproducto.Producto.IdProducto);
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al realizar la insercción del producto";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
