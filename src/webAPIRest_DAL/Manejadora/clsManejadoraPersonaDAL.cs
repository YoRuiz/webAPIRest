using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using webAPIRest_Ent;

namespace webAPIRest_DAL
{
    public class clsManejadoraPersonaDAL
    {
        private clsMyConnection miConexion;
        public clsManejadoraPersonaDAL()
        {
            miConexion = new clsMyConnection();
        }

        /// <summary>
        /// este método recibe una persona de la capa BL y la inserta en la base de datos, en caso de error lanza una excepción, devuelve un entero con la salida de executeNonQuery
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int insertaPersonaDAL(clsPersona p)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = p.nombre;
            comando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar).Value = p.apellidos;
            comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = p.direccion;
            comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = p.telefono;
            comando.Parameters.Add("@fechaNac", System.Data.SqlDbType.Date).Value = p.fechaNac;
            try
            {
                conexion = miConexion.getConnection();
                comando.CommandText = "Insert into personas (nombre, apellidos, fechaNac, direccion, telefono) values (@nombre, @apellidos, @fechaNac, @direccion, @telefono)";
                comando.Connection = conexion;
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                miConexion.closeConnection(ref conexion);
                conexion.Close();
            }

            return resultado;
        }

        /// <summary>
        /// este método se concecta a la base de datos y actualiza una persona que se recibe de la capa BL, devuelve un entero con la salida del ExecuteNonQuery
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int actualizaPersonaDAL(clsPersona p)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            comando.Parameters.Add("@id", System.Data.SqlDbType.VarChar).Value = p.id;
            comando.Parameters.Add("@nombre", System.Data.SqlDbType.VarChar).Value = p.nombre;
            comando.Parameters.Add("@apellidos", System.Data.SqlDbType.VarChar).Value = p.apellidos;
            comando.Parameters.Add("@direccion", System.Data.SqlDbType.VarChar).Value = p.direccion;
            comando.Parameters.Add("@telefono", System.Data.SqlDbType.VarChar).Value = p.telefono;
            comando.Parameters.Add("@fechaNac", System.Data.SqlDbType.Date).Value = p.fechaNac;
            try
            {
                conexion = miConexion.getConnection();
                comando.CommandText = "Update personas set nombre=@nombre, apellidos=@apellidos, fechaNac=@fechaNac, direccion=@direccion, telefono=@telefono where IDPersona=@id";
                comando.Connection = conexion;
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                miConexion.closeConnection(ref conexion);
                conexion.Close();
            }

            return resultado;
        }

        /// <summary>
        /// este método se conecta a la base de datos y borra una persona cuyo id coincide con el id que tiene la persona que recibe por parámetros de la capa BL, devuelve como entero la salida de ExecuteNonQuery, en caso de error lanza una excepción
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int borraPersonaDAL(int id)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

            try
            {
                conexion = miConexion.getConnection();
                comando.CommandText = "delete from personas where IDPersona=@id";
                comando.Connection = conexion;
                resultado = comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                miConexion.closeConnection(ref conexion);
                conexion.Close();
            }

            return resultado;
        }


        /// <summary>
        /// método que se conecta a la base de datos y devuelve la persona cuyo id coincide con el id pasado por parámetros, en caso de error lanza una excepción
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public clsPersona getPersonaDAL(int id)
        {
            clsPersona persona = null;
            clsMyConnection miConexion = new clsMyConnection();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            comando.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;
            try
            {
                conexion = miConexion.getConnection();
                comando.CommandText = "SELECT * FROM personas where IDPersona=@id";
                comando.Connection = conexion;
                lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    lector.Read();
                    persona = new clsPersona();
                    persona.id = (int)lector["IDPersona"];
                    persona.nombre = (String)lector["nombre"];
                    persona.apellidos = (String)lector["apellidos"];
                    persona.telefono = (String)lector["telefono"];
                    persona.direccion = (String)lector["direccion"];
                    persona.fechaNac = (DateTime?)lector["fechaNac"];
                }
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { miConexion.closeConnection(ref conexion); }
            return persona;
        }
    }
}
