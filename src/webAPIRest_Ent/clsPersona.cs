using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webAPIRest_Ent
{

    public class clsPersona
    {
        public int id { get; set; }
        public string nombre { get; set; } = "";
        public string apellidos { get; set; } = "";
        public string direccion { get; set; } = "";
        public string telefono { get; set; } = "";
        [DisplayFormat (DataFormatString= "{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        [Display(Name = "Date of Birthday")]
        [DataType (DataType.Date)]
        public DateTime? fechaNac { get; set; }
        public clsPersona(int id, string nombre, string apellidos, string direccion, string telefono, DateTime? fechaNac)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.direccion = direccion;
            this.telefono = telefono;
            this.fechaNac = fechaNac;
        }
        public clsPersona()
        {
            this.id = 0;
            this.nombre = "";
            this.apellidos = "";
            this.direccion = "";
            this.telefono = "";
            this.fechaNac = null;
        }
    }
}
