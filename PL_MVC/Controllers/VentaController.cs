using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class VentaController : Controller
    {
        //
        // GET: /Venta/
        public ActionResult ProductoGetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            ML.Producto producto = new ML.Producto();
            producto.Productos = result.Objects;

            return View(producto);
        }

         public ActionResult Carrito()
        {
            return View();
        }

        public ActionResult AddCarrito(int IdProducto)
        {
            ML.Result result = new ML.Result();
            if (Session["Carrito"] == null)
            {
                result.Objects = new List<object>();
                ML.Result resultproducto = new ML.Result();
                ML.VentaProducto ventaproducto = new ML.VentaProducto();
                ventaproducto.Producto = new ML.Producto();

                resultproducto = BL.Producto.GetByIdEF(IdProducto);

                if (resultproducto.Correct == true)
                {
                    ventaproducto.Producto = (ML.Producto)resultproducto.Object;

                    ventaproducto.Cantidad = 1;

                    result.Objects.Add(ventaproducto);

                    Session["Carrito"] = result.Objects;
                }

            }
            
            
            else
            {
                bool IsList = false;
                result.Objects = (List<object>)Session["Carrito"];

                foreach(ML.VentaProducto ventaproducto in result.Objects)
                {                   
                    if (IdProducto == ventaproducto.Producto.IdProducto)
                    {
                        IsList = true;
                    }                   
                }   
       
                if(IsList == true)
                {
                    foreach(ML.VentaProducto ventaproducto in result.Objects)
                    {
                        if(ventaproducto.Producto.IdProducto == IdProducto)
                        {
                            ventaproducto.Cantidad++;
                        }                            
                    }
                }
                else
                {
                    ML.Result resultproducto = new ML.Result();
                    ML.VentaProducto ventaproducto = new ML.VentaProducto();

                    resultproducto = BL.Producto.GetByIdEF(IdProducto);
                    ventaproducto.Producto = (ML.Producto)resultproducto.Object;

                    ventaproducto.Cantidad = 1;

                    result.Objects.Add(ventaproducto);
                }
                Session["Carrito"] = result.Objects;
            }
            return RedirectToAction("Carrito");
        }

        public ActionResult Decrementar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            foreach(ML.VentaProducto ventaproducto in result.Objects)
            {
                if(ventaproducto.Producto.IdProducto == IdProducto)
                {
                    if(ventaproducto.Cantidad > 0)
                    {
                        ventaproducto.Cantidad--;
                    }
                }
            }

            Session["Carrito"] = result.Objects;

            return RedirectToAction("Carrito");
        }

        public ActionResult Incrementar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            foreach (ML.VentaProducto ventaproducto in result.Objects)
            {
                if (ventaproducto.Producto.IdProducto == IdProducto)
                {
                    if(ventaproducto.Cantidad<ventaproducto.Producto.Stock)
                    {
                        ventaproducto.Cantidad++;
                    }   
                    //else no hay mas producto
                }
            }

            Session["Carrito"] = result.Objects;

            return RedirectToAction("Carrito");
        }

        public ActionResult Eliminar(int IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            foreach (ML.VentaProducto ventaproducto in result.Objects)
            {
                if (ventaproducto.Producto.IdProducto == IdProducto)
                {
                    result.Objects.Remove(ventaproducto);
                    break;
                }
            }
            Session["Carrito"] = result.Objects;

            return RedirectToAction("Carrito");
        }

        public ActionResult ProcesarCompra()
        {
            ML.Result result = new ML.Result();
            ML.Venta venta = new ML.Venta();
            result.Objects = new List<object>();
            result.Objects = (List<object>)Session["Carrito"];

            venta.Cliente.IdCliente = 1;
            venta.Total = 1;
            venta.Metodopago.IdMetodoPago = 1;

            BL.Venta.AddEF(venta, result.Objects);
            
            return RedirectToAction("ProductoGetAll");
        }
	}    
}