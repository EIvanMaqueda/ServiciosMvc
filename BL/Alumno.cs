using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAllSQLClient()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Getconection()))
                {
                    string query = "AlumnoGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in dataTable.Rows)
                            {
                               ML.Alumno alumno=new ML.Alumno();
                                alumno.IdAlumno = (int)row[0];
                                alumno.Nombre = row[1].ToString();
                                alumno.ApellidoPaterno = row[2].ToString();
                                alumno.ApellidoMaterno = row[3].ToString();
                               

                                result.Objects.Add(alumno);
                            }
                        }
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public static ML.Result GetByIdSQLClient(int Id)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Getconection()))
                {
                    string query = "AlumnoGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@idAlumno", SqlDbType.Int);
                        collection[0].Value = Id;
                        cmd.Parameters.AddRange(collection);

                        DataTable dataTable = new DataTable();
                        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

                        sqlDataAdapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            DataRow row = dataTable.Rows[0];

                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = (int)row[0];
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();
                           

                            result.Object = alumno;

                        }
                        result.Correct = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return result;

        }

        public static ML.Result AddSQLClient(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var context = new SqlConnection(DL.Conexion.Getconection()))
                {
                    string query = "AlumnoAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("nombre", SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;

                        collection[1] = new SqlParameter("apellidopaterno", SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;

                        collection[2] = new SqlParameter("apellidomaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoMaterno;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                            result.Message = "usuario agregado correctamente";
                        }


                    }
                }
                {


                }

            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.Message = "Ocurrio un problema al ingresar el usario: " + ex;
            }
            return result;
        }

        public static ML.Result UpdateSQLClient(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var context = new SqlConnection(DL.Conexion.Getconection()))
                {
                    string query = "AlumnoUpdate";
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("nombre", SqlDbType.VarChar);
                        collection[0].Value = alumno.Nombre;
                        collection[1] = new SqlParameter("apellidopaterno", SqlDbType.VarChar);
                        collection[1].Value = alumno.ApellidoPaterno;
                        collection[2] = new SqlParameter("apellidomaterno", SqlDbType.VarChar);
                        collection[2].Value = alumno.ApellidoMaterno;
                        collection[3] = new SqlParameter("idalumno", SqlDbType.Int);
                        collection[3].Value =alumno.IdAlumno;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int rowsaffected = cmd.ExecuteNonQuery();
                        if (rowsaffected > 0)
                        {
                            result.Correct = true;
                            result.Message = "Usuario Actualizado Correctamente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = "Error al actualizar el usuario: " + ex;

            }
            return result;
        }

        public static ML.Result DeleteSQLClient(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var context = new SqlConnection(DL.Conexion.Getconection()))
                {
                    string query = "AlumnoDelete";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("idalumno", SqlDbType.Int);
                        collection[0].Value = IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result.Correct = true;
                            result.Message = "Usuario eliminado con exito";

                        }
                        else
                        {

                            result.Correct = false;
                            result.Message = "Error al eliminar el usuario";
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                
                result.Correct = false;
                result.Message = "Error al eliminar el usuario: " + ex;
            }
            return result;
        }

    }
}
