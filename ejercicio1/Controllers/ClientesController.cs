using ejercicio1.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace ejercicio1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Lista todas los clientes
        /// </summary>
        [HttpGet(Name = "Get All Clientes")]
        public ActionResult<Retorno> GetClientes()
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno= context.Personas.ToList();
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
        /// Busca clientes por el id
        /// </summary>
        [HttpGet("{id}",Name ="Return  by id")]
        public ActionResult<Retorno> GetCliente(int id)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;
                    lo_retorno.retorno = context.Personas.Where(con => con.IdPersona == id).ToList();
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
        /// Agrega un nuevo cliente
        /// </summary>
        [HttpPost(Name = "Add new cliente")]
        public async Task<ActionResult<Retorno>>  InsertCliente(IupClientes persona)
        {
            Retorno lo_retorno = new Retorno();

            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;

                    Personas sub = new Personas() { 
                    Direccion= persona.Direccion,
                    Edad=persona.Edad,
                    Genero=persona.Genero,
                    Nombre=persona.Nombre,
                    Telefono=persona.Telefono,
                    Identificacion=persona.Identificacion,
                    IdPersona=persona.IdPersona,
                    };
                   

                    context.Personas.Add(sub);
                    await context.SaveChangesAsync();

                    lo_retorno.retorno = CreatedAtAction(nameof(GetCliente), new { id = persona.IdPersona }, persona).Value;


                    Clientes stu = new Clientes()
                    {
                        IdCliente=0,
                        IdPersona =persona.IdPersona,
                        Estado=1,
                        Clave=persona.Clave,
                        IdPersonaNavigation=sub
                    };

                    context.Clientes.Add(stu);
                    await context.SaveChangesAsync();

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
        /// Actualiza un cliente
        /// </summary>
        [HttpPut("{id}",Name = "update cliente")]
        public async Task<ActionResult<Retorno>> updateCliente(int id, IupClientes persona)
        {
            Retorno lo_retorno = new Retorno();

            if(id != persona.IdPersona)
            {
                return BadRequest();
            }


            try
            {
                using (var context = new ejercicio1Context())
                {
                    lo_retorno.procesoCorrecto = true;

                    Personas sub = new Personas()
                    {
                        Direccion = persona.Direccion,
                        Edad = persona.Edad,
                        Genero = persona.Genero,
                        Nombre = persona.Nombre,
                        Telefono = persona.Telefono,
                        Identificacion = persona.Identificacion,
                        IdPersona = persona.IdPersona,
                    };

                    context.Entry(sub).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    lo_retorno.retorno = CreatedAtAction(nameof(GetCliente), new { id = persona.IdPersona }, persona).Value;

                }


                return lo_retorno;
            }
            catch (Exception er)
            {
                using (var context = new ejercicio1Context())
                {
                    var cuentaFind = await context.Personas.FindAsync(id);
                    if (cuentaFind == null)
                    {
                        lo_retorno.procesoCorrecto = false;
                        lo_retorno.retorno = "No existe el cliente";
                        return lo_retorno;
                    }
                }
                   
                lo_retorno.procesoCorrecto = false;
                lo_retorno.retorno = er.Message;

                return lo_retorno;
            }
        }

        /// <summary>
        /// Elimina un cliente
        /// </summary>
        [HttpDelete("{id}", Name = "delete cliente")]
        public async Task<ActionResult<Retorno>> deleteCliente(int id)
        {
            Retorno lo_retorno = new Retorno();


            try
            {
                using (var context = new ejercicio1Context())
                {

                    if (context.Personas==null)
                    {
                        return BadRequest();
                    }

                    var cuentaFind = await context.Personas.FindAsync(id);
                    if (cuentaFind == null)
                    {
                        lo_retorno.procesoCorrecto = false;
                        lo_retorno.retorno = "No existe el cliente";
                        return lo_retorno;
                    }

                    lo_retorno.procesoCorrecto = true;

                    List<Clientes> cli = new List<Clientes>();
                    cli = context.Clientes.Where(con => con.IdPersona == id).ToList();

                    context.Clientes.Remove(cli[0]);
                    await context.SaveChangesAsync();

                    context.Personas.Remove(cuentaFind);
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