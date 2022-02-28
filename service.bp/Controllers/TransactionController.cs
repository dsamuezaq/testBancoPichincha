using business.bp;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using model.bp;
using service.bp.libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using transversal.bp.ClienteViewModels;
using transversal.bp.CuentaViewModels;
using transversal.bp.MovimientoViewModels;

namespace service.bp.Controllers
{
    public class TransactionController : AController<TransactionController>
    {
        #region class variables
        private readonly PersonaBusiness _personaBusiness;
        private readonly CuentaBusiness _cuentaBusiness;
        private readonly MovimientoBusiness _movimientoBusiness;
        private readonly ILogger<TransactionController> _logger;
        #endregion

        public TransactionController(ILogger<TransactionController> logger, ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {
            _logger = logger;
            _personaBusiness = new PersonaBusiness(_contextBancoPichincha);
            _cuentaBusiness= new CuentaBusiness(_contextBancoPichincha);

            _movimientoBusiness = new MovimientoBusiness(_contextBancoPichincha);
        }

        #region API Cliente
        [HttpGet]
        [Route("ObtenerClientes")]
        public async Task<IActionResult> Getcliente()
        {
            return Ok(_personaBusiness.Get());
        }
        [HttpPost]
        [Route("GuardarCliente")]
        public async Task<IActionResult> savecliente(ClienteModel _cliente)
        {
            return Ok(_personaBusiness.Save(_cliente));
        }
        [HttpPut]
        [Route("ActualizarCliente")]
        public async Task<IActionResult> UpdateCliente(ClienteModel _cliente, string identificacion)
        {

            return Ok(_personaBusiness.UpdateById(_cliente, identificacion));
        }
        [HttpDelete]
        [Route("BorrarCliente")]
        public async Task<IActionResult> DeleteCliente( string identificacion)
        {

            return Ok(_personaBusiness.Delete( identificacion));
        }

        [HttpGet]
        [Route("Clientes")]
        public async Task<IActionResult> GetData()
        {
            return Ok(_personaBusiness.GetdataCreation());
        }
        #endregion
        #region API Cuentas
        [HttpPost]
        [Route("GuardarCuentas")]
        public async Task<IActionResult> saveCuentas(CuentaModel _cuenta)
        {
            return Ok(_cuentaBusiness.Save(_cuenta));
        }

        [HttpPut]
        [Route("ActualizarCuentas")]
        public async Task<IActionResult> UpdateCuentas(CuentaModel _cuenta,int numero_cuenta)
        {
            return Ok(_cuentaBusiness.Update(_cuenta, numero_cuenta));
        }
        [HttpDelete]
        [Route("BorrarCuenta")]
        public async Task<IActionResult> DeleteCuenta( int numero_cuenta)
        {

            return Ok(_cuentaBusiness.Delete(numero_cuenta));
        }
        [HttpGet]
        [Route("Cuentas")]
        public async Task<IActionResult> GetCuentas()
        {

            return Ok(_cuentaBusiness.Get());
        }
        #endregion
        #region API Movimiento
        [HttpPost]
        [Route("GuardarMoviemiento")]
        public async Task<IActionResult> saveMoviemientos(MovimientoModel _movimiento)
        {
            return Ok(_movimientoBusiness.Save(_movimiento));
        }

        [Route("Movimientos")]
        [HttpGet]
        public async Task<IActionResult> GetMovimiento()
        {

            return Ok(_movimientoBusiness.Get());
        }

        [Route("ReporteMovimientos")]
        [HttpGet]
        public async Task<IActionResult> GetMovimiento(DateTime fechainicio , DateTime fechafin , string identificacion)
        {

            return Ok(_movimientoBusiness.GetByParameter(fechainicio, fechafin, identificacion));
        }
        #endregion
    }
}