
using model.bp.dbo;
using model.bp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dataAccess.bp
{
    public class PersonaDAO : ADao
    {
        public PersonaDAO(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {

        }

        public Persona GetPersonabyID(String identificacion) {

            try
            {
                var _persona = Context.Personas.Where(x => x.bp_per_identificacion == identificacion).Include(tables => tables.Clientes);
                if (_persona.Count() == 0) throw new Exception("No se encontro el cliente", null);
                return _persona.First();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }

        public List<Persona> GetPersonas()
        {

            try
            {
                var _persona = Context.Personas.Include(tables => tables.Clientes);
                if (_persona.Count() == 0) throw new Exception("No se encontro persona", null);
                return _persona.ToList();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message, ex);
            }
        }
    }
}
