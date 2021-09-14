using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //ServiceReference1.Service1Client nombre = new ServiceReference1.Service1Client();
            //var resultado = nombre.Nombre("misael");

            //ServiceReferenceDepartamento.DepartamentoClient departamentoadd = new ServiceReferenceDepartamento.DepartamentoClient();          

            //var resultado = departamentoadd.AddDepartamento(departamento);


            
            
            //int opcion;
            //Console.WriteLine("Selecciona el obejto que deseas hacer una accion");
            //Console.WriteLine("1.-Productos");
            //Console.WriteLine("2.-Departamentos");
            //opcion = int.Parse(Console.ReadLine());

            //if (opcion == 1)
            //{
            //    Console.WriteLine("Selecciona la accion que desea hacer con producto");
            //    Console.WriteLine("1.-Anadir");
            //    Console.WriteLine("2.-Eliminar");
            //    Console.WriteLine("3.-Modificar");
            //    opcion = int.Parse(Console.ReadLine());
            //    switch(opcion){
            //        case 1:
            //          PL.Producto.Add();
            //          break;

            //        case 2:
            //            PL.Producto.Delete();
            //            break;

            //        case 3:
            //            PL.Producto.Delete();
            //            break;

            //    }
            //}

            //else if(opcion==2)
            //{
            //    Console.WriteLine("Selecciona la accion que desea hacer con departamento");
            //    Console.WriteLine("1.-Anadir");
            //    Console.WriteLine("2.-Eliminar");
            //    Console.WriteLine("3.-Modificar");
            //    opcion = int.Parse(Console.ReadLine());
            //    switch(opcion){
            //        case 1:
            //          PL.Departamento.Add();
            //          break;

            //        case 2:
            //          PL.Departamento.Delete();
            //            break;

            //        case 3:
            //          PL.Departamento.Delete();
            //          break;

            //    }
            //}

            //PL.Producto.Add();

            //PL.Producto.Delete();

            //PL.Producto.Update();

            //PL.Producto.GetAll();

            PL.Departamento.GetAll();

            //PL.Departamento.Add();

            //PL.Departamento.Delete();

            //PL.Departamento.Update();

            //PL.Venta.Add();

            //BL.Producto.GetByIdEF(4);

            //PL.VentaProducto.Add();

            //BL.Departamento.AddEF();

            //BL.Departamento.GetByIdArea(1);
        }
    }
}
