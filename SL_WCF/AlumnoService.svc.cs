using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AlumnoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AlumnoService.svc or AlumnoService.svc.cs at the Solution Explorer and start debugging.
    public class AlumnoService : IAlumnoService
    {
        public ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.AddSQLClient(alumno);
            if (result.Correct)
            {
                return result;
            }
            else
            {
                return null;
            }


        }

        public ML.Result GetAll()
        {
            ML.Result result = BL.Alumno.GetAllSQLClient();
            return result;
        }

        public ML.Result GetById(int idAlumno)
        {
            ML.Result result = BL.Alumno.GetByIdSQLClient(idAlumno);
            return result;
        }

        public ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.UpdateSQLClient(alumno);
            return result;
        }

        public ML.Result Delete(int idAlumno)
        {
            ML.Result result = BL.Alumno.DeleteSQLClient(idAlumno);           
                return result;

        }
    }
}
