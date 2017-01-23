using System.Collections.Generic;
using webAPIRest_DAL;
using webAPIRest_Ent;

namespace webAPIRest_BL
{
    public class clsListadosBL
    {
        public List<clsPersona> listarBL()
        {
            List<clsPersona> lista = new List<clsPersona>();
            clsListadosDAL listado = new clsListadosDAL();
            lista = listado.getListadoPersonasDAL();
            return lista;
        }
    }
}
