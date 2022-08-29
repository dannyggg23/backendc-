using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Personas
    {
        public Personas()
        {
            Clientes = new HashSet<Clientes>();
        }

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }


 
}
