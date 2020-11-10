using FinalProgramacion.Models;
using FinalProgramacion.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinalProgramacion
{
    public class AccesoDatos
    {
        public static bool InsertarNuevoExamen(examen e)
        {
            bool resultado = false;
            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "INSERT INTO Examenes VALUES(@Fecha, @IdMateria, @Nota)";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                cmd.Parameters.AddWithValue("@Fecha", e.fecha); //Le agregamos nuevos parametros
                cmd.Parameters.AddWithValue("@IdMateria", e.materia);
                cmd.Parameters.AddWithValue(@"Nota", e.Nota);


                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos
                cmd.ExecuteNonQuery();

                resultado = true;


            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }




            return resultado;
        }
        public static List<listaExamenes> ListadoExamenes()
        {
            List<listaExamenes> resultado = new List<listaExamenes>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select e.Fecha, e.Nota, m.Nombre, m.Nivel
                                       from Examenes e join Materias m on e.IdMateria = m.IdMateria
                                           order by m.Nivel asc, e.Nota desc";
                cmd.Parameters.Clear(); 

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        listaExamenes le = new listaExamenes();
                        le.fecha = dr["Fecha"].ToString();
                        le.nota = int.Parse(dr["Nota"].ToString());
                        le.nombreM = dr["Nombre"].ToString();
                        le.nivel = int.Parse(dr["Nivel"].ToString());


                        resultado.Add(le);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }
        public static List<MateriaEscuela> listaMateria()
        {
            List<MateriaEscuela> resultado = new List<MateriaEscuela>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            //creamos una cadena de conexion hacia la base de datos
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                //creamos un comando
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT * FROM Materias";
                cmd.Parameters.Clear(); //le limpiamos los parametros 

                //se va a ejecutar una accion por usuario por eso es .text como una sentencia sql 
                cmd.CommandType = System.Data.CommandType.Text;
                //le pasa cual seria la consulta que en este caso es el insert
                cmd.CommandText = consulta;

                //se abra la conexion
                cn.Open();
                //que el comando tome la conexion con la base de datos
                cmd.Connection = cn;
                //los parametros debe ser correctos

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        MateriaEscuela ma = new MateriaEscuela();
                        ma.id = int.Parse(dr["IdMateria"].ToString());
                        ma.nombre = dr["Nombre"].ToString();
                        ma.nivel = int.Parse(dr["Nivel"].ToString()); ;



                        resultado.Add(ma);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }

        public static List<Reporte> PromedioDeAprobados()
        {
            List<Reporte> resultado = new List<Reporte>();

            string CadenaC = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"].ToString();
            SqlConnection cn = new SqlConnection(CadenaC);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select m.Nivel, AVG (nota) Promedio
                                      from Examenes e join Materias m on e.IdMateria = m.IdMateria
                                       where Nota >= 6
                                         group by Nivel";
                cmd.Parameters.Clear(); 
               
                cmd.CommandType = System.Data.CommandType.Text;
                
                cmd.CommandText = consulta;

             
                cn.Open();
    
                cmd.Connection = cn;
              

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Reporte r = new Reporte();
                        r.nivelR = int.Parse(dr["Nivel"].ToString());
                        r.promedio = double.Parse(dr["Promedio"].ToString()); ;



                        resultado.Add(r);

                    }
                }

            }
            catch (Exception)
            {
                throw;

            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }

    }
}
