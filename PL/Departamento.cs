using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Departamento
    {
        public static void Add()
        {
            ML.Departamento departamento = new ML.Departamento();
            ServiceReferenceDepartamento.DepartamentoClient departamentoservice = new ServiceReferenceDepartamento.DepartamentoClient();
            ML.Result result = new ML.Result();
            

            Console.WriteLine("Ingrese el nombre del Departamento");
            departamento.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el id del Area");
            departamento.Area = new ML.Area();
            departamento.Area.IdArea = int.Parse(Console.ReadLine());

            

            //ML.Result result = BL.Departamento.AddEFLinq(departamento);
            var resultado = departamentoservice.Add(departamento);

            result.Correct = resultado.Correct;

            if (result.Correct)
            {
                Console.WriteLine("El Departamento se insertó correctamente");
            }
            else
            {
                Console.WriteLine("El Departamento no pudo ser insertado correctamente "+ result.ErrorMessage);
            }


        }

        public static void Delete()
        {
            ML.Departamento departamento = new ML.Departamento();
            ServiceReferenceDepartamento.DepartamentoClient departamentoservice = new ServiceReferenceDepartamento.DepartamentoClient();
            ML.Result result = new ML.Result();

            Console.WriteLine("Ingrese el id del Departamento que desee eliminar");
            departamento.IdDepartamento = int.Parse(Console.ReadLine());

            var resultado = departamentoservice.Delete(departamento.IdDepartamento);

            if (result.Correct)
            {
                Console.WriteLine("El Departamento se elimino correctamente");
            }
            else
            {
                Console.WriteLine("El Departamento no pudo ser eliminado correctamente " + result.ErrorMessage);
            }

        }

        public static void Update()
        {
            ML.Departamento departamento = new ML.Departamento();
            ServiceReferenceDepartamento.DepartamentoClient departamentoservice = new ServiceReferenceDepartamento.DepartamentoClient();          
            ML.Result result = new ML.Result();

            Console.WriteLine("Ingrese el Id del Departamento que desea modificar");
            departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo nombre del Departamento");
            departamento.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo id del Area");
            departamento.Area = new ML.Area();
            departamento.Area.IdArea = int.Parse(Console.ReadLine());

            var resultado = departamentoservice.Update(departamento);
            result.Correct = resultado.Correct;

            if (result.Correct)
            {
                Console.WriteLine("El Departamento se actualizo correctamente");
            }
            else
            {
                Console.WriteLine("El Departamento no pudo ser actualizado correctamente " + result.ErrorMessage);
            }


        }

        public static void GetAll()
        {
            ML.Result result = new ML.Result();
            ServiceReferenceDepartamento.DepartamentoClient departamentoservice = new ServiceReferenceDepartamento.DepartamentoClient();

            var resultado = departamentoservice.GetAll();

            result.Objects = resultado.Objects.ToList();

            foreach (ML.Departamento departamento in result.Objects)
            {
                Console.WriteLine("ID   " + departamento.IdDepartamento);
                Console.WriteLine("Nombre   " + departamento.Nombre);
                Console.WriteLine("Id Area   " + departamento.Area.IdArea);
                
                Console.WriteLine(" ");
            }

        }
    }
}

