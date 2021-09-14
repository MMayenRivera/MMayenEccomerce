using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        //
        // GET: /Producto/
        public ActionResult GetAll()
        {
            //ML.Result result = BL.Producto.GetAllEF();
            ML.Result result = new ML.Result();
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<object>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var responsetask = client.GetAsync("producto");
                    responsetask.Wait();

                    var resulttask = responsetask.Result;

                    if (resulttask.IsSuccessStatusCode)
                    {
                        var readtask = resulttask.Content.ReadAsAsync<ML.Result>();
                        readtask.Wait();

                        foreach (var resultitem in readtask.Result.Objects)
                        {
                            ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultitem.ToString());

                            producto.Productos.Add(resultItemList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return View(producto);
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto) //Add , Update
        {
            ML.Producto producto = new ML.Producto();
            ML.Result resultproveedores = BL.Proveedor.GetAll();
            ML.Result resultdepartamentos = BL.Departamento.GetAllEF();
            ML.Result resultareas = BL.Area.GetAllEF();
            ML.Result result = new ML.Result();

            producto.proveedor = new ML.Proveedor();
            producto.proveedor.Proveedores = resultproveedores.Objects;

            producto.departamento = new ML.Departamento();
            producto.departamento.Departamentos = resultdepartamentos.Objects;

            producto.departamento.Area = new ML.Area();
            producto.departamento.Area.Areas = resultareas.Objects;

            if (IdProducto == null)//Add
            {
                return View(producto);
            }
            else //Update
            {
                //ML.Result result = BL.Producto.GetByIdEF(IdProducto.Value);

                //if (result.Correct)
                //{
                //    producto.IdProducto = ((ML.Producto)result.Object).IdProducto;
                //    producto.Nombre = ((ML.Producto)result.Object).Nombre;
                //    producto.PrecioUnitario = ((ML.Producto)result.Object).PrecioUnitario;
                //    producto.Stock = ((ML.Producto)result.Object).Stock;

                //    producto.proveedor.IdProveedor = ((ML.Producto)result.Object).proveedor.IdProveedor;
                //    producto.proveedor.Nombre = ((ML.Producto)result.Object).proveedor.Nombre;

                //    producto.departamento.IdDepartamento = ((ML.Producto)result.Object).departamento.IdDepartamento;
                //    producto.departamento.Nombre = ((ML.Producto)result.Object).departamento.Nombre;

                //    producto.Descripcion = ((ML.Producto)result.Object).Descripcion;

                //    producto.departamento.Area.IdArea = ((ML.Producto)result.Object).departamento.Area.IdArea;
                //    producto.departamento.Area.Nombre = ((ML.Producto)result.Object).departamento.Area.Nombre;
                //    producto.Imagen = ((ML.Producto)result.Object).Imagen;

                //    return View(producto);
                //}
                //else
                //{
                //    ViewBag.Message = result.ErrorMessage;
                //    return PartialView("Modal");
                //}

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:10026/api/");

                        var responsetask = client.GetAsync("producto/" + IdProducto);
                        responsetask.Wait();

                        var resulttask = responsetask.Result;

                        if (resulttask.IsSuccessStatusCode)
                        {
                            var readtask = resulttask.Content.ReadAsAsync<ML.Result>();
                            readtask.Wait();

                            var resultitem = readtask.Result.Object;
                            ML.Producto resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultitem.ToString());

                            producto.Nombre = resultItem.Nombre;
                            producto.PrecioUnitario = resultItem.PrecioUnitario;
                            producto.Imagen = resultItem.Imagen;
                            producto.Descripcion = resultItem.Descripcion;
                            producto.Stock = resultItem.Stock;
                            producto.departamento.IdDepartamento = resultItem.departamento.IdDepartamento;
                            producto.proveedor.IdProveedor = resultItem.proveedor.IdProveedor;
                            producto.departamento.Area.IdArea = resultItem.departamento.Area.IdArea;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                return View(producto);

            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {

            ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];
            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }

            if (producto.IdProducto == 0)//Add
            {
                ServiceReferenceProducto.ProductoClient Sproducto = new ServiceReferenceProducto.ProductoClient();

                //result = BL.Producto.AddEF(producto);

                //var resultado = Sproducto.Add(producto);
                //result.Correct = resultado.Correct;
                //result.ErrorMessage = resultado.ErrorMessage;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var posttask = client.PostAsJsonAsync<ML.Producto>("producto", producto);
                    posttask.Wait();

                    var resulthttp = posttask.Result;

                    if (resulthttp.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }

                ViewBag.Message = "El producto se agregó correctamente ";
            }
            else //Update
            {
                //result = BL.Producto.UpdateEF(producto);
                //ServiceReferenceProducto.ProductoClient Sproducto = new ServiceReferenceProducto.ProductoClient();

                //var resultado = Sproducto.Update(producto);
                //result.Correct = resultado.Correct;
                //result.ErrorMessage = resultado.ErrorMessage;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var puttask = client.PutAsJsonAsync<ML.Producto>("producto", producto);
                    puttask.Wait();

                    var resulthttp = puttask.Result;

                    if (resulthttp.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }
                ViewBag.Message = "La producto se actualizó correctamente ";
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente el producto " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            //ML.Result result = BL.Producto.DeleteEF(IdProducto);
            //ServiceReferenceProducto.ProductoClient Sproducto = new ServiceReferenceProducto.ProductoClient();
            ML.Result result = new ML.Result();

            //var resultado = Sproducto.Delete(IdProducto);
            //result.Correct = resultado.Correct;
            //result.ErrorMessage = resultado.ErrorMessage;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:10026/api/");

                var posttask = client.DeleteAsync("producto/" + IdProducto);
                posttask.Wait();

                var resulthttp = posttask.Result;

                if (resulthttp.IsSuccessStatusCode)
                {
                    result.Correct = true;
                }
            }

            return RedirectToAction("GetAll");
        }

        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Departamento.GetByIdArea(IdArea);
            List<SelectListItem> opciones = new List<SelectListItem>();

            opciones.Add(new SelectListItem { Text = "--Seleccione una opcion--", Value = "0" });
            if(result.Objects != null)
            {
                foreach(ML.Departamento departamento in result.Objects)
                {
                    opciones.Add(new SelectListItem { Text = departamento.Nombre.ToString(), Value = departamento.IdDepartamento.ToString() });
                }
            }
            return Json(new SelectList(opciones, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
	}
}