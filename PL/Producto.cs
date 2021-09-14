using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class Producto
    {
        public static void Add()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("Ingrese el nombre del Producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Precio");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese si hay stock");
            producto.Stock = byte.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el id del Proveedor");
            producto.proveedor = new ML.Proveedor();
            producto.proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el id del Departamento");
            producto.departamento = new ML.Departamento();
            producto.departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la Descripcion del Producto");
            producto.Descripcion = Console.ReadLine();

            ML.Result result = BL.Producto.AddEF(producto);

            if (result.Correct)
            {
                Console.WriteLine("El Producto se insertó correctamente");
            }
            else
            {
                Console.WriteLine("El Producto no pudo ser insertado correctamente "+ result.ErrorMessage);
            }


        }

        //public static void Delete()
        //{
        //    ML.Producto producto = new ML.Producto();

        //    Console.WriteLine("Ingrese el id del producto que desee eliminar");
        //    producto.IdProducto = int.Parse(Console.ReadLine());

        //    ML.Result result = BL.Producto.DeleteEF(producto);

        //    if (result.Correct)
        //    {
        //        Console.WriteLine("El Producto se elimino correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine("El Producto no pudo ser eliminado correctamente " + result.ErrorMessage);
        //    }

        //}

        public static void Update()
        {
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("Ingrese el Id del Producto que desea modificar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo nombre del Producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el nuevo Precio");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese si hay stock");
            producto.Stock = byte.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo id del Proveedor");
            producto.proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el nuevo id del Departamento");
            producto.departamento = new ML.Departamento();
            producto.departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la nuevo Descripcion del Producto");
            producto.Descripcion = Console.ReadLine();

            ML.Result result = BL.Producto.Update(producto);

            if (result.Correct)
            {
                Console.WriteLine("El Producto se actualizo correctamente");
            }
            else
            {
                Console.WriteLine("El Producto no pudo ser actualizado correctamente " + result.ErrorMessage);
            }


        }

        public static void GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            foreach (ML.Producto producto in result.Objects)
            {
                Console.WriteLine("IdProducto   " + producto.IdProducto);
                Console.WriteLine("Nombre   " + producto.Nombre);
                Console.WriteLine("PrecioUnitario   " + producto.PrecioUnitario);
                Console.WriteLine("Stock    " + producto.Stock);
                Console.WriteLine("IdProveedor  " + producto.proveedor.IdProveedor);
                Console.WriteLine("IdDepartamento   " + producto.departamento.IdDepartamento);
                Console.WriteLine("Descripcion  " + producto.Descripcion);
                Console.WriteLine(" ");
            }
            
        }


    }
}
