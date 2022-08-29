using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace TestProject
{
    public class BasePruebas
    {
        protected ejercicio1Context ConstruirContext(string nombredb)
        {

            var opciones = new DbContextOptionsBuilder<ejercicio1Context>()
                .UseInMemoryDatabase(nombredb).Options;
            var dbContext = new ejercicio1Context(opciones);

            return dbContext;
        }
    }
}
