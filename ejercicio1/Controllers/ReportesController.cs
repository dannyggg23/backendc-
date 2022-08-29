using ejercicio1.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ejercicio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportesController : ControllerBase
    {
        /// <summary>
        /// Lista todas los clientes
        /// </summary>
        [HttpGet("{cliente}/{fecha}", Name = "GET REPORTES MOVIMEINTOS")]
        public ActionResult<Retorno> GetMovimientos(int cliente,string fecha)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {

                    var fechas=fecha.Split('%');


                    List<Cuentas> newCuentas = new List<Cuentas>();
                    newCuentas = context.Cuentas.OrderByDescending(x => x.IdCliente == cliente).ToList();

                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno = context.Movimientos.Where(m => m.IdCuenta == newCuentas[0].IdCuenta && m.Fecha >= DateTime.Parse(fechas[0].ToString()) && m.Fecha <= DateTime.Parse(fechas[1])).ToList();
                }
                return lo_retorno;
            }
            catch (Exception er)
            {
                lo_retorno.procesoCorrecto = false;
                lo_retorno.retorno = er.Message;

                return lo_retorno;
            }
        }

    }
}