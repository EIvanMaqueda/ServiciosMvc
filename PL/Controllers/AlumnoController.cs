using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    { 
        // GET: Alumno
        public ActionResult GetAll()
        {
           AlumnoService.AlumnoServiceClient alumnoservice=new AlumnoService.AlumnoServiceClient();
            ML.Result result = alumnoservice.GetAll();
            ML.Alumno alumno = new ML.Alumno();
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                return View(alumno);
            }
        }

        [HttpGet]
        public ActionResult Form(int? idAlumno)
        {
            AlumnoService.AlumnoServiceClient alumnoservice = new AlumnoService.AlumnoServiceClient();
            ML.Alumno alumno = new ML.Alumno();
            if (idAlumno == null)//agregar
            {
                return View(alumno);//enviar usuario
            }
            else//actualizar
            {
                ML.Result result = alumnoservice.GetById(idAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                   
                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

            }
        }

        [HttpPost]

        public ActionResult Form(ML.Alumno alumno)
        {
            AlumnoService.AlumnoServiceClient alumnoservice = new AlumnoService.AlumnoServiceClient();
            ML.Result result = new ML.Result();
            if (alumno.IdAlumno == 0)
            {
                result = alumnoservice.Add(alumno);
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                result = alumnoservice.Update(alumno);
                ViewBag.Message = result.Message;
                return View("Modal");
            }

        }

        [HttpGet]
        public ActionResult Delete(int? idAlumno)
        {
            AlumnoService.AlumnoServiceClient alumnoservice = new AlumnoService.AlumnoServiceClient();
            if (idAlumno == null)
            {
                return View();
            }
            else
            {
                ML.Result result = alumnoservice.Delete(idAlumno.Value);

               
                    ViewBag.Message = result.Message;
                    return View("Modal");
                
            }
        }
    }
}