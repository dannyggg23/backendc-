using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Clientes
    {
        public int IdCliente { get; set; }
        public int IdPersona { get; set; }
        public string Clave { get; set; }
        public byte Estado { get; set; }

        public virtual Personas? IdPersonaNavigation { get; set; }
    }


    public partial class IupClientes
    {

        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int Edad { get; set; }
        public string Identificacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Clave { get; set; }
        public byte Estado { get; set; }

    }
}
