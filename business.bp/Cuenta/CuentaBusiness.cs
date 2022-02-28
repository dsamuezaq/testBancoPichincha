using dataAccess.bp;
using model.bp;
using model.bp.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transversal.bp.CuentaViewModels;
using transversal.bp.Resources;
using transversal.bp.SystemViewModel;

namespace business.bp
{
    public class CuentaBusiness : ABusiness
    {
        PersonaDAO _personaDao;
        CuentaDAO _cuentaDao;
        public CuentaBusiness(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {
            _personaDao = new PersonaDAO(_contextBancoPichincha);
            _cuentaDao= new CuentaDAO(_contextBancoPichincha);
        }

        public async Task<ReplyViewModel> Save(CuentaModel _cuenta)
        {
            try
            {
                Persona _persona = _personaDao.GetPersonabyID(_cuenta.identificacion);
                Cuenta _cuentas = new Cuenta();
                _cuentas = fillCuenta(_cuenta, _cuentas, _persona.Id);
                _cuentaDao.InsertUpdateOrDelete(_cuentas, "I");
                FillSuccessReplyViewModel("El cuenta se inserto correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Update(CuentaModel _cuentaview, int numerocuenta)
        {
            try
            {
                Cuenta _cuenta = _cuentaDao.GetCuentabyID(numerocuenta);
                Cuenta _cuentas= new Cuenta();
                _cuentas = fillCuenta(_cuentaview, _cuenta, _cuenta.bg_per_id);
                _cuentas.Id = _cuenta.Id;
                _cuentas.bp_cue_numero = _cuenta.bp_cue_numero;
                _cuentaDao.InsertUpdateOrDelete(_cuentas, "U");
                FillSuccessReplyViewModel("El cuenta se inserto correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Delete(int numerocuenta)
        {
            try
            {


                 Cuenta _cuenta = _cuentaDao.GetCuentabyID(numerocuenta);
                _personaDao.InsertUpdateOrDelete(_cuenta, "D");
                FillSuccessReplyViewModel("El cuenta se elimino correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Get()
        {
            try
            {


                List<Cuenta> _cuenta = _cuentaDao.GetCuentas();

                List<CuentaReplyModel>  _infocuenta= _cuenta.Select(x => new CuentaReplyModel
                {
                    Numero_Cuenta = x.bp_cue_numero,
                    Estado = x.bp_cue_estado,
                    Tipo = x.bp_cue_tipo,
                    Saldo_Inicial = x.bp_cue_sal_inicial,
                    Cliente = x._persona.bp_per_nombre
                }).ToList();
                FillSuccessReplyViewModel("Consulta Exitosa", _infocuenta);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }
        #region Method Private
        private Cuenta fillCuenta(CuentaModel cuenta, Cuenta _cuenta, int id_persona )
        {

          
            _cuenta.bp_cue_numero = cuenta.numero_cuenta;
            _cuenta.bp_cue_tipo = cuenta.tipo;
            _cuenta.bp_cue_estado = ConsEstado.Active;
            _cuenta.bp_cue_sal_inicial = cuenta.saldo_inicial;
            _cuenta.bg_per_id = id_persona;
            if (_cuenta.Id == 0) {
            Movimiento movimiento = new Movimiento();
            movimiento.bp_mov_tipo = "Deposito";
            movimiento.bp_mov_valor =  cuenta.saldo_inicial;
            movimiento.bp_mov_saldo = cuenta.saldo_inicial;
            movimiento.Fecha = DateTime.Now;
            _cuenta.movimientos.Add( movimiento );
            }
            return _cuenta;
        }

        #endregion

    }
}
