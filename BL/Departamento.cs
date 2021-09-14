using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Departamento
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
                        string Query = "SELECT IdDepartamento,Nombre,IdArea FROM Departamento";

                        cmd.Connection = context;
                        cmd.CommandText = Query;
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tabledepartamento = new DataTable();
                        da.Fill(tabledepartamento);
                        if (tabledepartamento.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tabledepartamento.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();

                                departamento.IdDepartamento = int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();

                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = int.Parse(row[2].ToString());
                                

                                result.Objects.Add(departamento);
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
                        cmd.CommandText = "DepartamentoGetAll";
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tabledepartamento = new DataTable();
                        da.Fill(tabledepartamento);
                        if (tabledepartamento.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tabledepartamento.Rows)
                            {
                                ML.Departamento departamento = new ML.Departamento();

                                departamento.IdDepartamento = int.Parse(row[0].ToString());
                                departamento.Nombre = row[1].ToString();

                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = int.Parse(row[2].ToString());


                                result.Objects.Add(departamento);
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var departamentos = context.DepartamentoGetAll().ToList();
                    result.Objects = new List<object>();

                    if(departamentos != null)
                        {
                            foreach (var obj in departamentos)
                            {
                                ML.Departamento departamento = new ML.Departamento();

                                departamento.IdDepartamento = obj.IdDepartamento;
                                departamento.Nombre = obj.DepartamentoNombre;
                                departamento.Area = new ML.Area();
                                departamento.Area.IdArea = obj.IdArea.Value;
                                
                                result.Objects.Add(departamento);

                                result.Correct = true;
                            }
                        }
                    else 
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los departamentos";
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

        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "INSERT INTO [Departamento]([Nombre],[IdProveedor])VALUES(@Nombre,@IdArea)";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = Query;

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = departamento.Nombre;

                        collection[1] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[1].Value = departamento.Area.IdArea;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del departamento";
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

        public static ML.Result Delete(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "DELETE FROM Departamento WHERE IdDepartamento = @IdDepartamento";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = Query;

                        cmd.Parameters.AddWithValue("@IdDepartamento", departamento.IdDepartamento);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del Departamento";
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

        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string Query = "Update [Departamento]SET[Nombre]=@Nombre,[IdArea]=@IdArea WHERE IdDepartamento=@IdDepartamento";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;

                        cmd.CommandText = Query;

                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = departamento.Nombre;

                        collection[4] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[4].Value = departamento.Area.IdArea;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la modificacion del Departamento";
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

        public static ML.Result AddSP(ML.Departamento departamento)
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
                        cmd.CommandText = "DepartamentoAdd";

                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = departamento.Nombre;

                        collection[1] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[1].Value = departamento.Area.IdArea;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del departamento";
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

        public static ML.Result AddEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    
                    var query = context.DepartamentoAdd(departamento.Nombre, departamento.Area.IdArea);

                    if (query  >= 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al realizar la insercción del departamento";
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

        public static ML.Result DeleteSP(ML.Departamento departamento)
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
                        cmd.CommandText = "DepartamentoDelete";

                        cmd.Parameters.AddWithValue("@IdDepartamento", departamento.IdDepartamento);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al realizar la insercción del Departamento";
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

        public static ML.Result DeleteEF(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = context.DepartamentoDelete(IdDepartamento);
                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el departamento";
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

        public static ML.Result UpdateSP(ML.Departamento departamento)
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
                        cmd.CommandText = "DepartamentoUpdate";

                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("IdDepartamento", SqlDbType.Int);
                        collection[0].Value = departamento.IdDepartamento;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = departamento.Nombre;

                        collection[4] = new SqlParameter("IdArea", SqlDbType.Int);
                        collection[4].Value = departamento.Area.IdArea;

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
                            result.ErrorMessage = "Ocurrió un error al realizar la modificacion del Departamento";
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

        public static ML.Result UpdateEF(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var updateresult = context.DepartamentoUpdate(departamento.IdDepartamento,departamento.Nombre,departamento.Area.IdArea);

                    if(updateresult > 0)
                    {
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

        public static ML.Result AddEFLinq(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    DL_EF.Departamento departamentos = new DL_EF.Departamento();

                    departamentos.Nombre = departamento.Nombre;                    
                    departamentos.Area.IdArea = departamento.Area.IdArea;

                    context.Departamento.Add(departamentos);
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

        public static ML.Result DeleteEFLinq(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var query = (from departamentos in context.Departamento
                                 where departamentos.IdDepartamento == departamento.IdDepartamento
                                 select departamentos).First();
                    context.Departamento.Remove(query);
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

        public static ML.Result UpdateEFLinq(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {

                    var query = (from departamentoDL in context.Departamento
                                 where departamentoDL.IdDepartamento == departamento.IdDepartamento
                                 select departamentoDL).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = departamento.Nombre;
                        query.IdArea = departamento.Area.IdArea;

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

        public static ML.Result GetAllEFLinq()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var departamentos = context.DepartamentoGetAll().ToList();
                    result.Objects = new List<object>();

                    if (departamentos != null)
                    {
                        foreach (var obj in departamentos)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.AreaNombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;

                            result.Objects.Add(departamento);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los departamentos";
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

        public static ML.Result GetByIdEF(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var departamentoDL = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();
                    result.Object = new object();

                    if (departamentoDL != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();
                       
                        departamento.IdDepartamento = departamentoDL.IdDepartamento;
                        departamento.Nombre = departamentoDL.Nombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = departamentoDL.IdArea.Value;

                        result.Object = departamento;
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

        public static ML.Result GetByIdArea(int IdArea)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var departamentoDL = context.AreaGetById(IdArea).ToList();
                    result.Objects = new List<object>();

                    if (departamentoDL != null)
                    {
                        foreach (var obj in departamentoDL)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            result.Objects.Add(departamento);

                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los departamentos";
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
