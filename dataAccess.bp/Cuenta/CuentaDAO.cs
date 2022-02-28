using model.bp;
using model.bp.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dataAccess.bp
{
    public class CuentaDAO : ADao
    {
        public CuentaDAO(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {

        }

        public Cuenta GetCuentabyID(int numerocuenta)
        {

            try
            {
                var _persona = Context.Cuentas.Where(x => x.bp_cue_numero == numerocuenta);
                if (_persona.Count() == 0) throw new Exception("No se encontro cuenta", null);
                return _persona.First();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public List<Cuenta> GetCuentas()
        {

            try
            {
                var _persona = Context.Cuentas.Include(table=>table._persona);
                if (_persona.Count() == 0) throw new Exception("No se encontro cuenta", null);
                return _persona.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
