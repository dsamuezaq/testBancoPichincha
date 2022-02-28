using business.bp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using model.bp;
using NUnit.Framework;
using service.bp.Controllers;
using System.Threading.Tasks;
using transversal.bp.CuentaViewModels;
using transversal.bp.MovimientoViewModels;
using transversal.bp.SystemViewModel;

namespace TestProject
{
    public class Tests
    {
    
        private readonly ILogger<TransactionController> _logger;
        private readonly TransactionController  _controller;
        public static DbContextOptions<ContextBancoPichincha> dbContextOptions { get; set; }
        public IConfiguration Configuration { get; }

        public Tests()
        {

            var conn = "Server=DESKTOP-29J30S7\\SERVERDS;Initial Catalog=BP_TEST;Persist Security Info=False;User ID=sa;Password=1990Dquimbia;MultipleActiveResultSets=True;App=EntityFramework;Connection Lifetime=120; Max Pool Size=20; Min Pool Size = 1; Pooling=True;";

            dbContextOptions = new DbContextOptionsBuilder<ContextBancoPichincha>()
                      .UseSqlServer(conn)
                      .Options;

            _controller = new TransactionController(_logger, new ContextBancoPichincha(dbContextOptions));
        }

        [SetUp]
        public void Setup()
        {
        


        }

        [Test]
        public  void EliminarCuenta()
        {   // Act
                  // Assert
             var okObjectResult = _controller.DeleteCliente("21312312").Result as ObjectResult; ;
            // act
           var actualtResult =okObjectResult.Value ;
          var resp = ((ReplyViewModel)actualtResult).messege;
            Assert.IsTrue(resp.Equals("No se encontro cuenta"));
        }

        [Test]
        public void CrearCuenta()
        {   // Act
            // Assert
            CuentaModel cuentaModel = new CuentaModel();
            cuentaModel.tipo = "Ahorros";
            cuentaModel.identificacion = "1718135161";
            cuentaModel.saldo_inicial = 200;
            cuentaModel.numero_cuenta = 33000;
            var okObjectResult = _controller.saveCuentas(cuentaModel).Result as ObjectResult; ;
            var actualtResult = okObjectResult.Value;
            var resp = ((ReplyViewModel)actualtResult).messege;
            Assert.IsTrue(resp.Equals("El cuenta se inserto correctamente"));
        }
        [Test]
        public void saldoNoDisponible()
        {   // Act
            // Assert
            MovimientoModel Mov = new MovimientoModel();
            Mov.Numero_cuenta = 2490;
            Mov.Valor = 1000;
            Mov.tipo_moviemiento = "Retiro";
       
               var okObjectResult = _controller.saveMoviemientos(Mov).Result as ObjectResult; ;
            var actualtResult = okObjectResult.Value;
            var resp = ((ReplyViewModel)actualtResult).messege;
            Assert.IsTrue(resp.Equals("Saldo no disponible"));
        }
    }
}