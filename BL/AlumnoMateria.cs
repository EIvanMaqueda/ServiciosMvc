using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result MateriasGetAllByIdAlumno(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try

            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    var query = context.MateriasGetAllByIdAlumno(idAlumno).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            
                            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
                            alumnoMateria.IdAlumnoMateria = obj.IdAlumnoMateria;
                            alumnoMateria.Alumno = new ML.Alumno();
                            alumnoMateria.Alumno.IdAlumno = obj.IdAlumno.Value;
                            alumnoMateria.Alumno.Nombre = obj.AlumnoNombre;
                            alumnoMateria.Alumno.ApellidoPaterno = obj.ApellidoPaterno;
                            alumnoMateria.Alumno.ApellidoMaterno = obj.ApellidoMaterno;
                            alumnoMateria.Materia = new ML.Materia();
                            alumnoMateria.Materia.IdMateria = obj.IdMateria.Value;
                            alumnoMateria.Materia.Nombre = obj.MateriaNombre;
                            alumnoMateria.Materia.Costo = obj.Costo.Value;

                            result.Objects.Add(alumnoMateria);
                        }
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: " + ex.Message;

            }
            return result;
        }

        public static ML.Result Delete(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    
                    int query = context.AlumnoMateriaDelete(idMateria);
                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "Materia Borrado de la base de datos";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: " + ex.Message;

            }
            return result;
        }

        public static ML.Result GetAllMateriasNoInscritas(int idAlumno)
        {
            ML.Result result = new ML.Result();
            try

            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    var query = context.AlumnoMateriasNoInscritas(idAlumno).ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre = obj.Nombre;
                            materia.Costo = obj.Costo.Value;
                            result.Objects.Add(materia);
                        }
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error: " + ex.Message;

            }
            return result;
        }

        public static ML.Result Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    for (int i = 0; i < alumnoMateria.Materia.Materias.Count; i++)
                    {
                        int query = context.AlumnoMateriaAdd(alumnoMateria.Alumno.IdAlumno, int.Parse(alumnoMateria.Materia.Materias[i].ToString()));

                        if (query >= 1)
                        {
                            result.Correct = true;
                            result.Message = "Materia Ingresada Correctamente";
                        }
                    }
                    //result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al ingresar al usuario: " + ex.Message;
            }
            return result;
        }
    }
}
