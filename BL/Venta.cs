using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Venta
    {
        public static ML.Result AddSP(ML.Venta venta, List<object>Objects)
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
                        cmd.CommandText = "VentaAdd";
                     
                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("@IdVenta", SqlDbType.Int);
                        collection[0].Direction = ParameterDirection.Output;

                        collection[1] = new SqlParameter("IdCliente", SqlDbType.Int);
                        collection[1].Value = venta.Cliente.IdCliente;

                        collection[2] = new SqlParameter("Total", SqlDbType.Int);
                        collection[2].Value = venta.Total;

                        collection[3] = new SqlParameter("IdMetodoPago", SqlDbType.Bit);
                        collection[3].Value = venta.Metodopago.IdMetodoPago;

                        
                        cmd.Parameters.AddRange(collection);

                        context.Open();                    

                        int RowsAffected = cmd.ExecuteNonQuery();

                        venta.IdVenta = Convert.ToInt16(cmd.Parameters["@IdVenta"].Value);

                        foreach (ML.VentaProducto ventaproducto in Objects)
                        {

                            ventaproducto.Venta = new ML.Venta();

                            ventaproducto.Venta.IdVenta = venta.IdVenta;
                            BL.VentaProducto.AddSP(ventaproducto);
                        }


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

        public static ML.Result AddEF(ML.Venta venta, List<object> Objects)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    System.Data.Entity.Core.Objects.ObjectParameter OutputVenta = new System.Data.Entity.Core.Objects.ObjectParameter("idVenta", typeof(Int32));

                    var query = context.VentaAdd(OutputVenta, venta.Cliente.IdCliente, venta.Total, venta.Metodopago.IdMetodoPago);

                    if (query > 0)
                    {
                        venta.IdVenta = Convert.ToUInt16(OutputVenta.Value);

                        foreach (ML.VentaProducto ventaproducto in Objects)
                        {
                            ventaproducto.Venta = new ML.Venta();
                            ventaproducto.Venta.IdVenta = Convert.ToUInt16(OutputVenta.Value);
                            BL.VentaProducto.AddEF(ventaproducto);
                        }
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
