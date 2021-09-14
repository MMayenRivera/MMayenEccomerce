using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    class VentaProducto
    {
        public static void Add()
        {
            ML.VentaProducto ventaproducto = new ML.VentaProducto();

            Console.WriteLine("Ingrese el Id de la venta");
            ventaproducto.Venta = new ML.Venta();
            ventaproducto.Venta.IdVenta = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la cantidad");
            ventaproducto.Cantidad = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el id del producto");
            ventaproducto.Producto = new ML.Producto();
            ventaproducto.Producto.IdProducto = int.Parse(Console.ReadLine());

            ML.Result result = BL.VentaProducto.AddSP(ventaproducto);

            if (result.Correct)
            {
                Console.WriteLine("La ventaproducto se insertó correctamente");
            }
            else
            {
                Console.WriteLine("El ventaproducto no pudo ser insertado correctamente " + result.ErrorMessage);
            }


        }
    }
}
