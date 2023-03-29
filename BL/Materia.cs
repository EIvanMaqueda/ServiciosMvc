using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Materia
    {
        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    int query = context.MateriasAdd(materia.Nombre,materia.Costo);

                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Materia Agregada Correctamente";
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

        public static ML.Result UpdateEF(ML.Materia materia)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    int query = context.MateriaUpdate(materia.Nombre,materia.Costo,materia.IdMateria);
                    if (query >= 1)
                    {
                        result.Correct = true;
                        result.Message = "Materia Actualizado Correctamente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al actualizar la materia: " + ex.Message;

            }
            return result;

        }

        public static ML.Result DeleteEF(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    int query = context.MateriaDelete(idMateria);
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

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();
            try
               
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    var query = context.MateriaGetAll().ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Materia materia=new ML.Materia();
                            materia.IdMateria = obj.IdMateria;
                            materia.Nombre= obj.Nombre;
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
       
        public static ML.Result GetByIdEF(int idMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.EMaquedaControlEscolarEntities context = new DL.EMaquedaControlEscolarEntities())
                {
                    var query = context.MateriaGetById(idMateria).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Materia materia= new ML.Materia();
                        materia.IdMateria=query.IdMateria;
                        materia.Nombre = query.Nombre;
                        materia.Costo = query.Costo.Value;

                        result.Object = materia;
                    }
                    result.Correct = true;
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error Materia no enecontrada: " + ex.Message;

            }
            return result;
        }

    }
}
