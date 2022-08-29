using ejercicio1.Controllers;
using ejercicio1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.PruebasUnitarias
{
    [TestClass]
    public class CuentasControllerTest: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerLasCuentas()
        {
            var nombreDB=Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Cuentas.Add(new Cuentas() { NumeroCuenta = "12345678", IdCliente = 1, IdCuenta = 0, SaldoInicial = 124585, Estado = 1, TipoCuenta = "Ahorro" });
            contexto.Cuentas.Add(new Cuentas() { NumeroCuenta = "12458752", IdCliente = 1, IdCuenta = 0, SaldoInicial = 10000, Estado = 1, TipoCuenta = "Corriente" });
            await contexto.SaveChangesAsync();

            var contexto2=ConstruirContext(nombreDB);
           

            var controller = new CuentasController();
            var respuesta =  controller.GetCuenta();
            var cuentas = respuesta.Value;
            Assert.IsNotNull(cuentas);
        }
    }
}
