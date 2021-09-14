using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Area
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.MMayenDataBaseEntities context = new DL_EF.MMayenDataBaseEntities())
                {
                    var areas = context.AreaGetAll().ToList();
                    result.Objects = new List<object>();

                    if (areas != null)
                    {
                        foreach (var obj in areas)
                        {
                            ML.Area area = new ML.Area();

                            area.IdArea = obj.IdArea;
                            area.Nombre = obj.Nombre;

                            result.Objects.Add(area);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar las areas";
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
