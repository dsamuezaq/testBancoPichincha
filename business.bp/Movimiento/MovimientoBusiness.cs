using dataAccess.bp;
using model.bp;
using model.bp.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transversal.bp.MovimientoViewModels;
using transversal.bp.SystemViewModel;

namespace business.bp
{
    public class MovimientoBusiness : ABusiness
    {
        MovimientoDAO _movimientoDAO;
        CuentaDAO _cuentaDao;
        public MovimientoBusiness(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {
            _movimientoDAO = new MovimientoDAO(_contextBancoPichincha);
            _cuentaDao = new CuentaDAO(_contextBancoPichincha);
        }
        public async Task<ReplyViewModel> Get()
        {
            try
            {
                var _movimientos = _movimientoDAO.GetMovimientos().Select(x => new MovimientoReplyModel
                {
                    Estado = x._cuenta.bp_cue_estado,
                    Movimiento = x.bp_mov_tipo +" "+ Math.Abs(x.bp_mov_valor) 
                    ,
                    Numero_Cuenta = x._cuenta.bp_cue_numero
                    ,
                    Saldo_Inicial = x._cuenta.bp_cue_sal_inicial,
                    Tipo=x.bp_mov_tipo

                });
                FillSuccessReplyViewModel("Consulta Exitosa", _movimientos);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }
        public async Task<ReplyViewModel> GetByParameter(DateTime fechainicio, DateTime fechafin, string identificacion)
        {
            try
            {
                FillSuccessReplyViewModel("Consulta Exitosa", _movimientoDAO.GetMovimientosParametros(fechainicio, fechafin, identificacion));
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Save(MovimientoModel _movimiento)
        {
            try
            {
                Cuenta _cuenta = _cuentaDao.GetCuentabyID(_movimiento.Numero_cuenta);
                Movimiento movimiento = new Movimiento();
              
                movimiento = fillMovimiento(_movimiento, movimiento, _cuenta);



                _cuentaDao.InsertUpdateOrDelete(movimiento, "I");
                FillSuccessReplyViewModel("El movimiento se inserto correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        #region Method Private
        private Movimiento fillMovimiento(MovimientoModel _movimientomodel, Movimiento movimiento, Cuenta _cuenta)
        {
            if (_movimientomodel.tipo_moviemiento != "Retiro" && _movimientomodel.tipo_moviemiento != "Deposito")
                throw new Exception("El tipo de movimiento no existe, debe ser Retiro o Deposito ", null);

            decimal valorIngreso = _movimientomodel.tipo_moviemiento == "Retiro" ? _movimientomodel.Valor * -1 : _movimientomodel.Valor;
            var transacciones = _movimientoDAO.GetTransactionsbyID(_cuenta.Id);
            decimal saldoactual = transacciones.Last().bp_mov_saldo + valorIngreso;
            if (saldoactual < 0) {
                throw new Exception("Saldo no disponible", null);
            }
            decimal limitediario = transacciones.Where(x => x.bp_mov_tipo == "Retiro" && x.Fecha.Date == DateTime.Now.Date).Sum(x => x.bp_mov_valor);
            if (Math.Abs(limitediario) > 1000)
                throw new Exception("Cupo diario Excedido por dia", null);

            movimiento.bp_mov_tipo = _movimientomodel.tipo_moviemiento;
            movimiento.bg_cue_id = _cuenta.Id;
            movimiento.bp_mov_valor = valorIngreso;
            movimiento.bp_mov_saldo = saldoactual;
            movimiento.Fecha = DateTime.Now;
            return movimiento;
        }

        #endregion


    }
}
