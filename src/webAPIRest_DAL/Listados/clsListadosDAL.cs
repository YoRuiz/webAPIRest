using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using webAPIRest_Ent;
using webAPIRest_DAL;

namespace webAPIRest_DAL
{
    public class clsListadosDAL
    {
        /// <summary>
        /// este método se conecta a la base de datos y crea una lista de personas con los datos de la tabla personas de la base de datos, en caso de error lanza una excepcion
        /// </summary>
        /// <returns></returns>
        public List<clsPersona> getListadoPersonasDAL()
        {
            clsPersona persona;
            List<clsPersona> lista = new List<clsPersona>();
            clsMyConnection miConexion = new clsMyConnection();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;
            try
            {
                conexion = miConexion.getConnection();
                comando.CommandText = "SELECT * FROM personas";
                comando.Connection = conexion;
                lector = comando.ExecuteReader();
                if (lector.HasRows)
                {
                    while (lector.Read())
                    {
                        persona = new clsPersona();
                        persona.id = (int) lector["IDPersona"];
                        persona.nombre = (String)lector["nombre"];
                        persona.apellidos = (String)lector["apellidos"];
                        persona.telefono = (String)lector["telefono"];
                        persona.direccion = (String)lector["direccion"];
                        persona.fechaNac = (DateTime)lector["fechaNac"];
                        lista.Add(persona);
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally { miConexion.closeConnection(ref conexion); }

            return lista;
        }     
    }
}
