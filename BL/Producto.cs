using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Producto
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string Query = "SELECT IdProducto, Nombre, PrecioUnitario, Stock, IdProveedor, IdDepartamento, Descripcion FROM Producto";

                        cmd.Connection = context;
                        cmd.CommandText = Query;
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableproducto = new DataTable();
                        da.Fill(tableproducto);
                        if (tableproducto.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableproducto.Rows)
                            {
                                ML.Producto producto = new ML.Producto();

                                producto.IdProducto = int.Parse(row[0].ToString());
                                producto.Nombre = row[1].ToString();
                                producto.PrecioUnitario = int.Parse(row[2].ToString());
                                producto.Stock = byte.Parse(row[3].ToString());

                                producto.proveedor = new ML.Proveedor();
                                producto.proveedor.IdProveedor = int.Parse(row[4].ToString());

                                producto.departamento = new ML.Departamento();
                                producto.departamento.IdDepartamento = int.Parse(row[5].ToString());

                                producto.Descripcion = row[6].ToString();

                                result.Objects.Add(producto);
                            }
                            result.Correct = true;
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

        public static ML.Result GetAllSP()
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
                        cmd.CommandText = "ProductoGetAll";
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableproducto = new DataTable();
                        da.Fill(tableproducto);
                        if (tableproducto.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableproducto.Rows)
                            {
                                ML.Producto producto = new ML.Producto();

                                producto.IdProducto = int.Parse(row[0].ToString());
                                producto.Nombre = row[1].ToString();
                                producto.PrecioUnitario = int.Parse(row[2].ToString());
                                producto.Stock = byte.Parse(row[3].ToString());

                                producto.proveedor = new ML.Proveedor();
                                producto.proveedor.IdProveedor = int.Parse(row[4].ToString());

                                producto.departamento = new ML.Departamento();
                                producto.departamento.IdDepartamento = int.Parse(row[5].ToString());

                                producto.Descripcion = row[6].ToString();
                            }
                            result.Correct = true;
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

        public static ML.Result GetById(int IdProducto)
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
                        cmd.CommandText = "ProductoGetById";
                        context.Open();

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = IdProducto;

                        cmd.Parameters.AddRange(collection);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableproducto = new DataTable();

                        da.Fill(tableproducto);
                        if (tableproducto.Rows.Count > 0)
                        {
                            DataRow row = tableproducto.Rows[0];

                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = int.Parse(row[0].ToString());
                            producto.Nombre = row[1].ToString();
                            producto.PrecioUnitario = int.Parse(row[2].ToString());
                            producto.Stock = byte.Parse(row[3].ToString());

                            producto.proveedor = new ML.Proveedor();
                            producto.proveedor.IdProveedor = int.Parse(row[4].ToString());

                            producto.departamento = new ML.Departamento();
                            producto.departamento.IdDepartamento = int.Parse(row[5].ToString());

                            producto.Descripcion = row[6].ToString();

                            result.Object = producto;
                            
                            result.Correct = true;
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

        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "INSERT INTO [dbo].[Producto]([Nombre],[PrecioUnitario],[Stock],[IdProveedor],[IdDepartamento],[Descripcion])VALUES(@Nombre,@PrecioUnitario,@Stock,@IdProveedor,@IdDepartamento,@Descripcion)";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = Query;

                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = producto.Nombre;

                        collection[1] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[1].Value = producto.PrecioUnitario;

                        collection[2] = new SqlParameter("Stock", SqlDbType.Bit);
                        collection[2].Value = producto.Stock;

                        collection[3] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[3].Value = producto.proveedor.IdProveedor;

                        collection[4] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[4].Value = producto.departamento.IdDepartamento;

                        collection[5] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[5].Value = producto.Descripcion;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del producto"; 
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

        public static ML.Result Delete(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "DELETE FROM Producto WHERE IdProducto = @IdProducto";


                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = context;
                            cmd.CommandText = Query;

                            cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto); 

                            context.Open();

                            int RowsAffected = cmd.ExecuteNonQuery();

                            if (RowsAffected > 0)
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
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "Update [dbo].[Producto]SET[Nombre]=@Nombre,[PrecioUnitario]=@PrecioUnitario,[Stock]=@Stock,[IdProveedor]=@IdProveedor,[IdDepartamento]=@IdDepartamento,[Descripcion]=@Descripcion WHERE IdProducto=@IdProducto";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = Query;

                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = producto.IdProducto;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = producto.Nombre;

                        collection[2] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[2].Value = producto.PrecioUnitario;

                        collection[3] = new SqlParameter("Stock", SqlDbType.Bit);
                        collection[3].Value = producto.Stock;

                        collection[4] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[4].Value = producto.proveedor.IdProveedor;

                        collection[5] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[5].Value = producto.departamento.IdDepartamento;

                        collection[6] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[6].Value = producto.Descripcion;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la modificacion del producto";
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

        public static ML.Result AddSP(ML.Producto producto)
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
                        cmd.CommandText = "ProductoAdd";

                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = producto.Nombre;

                        collection[1] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[1].Value = producto.PrecioUnitario;

                        collection[2] = new SqlParameter("Stock", SqlDbType.Bit);
                        collection[2].Value = producto.Stock;

                        collection[3] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[3].Value = producto.proveedor.IdProveedor;

                        collection[4] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[4].Value = producto.departamento.IdDepartamento;

                        collection[5] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[5].Value = producto.Descripcion;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del producto";
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

        public static ML.Result DeleteSP(ML.Producto producto)
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
                        cmd.CommandText = "ProductoDelete";

                        cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
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
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static ML.Result UpdateSP(ML.Producto producto)
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
                        cmd.CommandText = "ProductoUpdate";

                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("IdProducto", SqlDbType.Int);
                        collection[0].Value = producto.IdProducto;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = producto.Nombre;

                        collection[2] = new SqlParameter("PrecioUnitario", SqlDbType.Decimal);
                        collection[2].Value = producto.PrecioUnitario;

                        collection[3] = new SqlParameter("Stock", SqlDbType.Bit);
                        collection[3].Value = producto.Stock;

                        collection[4] = new SqlParameter("IdProveedor", SqlDbType.Int);
                        collection[4].Value = producto.proveedor.IdProveedor;

                        collection[5] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[5].Value = producto.departamento.IdDepartamento;

                        collection[6] = new SqlParameter("Descripcion", SqlDbType.VarChar);
                        collection[6].Value = producto.Descripcion;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la modificacion del producto";
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

        public static ML.Result AddEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {

                    var query = context.ProductoAdd(producto.Nombre, producto.PrecioUnitario, producto.Stock, 
                        producto.proveedor.IdProveedor, producto.departamento.IdDepartamento, producto.Descripcion,producto.Imagen);
                        if (query >= 0)
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

        public static ML.Result DeleteEF(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = context.ProductoDelete(IdProducto);

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

        public static ML.Result UpdateEF(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.PrecioUnitario, producto.Stock, 
                        producto.proveedor.IdProveedor, producto.departamento.IdDepartamento, producto.Descripcion, producto.Imagen);

                        if (query > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al realizar la modificacion del producto";
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var productos = context.ProductoGetAll().ToList();
                    result.Objects = new List<object>();

                    if(productos != null)
                    {
                        foreach(var obj in productos)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.ProductoNombre;
                            producto.PrecioUnitario = obj.PrecioUnitario.Value;
                            producto.Stock = obj.Stock.Value;
                            producto.proveedor = new ML.Proveedor();
                            producto.proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.departamento = new ML.Departamento();
                            producto.departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;

                            result.Objects.Add(producto);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los productos";
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

        public static ML.Result GetByIdEF(int IdProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var productosDL = context.ProductoGetById(IdProducto).FirstOrDefault();
                   

                    if (productosDL != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = productosDL.IdProducto;
                        producto.Nombre = productosDL.ProductoNombre;
                        producto.PrecioUnitario = productosDL.PrecioUnitario.Value;
                        producto.Stock = productosDL.Stock.Value;

                        producto.proveedor = new ML.Proveedor();
                        producto.proveedor.IdProveedor = productosDL.IdProveedor.Value;

                        producto.departamento = new ML.Departamento();
                        producto.departamento.IdDepartamento = productosDL.IdDepartamento.Value;
                        producto.Descripcion = productosDL.Descripcion;

                        producto.departamento.Area = new ML.Area();
                        producto.departamento.Area.IdArea = productosDL.IdArea;
                        producto.Imagen = productosDL.Imagen;

                        result.Object = new object();//Revisa si es necesario 

                        result.Object = producto;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar el producto";
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

        public static ML.Result AddEFLinq(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {

                    DL_EF.Producto productos = new DL_EF.Producto();

                    productos.Nombre = producto.Nombre;
                    productos.PrecioUnitario = producto.PrecioUnitario;
                    productos.Stock = producto.Stock;
                    productos.Proveedor.IdProveedor = producto.proveedor.IdProveedor;
                    productos.Departamento.IdDepartamento = producto.departamento.IdDepartamento;
                    productos.Descripcion = producto.Descripcion;

                    context.Producto.Add(productos);
                    context.SaveChanges();

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result DeleteEFLinq(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = (from productos in context.Producto
                                 where productos.IdProducto == producto.IdProducto
                                 select productos).First();
                    context.Producto.Remove(query);
                    context.SaveChanges();

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static ML.Result UpdateEFLinq(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = (from productoDL in context.Producto
                                 where productoDL.IdProducto == producto.IdProducto
                                 select productoDL).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = producto.Nombre;
                        query.PrecioUnitario = producto.PrecioUnitario;
                        query.Stock = producto.Stock;
                        query.Proveedor.IdProveedor = producto.proveedor.IdProveedor;
                        query.Departamento.IdDepartamento = producto.departamento.IdDepartamento;
                        query.Descripcion = producto.Descripcion;

                        context.SaveChanges();

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el registro";
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
