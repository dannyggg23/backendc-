using System;
using System.Collections.Generic;

namespace ejercicio1.Models
{
    public partial class Movimientos
    {
        public int IdMovimiento { get; set; }
        public DateTime? Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Saldo { get; set; }
        public int IdCuenta { get; set; }
    }


    public class iupMovimientos
    {
        public int IdMovimiento { get; set; }
        public DateTime? Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal? Valor { get; set; }
        public string cuenta { get; set; }
    }
}
