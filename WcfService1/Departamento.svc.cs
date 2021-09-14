using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Departamento" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Departamento.svc or Departamento.svc.cs at the Solution Explorer and start debugging.
    public class Departamento : IDepartamento
    {
        public void DoWork()
        {

        }
        public Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.AddEF(departamento);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }

        public Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.UpdateEF(departamento);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }

        public Result Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.DeleteEF(IdDepartamento);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex };
        }

        public Result GetAll()
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.GetAllEF();

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex, Objects=result.Objects };
        }

        public Result GetById(int IdDepartamento)
        {
            ML.Result result = new ML.Result();
            result = BL.Departamento.GetByIdEF(IdDepartamento);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, ex = result.Ex, Object = result.Object };
        }
    }
}
