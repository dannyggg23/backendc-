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
    public class MovimientosControllerTest: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerLosMovimientos()
        {
            var nombreDB=Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreDB);

            contexto.Movimientos.Add(new Movimientos() { IdMovimiento = 0, Fecha =  DateTime.Parse("2022-08-29T05:08:41.749Z") , IdCuenta = 1, Saldo = 124585,Valor=100,TipoMovimiento="Deposito" });
            contexto.Movimientos.Add(new Movimientos() { IdMovimiento = 0, Fecha =  DateTime.Parse("2022-08-29T05:08:41.749Z") , IdCuenta = 1, Saldo = 124585,Valor=100,TipoMovimiento="Deposito" });
            await contexto.SaveChangesAsync();

            var contexto2=ConstruirContext(nombreDB);
           

            var controller = new MovimientosController();
            var respuesta =  controller.GetMovimientos();
            var movimientos = respuesta.Value;
            Assert.IsNotNull(movimientos);
        }
    }
}
