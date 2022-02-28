using Microsoft.EntityFrameworkCore;
using model.bp;
using model.bp.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transversal.bp.MovimientoViewModels;

namespace dataAccess.bp
{
    public class MovimientoDAO : ADao
    {
        public MovimientoDAO(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {

        }


        public List<Movimiento> GetTransactionsbyID(int idcuenta)
        {

            try
            {
                var _persona = Context.Movimientos.Where(x => x.bg_cue_id == idcuenta);
                if (_persona.Count() == 0) throw new Exception("No se encontro transacciones", null);
                return _persona.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public List<Movimiento> GetMovimientos()
        {

            try
            {
                var _movimiento = Context.Movimientos.Include(table => table._cuenta);
                if (_movimiento.Count() == 0) throw new Exception("No se encontro cuenta", null);
                return _movimiento.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
        public List<MovimientoReport> GetMovimientosParametros(DateTime fechainicio, DateTime fechafin, string identificacion)
        {

            try
            {
                var _movimiento = from m in Context.Movimientos
                                  join c in Context.Cuentas on m.bg_cue_id equals c.Id
                                  join p in Context.Personas on c.bg_per_id equals p.Id
                                  where m.Fecha.Date >= fechainicio.Date && m.Fecha.Date <= fechafin.Date && p.bp_per_identificacion == identificacion
                                  select new MovimientoReport {
                                      Cliente = p.bp_per_nombre,
                                      Estado = c.bp_cue_estado,
                                      Fecha = m.Fecha
                                  , Movimiento = m.bp_mov_valor,
                                      Numero_Cuenta = c.bp_cue_numero
                                  , Saldo_disponible = m.bp_mov_saldo,
                                      Saldo_inicial = c.bp_cue_sal_inicial,
                                      Tipo = c.bp_cue_tipo

                                  };

                if (_movimiento.Count() == 0) throw new Exception("No se encontro movimiento", null);
                return _movimiento.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
