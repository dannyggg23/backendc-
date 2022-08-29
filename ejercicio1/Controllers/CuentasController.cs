using ejercicio1.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ejercicio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : ControllerBase
    {
        /// <summary>
        /// Lista todas las cuentas
        /// </summary>
        [HttpGet(Name = "Get All Cuentas")]
        public ActionResult<Retorno> GetCuenta()
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno= context.Cuentas.ToList();
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

        /// <summary>
        /// Busca las cuentas por el id
        /// </summary>
        [HttpGet("{id}",Name ="Return by id")]
        public ActionResult<Retorno>  GetCuenta(int id)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno = context.Cuentas.Where(con => con.IdCuenta == id).ToList();
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

        /// <summary>
        /// Agrega una nueva cuenta
        /// </summary>
        [HttpPost(Name = "Add new cuenta")]
        public async Task<ActionResult<Retorno>>  InsertCuenta(Cuentas cuenta)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    context.Cuentas.Add(cuenta);
                    await context.SaveChangesAsync();
                    
                    lo_retorno.retorno = CreatedAtAction(nameof(GetCuenta), new {id=cuenta.IdCuenta},cuenta ).Value;
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

        /// <summary>
        /// Actualiza una cuenta
        /// </summary>
        [HttpPut("{id}",Name = "update cuenta")]
        public async Task<ActionResult<Retorno>> updateCuenta(int id,Cuentas cuenta)
        {
            Retorno lo_retorno = new Retorno();

            if(id != cuenta.IdCuenta)
            {
                return BadRequest();
            }


            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                   context.Entry(cuenta).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                   await context.SaveChangesAsync();

                    lo_retorno.retorno = CreatedAtAction(nameof(GetCuenta), new { id = cuenta.IdCuenta }, cuenta).Value;
                }


                return lo_retorno;
            }
            catch (Exception er)
            {
                using (var context = new ejercicio1Context())
                {
                    var cuentaFind = await context.Cuentas.FindAsync(id);
                    if (cuentaFind == null)
                    {
                        lo_retorno.procesoCorrecto = false;
                        lo_retorno.retorno = "No existe la cuenta";
                        return lo_retorno;
                    }
                }
                   
                lo_retorno.procesoCorrecto = false;
                lo_retorno.retorno = er.Message;

                return lo_retorno;
            }
        }

        /// <summary>
        /// Elimina una cuenta
        /// </summary>
        [HttpDelete("{id}", Name = "delete cuenta")]
        public async Task<ActionResult<Retorno>> deleteCuenta(int id)
        {
            Retorno lo_retorno = new Retorno();


            try
            {
                using (var context = new ejercicio1Context())
                {

                    if (context.Cuentas==null)
                    {
                        return BadRequest();
                    }

                    var cuentaFind = await context.Cuentas.FindAsync(id);
                    if (cuentaFind == null)
                    {
                        lo_retorno.procesoCorrecto = false;
                        lo_retorno.retorno = "No existe la cuenta";
                        return lo_retorno;
                    }

                    lo_retorno.procesoCorrecto = true;
                    context.Cuentas.Remove(cuentaFind);
                    await context.SaveChangesAsync();

                    lo_retorno.retorno = "Datos eliminados";
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