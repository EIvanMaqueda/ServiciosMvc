using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        // GET: AlumnoMateria
        public ActionResult GetAllAlumnos()
        {
            ML.Alumno alumno= new ML.Alumno();
            ML.Result result = BL.Alumno.GetAllSQLClient();
           
            alumno.Alumnos = result.Objects;
           
            return View(alumno);
        }

        public ActionResult GetAllMateriasAlumnos(int idAlumno) {
            ML.AlumnoMateria alumnoMateria=new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            ML.Result result=new ML.Result();
            if (idAlumno != 0)
            {
                
                alumnoMateria.Alumno.IdAlumno = idAlumno;
                result = BL.AlumnoMateria.MateriasGetAllByIdAlumno(idAlumno);
                Session["id"] = idAlumno;
            }
            else
            {
                alumnoMateria.Alumno.IdAlumno = int.Parse(Session["id"].ToString());
                result = BL.AlumnoMateria.MateriasGetAllByIdAlumno(alumnoMateria.Alumno.IdAlumno);
            }
            if (result.Objects==null)
            {
                alumnoMateria = (ML.AlumnoMateria)result.Objects[0];
            }
            else {
                ML.Result resultAlumno = BL.Alumno.GetByIdSQLClient(alumnoMateria.Alumno.IdAlumno);
                alumnoMateria.Alumno =(ML.Alumno) resultAlumno.Object;
            }
            
           
            alumnoMateria.AlumnosMaterias=result.Objects;
           
            
            

            return View(alumnoMateria);
        }

        public ActionResult Delete(int idAlumnoMateria) {
            ML.Result result = BL.AlumnoMateria.Delete(idAlumnoMateria);
            ViewBag.Message = result.Message;
            ViewBag.Location = "GetAllMateriasAlumnos?idAlumno=" + (int)Session["id"];
            return PartialView("Modal");
        }


        public ActionResult GetAllMateriasNot()
        {
            int id = (int)Session["id"];
            ML.Result result=BL.AlumnoMateria.GetAllMateriasNoInscritas(id);
            ML.Materia materia= new ML.Materia();
            materia.Materias = result.Objects;
            return View(materia);
        }

        [HttpPost]
        public ActionResult AddMaterias(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
           
              ML.AlumnoMateria alumnoMateria=new ML.AlumnoMateria();
              //materia.IdMateria = int.Parse(materia.Materias[i].ToString());
              alumnoMateria.Alumno=new ML.Alumno();
              //alumnoMateria.Materia = new ML.Materia();
              alumnoMateria.Materia = materia;
              alumnoMateria.Alumno.IdAlumno= (int)Session["id"];
              result = BL.AlumnoMateria.Add(alumnoMateria);
            
            ViewBag.Message = result.Message;
            ViewBag.Location = "GetAllMateriasAlumnos?idAlumno=" + (int)Session["id"];
            return PartialView("Modal");
        }
    }
}