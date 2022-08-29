using ejercicio1.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ejercicio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovimientosController : ControllerBase
    {
        /// <summary>
        /// Lista todas las cuentas
        /// </summary>
        [HttpGet(Name = "Get All movimientos")]
        public ActionResult<Retorno> GetMovimientos()
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno= context.Movimientos.ToList();
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
        [HttpGet("{id}",Name ="Return by id movimiento")]
        public ActionResult<Retorno>  GetCuenta(int id)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno = context.Movimientos.Where(con => con.IdMovimiento == id).ToList();
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
        [HttpPost(Name = "Add new movimiento")]
        public async Task<ActionResult<Retorno>>  InsertCuenta(iupMovimientos movimiento)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;

                    Movimientos insertMovi = new Movimientos();

                    insertMovi.IdMovimiento = movimiento.IdMovimiento;
                    insertMovi.Fecha = movimiento.Fecha;
                    insertMovi.TipoMovimiento = "";
                    insertMovi.Valor = movimiento.Valor;
                    insertMovi.IdMovimiento=movimiento.IdMovimiento;
                    List<Movimientos> latestMovi = new List<Movimientos>();

                    List<Cuentas> newCuentas = new List<Cuentas>();
                    newCuentas = context.Cuentas.OrderByDescending(x => x.NumeroCuenta == movimiento.cuenta).ToList();
                    insertMovi.IdCuenta = newCuentas[0].IdCuenta;

                    latestMovi = context.Movimientos.Where(x => x.IdCuenta == newCuentas[0].IdCuenta).OrderByDescending(x => x.IdMovimiento).ToList();

                    if (latestMovi.Count()==0)
                    {
                        
                            insertMovi.Saldo = newCuentas[0].SaldoInicial + movimiento.Valor;
                      


                    }
                    else
                    {
                      
                            insertMovi.Saldo = latestMovi[0].Saldo + movimiento.Valor;
                        
                    }

                    context.Movimientos.Add(insertMovi);
                    await context.SaveChangesAsync();
                    
                    lo_retorno.retorno = CreatedAtAction(nameof(GetCuenta), new {id= insertMovi.IdMovimiento}, insertMovi).Value;
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
        [HttpPut("{id}",Name = "update movimiento")]
        public async Task<ActionResult<Retorno>> updateCuenta(int id,iupMovimientos movimiento)
        {
            Retorno lo_retorno = new Retorno();

            if(id != movimiento.IdMovimiento)
            {
                return BadRequest();
            }


            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;

                    Movimientos insertMovi = new Movimientos();

                    insertMovi.IdMovimiento = movimiento.IdMovimiento;
                    insertMovi.Fecha = movimiento.Fecha;
                    insertMovi.TipoMovimiento = "";
                    insertMovi.Valor = movimiento.Valor;
                    insertMovi.IdMovimiento = movimiento.IdMovimiento;
                    List<Movimientos> latestMovi = new List<Movimientos>();

                    List<Cuentas> newCuentas = new List<Cuentas>();
                    newCuentas = context.Cuentas.OrderByDescending(x => x.NumeroCuenta == movimiento.cuenta).ToList();
                    insertMovi.IdCuenta = newCuentas[0].IdCuenta;

                    latestMovi = context.Movimientos.Where(x => x.IdCuenta == newCuentas[0].IdCuenta && x.IdMovimiento != movimiento.IdMovimiento).OrderByDescending(x => x.IdMovimiento).ToList();

                    if (latestMovi.Count() == 0)
                    {
                        
                            insertMovi.Saldo = newCuentas[0].SaldoInicial + movimiento.Valor;
                        



                    }
                    else
                    {
                        
                            insertMovi.Saldo = latestMovi[0].Saldo + movimiento.Valor;
                        
                    }


                    context.Entry(insertMovi).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                   await context.SaveChangesAsync();

                    lo_retorno.retorno = CreatedAtAction(nameof(GetCuenta), new { id = insertMovi.IdMovimiento }, insertMovi).Value;
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
                        lo_retorno.retorno = "No existe el movimiento";
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
        [HttpDelete("{id}", Name = "delete movimiento")]
        public async Task<ActionResult<Retorno>> deleteMovimiento(int id)
        {
            Retorno lo_retorno = new Retorno();


            try
            {
                using (var context = new ejercicio1Context())
                {

                    if (context.Movimientos==null)
                    {
                        return BadRequest();
                    }

                    var cuentaFind = await context.Movimientos.FindAsync(id);
                    if (cuentaFind == null)
                    {
                        lo_retorno.procesoCorrecto = false;
                        lo_retorno.retorno = "No existe la cuenta";
                        return lo_retorno;
                    }

                    lo_retorno.procesoCorrecto = true;
                    context.Movimientos.Remove(cuentaFind);
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