using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Cuentas
    {
        public int IdCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public int IdCliente { get; set; }
        public byte Estado { get; set; }

        
    }
}
