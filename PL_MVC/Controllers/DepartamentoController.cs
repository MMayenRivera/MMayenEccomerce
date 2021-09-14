using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;



namespace PL_MVC.Controllers
{
    public class DepartamentoController : Controller
    {
        //
        // GET: /Departamento/
        [HttpGet]       
        public ActionResult GetAll()
        {
            //ML.Result result = BL.Departamento.GetAllEF();

            //ML.Departamento departamento = new ML.Departamento();
            //departamento.Departamentos = result.Objects;

            //return View(departamento);
            ML.Result result = new ML.Result();
            ML.Departamento departamento = new ML.Departamento();
            departamento.Departamentos = new List<object>();
            //ML.Result resultdepartamento = new ML.Result();
            //resultdepartamento.Objects = new List<object>();
            try
            {
                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var responsetask = client.GetAsync("departamento");
                    responsetask.Wait();

                    var resulttask = responsetask.Result;

                    if(resulttask.IsSuccessStatusCode)
                    {
                        var readtask = resulttask.Content.ReadAsAsync<ML.Result>();
                        readtask.Wait();

                        

                        foreach(var resultitem in readtask.Result.Objects)
                        {
                            ML.Departamento resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultitem.ToString());

                            departamento.Departamentos.Add(resultItemList);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return View(departamento);

        }

        [HttpGet]
        public ActionResult Form(int? IdDepartamento) //Add , Update
        {
            ML.Departamento departamento = new ML.Departamento();
            ML.Result resultareas = BL.Area.GetAllEF();

            departamento.Area = new ML.Area();
            departamento.Area.Areas = resultareas.Objects;
            ML.Result result = new ML.Result();


            if (IdDepartamento == null)//Add
            {               
                return View(departamento);
            }
            else //Update
            {
                //ML.Result result = BL.Departamento.GetByIdEF(IdDepartamento.Value);
                
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:10026/api/");

                        var responsetask = client.GetAsync("departamento/"+IdDepartamento);
                        responsetask.Wait();

                        var resulttask = responsetask.Result;

                        if (resulttask.IsSuccessStatusCode)
                        {
                            var readtask = resulttask.Content.ReadAsAsync<ML.Result>();
                            readtask.Wait();

                            var resultitem = readtask.Result.Object;
                            ML.Departamento resultItem = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Departamento>(resultitem.ToString());

                            departamento.Nombre = resultItem.Nombre;
                            departamento.IdDepartamento = resultItem.IdDepartamento;
                            departamento.Area.IdArea = resultItem.Area.IdArea;  
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;
                }
                return View(departamento);
                //if (result.Correct)
                //{                  
                //    departamento.IdDepartamento = ((ML.Departamento)result.Object).IdDepartamento;
                //    departamento.Nombre = ((ML.Departamento)result.Object).Nombre;
                //    departamento.Area.IdArea = ((ML.Departamento)result.Object).Area.IdArea;
                //    departamento.Area.Nombre = ((ML.Departamento)result.Object).Area.Nombre;
                    
                //    return View(departamento);
                //}
                //else
                //{
                //    ViewBag.Message = result.ErrorMessage;
                //    return PartialView("Modal");
                //}
            }
        }

        [HttpPost] 
        public ActionResult Form(ML.Departamento departamento)
        {
            
            ML.Result result = new ML.Result();
            ServiceReferenceDepartamento.DepartamentoClient SDepartamento = new ServiceReferenceDepartamento.DepartamentoClient();

            if (departamento.IdDepartamento == 0)//Add
            {
                //result = BL.Departamento.AddEF(departamento);
                //var resultado = SDepartamento.Add(departamento);                
                //result.Correct = resultado.Correct;
                //result.ErrorMessage = resultado.ErrorMessage;

                using(var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var posttask = client.PostAsJsonAsync<ML.Departamento>("departamento", departamento);
                    posttask.Wait();

                    var resulthttp = posttask.Result;

                    if(resulthttp.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }


                ViewBag.Message = "El departamento se agregó correctamente ";
            }
            else //Update
            {
                //result = BL.Departamento.UpdateEF(departamento); 
                //var resultado = SDepartamento.Add(departamento);
                //result.Correct = resultado.Correct;
                //result.ErrorMessage = resultado.ErrorMessage;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:10026/api/");

                    var puttask = client.PutAsJsonAsync<ML.Departamento>("departamento", departamento);
                    puttask.Wait();

                    var resulthttp = puttask.Result;

                    if (resulthttp.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                }
                ViewBag.Message = "La departamento se actualizó correctamente ";
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente el departamento " + result.ErrorMessage;
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdDepartamento)
        {
            //ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);
            //ServiceReferenceDepartamento.DepartamentoClient SDepartamento = new ServiceReferenceDepartamento.DepartamentoClient();
            ML.Result result = new ML.Result();

            //var resultado = SDepartamento.Delete(IdDepartamento);
            //result.Correct = resultado.Correct;
            //result.ErrorMessage = resultado.ErrorMessage;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:10026/api/");

                var posttask = client.DeleteAsync("departamento/"+IdDepartamento);
                posttask.Wait();

                var resulthttp = posttask.Result;

                if (resulthttp.IsSuccessStatusCode)
                {
                    result.Correct = true;
                }
            }


            return RedirectToAction("GetAll");
        }
	}
}