using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAPIRest_DAL;
using webAPIRest_Ent;

namespace webAPIRest_BL
{
    public class clsManejadoraPersonaBL
    {
        public int insertaPersonaBL(clsPersona p)
        {
            clsManejadoraPersonaDAL maneja = new clsManejadoraPersonaDAL();
            int i = maneja.insertaPersonaDAL(p);
            return i;
        }
        public int borraPersonaBL(int id)
        {
            clsManejadoraPersonaDAL maneja = new clsManejadoraPersonaDAL();
            int i = maneja.borraPersonaDAL(id);
            return i;
        }
        public clsPersona getPersonaBL(int id)
        {
            clsManejadoraPersonaDAL maneja = new clsManejadoraPersonaDAL();
            return maneja.getPersonaDAL(id);
        }
        public int actualizaPersonaBL(clsPersona p)
        {
            clsManejadoraPersonaDAL maneja = new clsManejadoraPersonaDAL();
            int i = maneja.actualizaPersonaDAL(p);
            return i;
        }
    }
}
