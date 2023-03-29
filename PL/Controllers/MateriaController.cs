using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Materia materia = new ML.Materia();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    string urlApi = "http://localhost:60141/";
                    client.BaseAddress = new Uri(urlApi);

                    var responseTask = client.GetAsync("Api/Materia/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Materia resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }
                    }
                    materia.Materias= result.Objects;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(materia);
            //ML.Result result = BL.Usuario.GetAll(usuario);
            //if (result.Correct)
            //{
            //    usuario.Usuarios = result.Objects;
            //    return View(usuario);
            //}
            //else
            //{
            //    return View(usuario);
            //}
        }

        [HttpGet]
        public ActionResult Form(int? idMateria)
        {
            
            ML.Materia materia = new ML.Materia();
            if (idMateria == null)//agregar
            {
                return View(materia);//enviar usuario
            }
            else//actualizar
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = "http://localhost:60141/";
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Api/Materia/GetById/" + idMateria);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Materia resultItemList = new ML.Materia();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
                catch (Exception ex)
                {

                    result.Correct = false;
                    result.Message = ex.Message;
                }
                if (result.Correct)
                {
                    materia = (ML.Materia)result.Object;

                    return View(materia);
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

            }
        }

        [HttpPost]

        public ActionResult Form(ML.Materia materia)
        {
            
            //ML.Result result = new ML.Result();
            if (materia.IdMateria == 0)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60141/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Materia>("Api/Materia/Add", materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Materia Agregada correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al agregar la materia";
                        return PartialView("Modal");
                    }
                }
                //result = BL.Materia.AddEF(materia);
                //ViewBag.Message = result.Message;
                //return View("Modal");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:60141/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Materia>("Api/Materia/Update", materia);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Materia Actualizada correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "Error al agregar la materia";
                        return PartialView("Modal");
                    }
                }
                //return View(materia);
                //result = BL.Materia.UpdateEF(materia);
                //ViewBag.Message = result.Message;
                //return View("Modal");
            }

        }

        [HttpGet]
        public ActionResult Delete(int? idMateria)
        {
            
            if (idMateria == null)
            {
                return View();
            }
            else
            {
                ML.Result result = new ML.Result();
                try
                {
                    using (var client = new HttpClient())
                    {
                        string urlApi = "http://localhost:60141/";
                        client.BaseAddress = new Uri(urlApi);

                        var responseTask = client.GetAsync("Api/Materia/Delete/" + idMateria);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Materia resultItemList = new ML.Materia();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Materia>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                        }

                    }
                }
                catch (Exception ex)
                {

                    result.Correct = false;
                    result.Message = ex.Message;
                }



                ViewBag.Message = result.Message;
                return View("Modal");

            }
        }
    }
}