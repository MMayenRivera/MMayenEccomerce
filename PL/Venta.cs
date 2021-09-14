using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Venta
    {
        //public static void Add()
        //{
        //    ML.Venta venta = new ML.Venta();

        //    Console.WriteLine("Ingrese el Id del cliente");
        //    venta.Cliente = new ML.Cliente();
        //    venta.Cliente.IdCliente = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingrese el total");
        //    venta.Total = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Ingrese el id del metodo de pago");
        //    venta.Metodopago = new ML.MetodoPago();
        //    venta.Metodopago.IdMetodoPago = int.Parse(Console.ReadLine());

        //    //Fecha

        //    ML.Result result = BL.Venta.AddSP(venta);

        //    if (result.Correct)
        //    {
        //        Console.WriteLine("La venta se insertó correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine("La venta no pudo ser insertado correctamente " + result.ErrorMessage);
        //    }


        //}

        public static void Add()
        {
            ML.Venta venta = new ML.Venta();


            Console.WriteLine("Ingrese el Id del cliente");
            venta.Cliente = new ML.Cliente();
            venta.Cliente.IdCliente = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el id del metodo de pago");
            venta.Metodopago = new ML.MetodoPago();
            venta.Metodopago.IdMetodoPago = int.Parse(Console.ReadLine());
            Console.WriteLine("Productos disponibles:");
            PL.Producto.GetAll();
            Console.WriteLine("Selecciona los productos que desees comprar");

            int respuesta = 1;
            venta.Total = 0;

            ML.Result result = new ML.Result();
            result.Objects = new List<object>();

            
            while (respuesta == 1)
            {

                ML.VentaProducto ventaproducto = new ML.VentaProducto();
                ventaproducto.Producto = new ML.Producto();

                Console.WriteLine("Escribe el id del articulo que deseas:");
                ventaproducto.Producto.IdProducto = int.Parse(Console.ReadLine());

                Console.WriteLine("Escribe la cantidad del articulo que deseas:");
                ventaproducto.Cantidad = int.Parse(Console.ReadLine());

                result.Objects.Add(ventaproducto);

                ML.Result resultproducto = BL.Producto.GetById(ventaproducto.Producto.IdProducto);

                decimal SubTotal = ventaproducto.Cantidad * ((ML.Producto)resultproducto.Object).PrecioUnitario;

                venta.Total += SubTotal;

                Console.WriteLine("Si deseeas comprar otro producto presiona 1 si no presiona 2.");
                respuesta = int.Parse(Console.ReadLine());
            }

            BL.Venta.AddEF(venta, result.Objects);


        }
    }
}
