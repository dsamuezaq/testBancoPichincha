using model.bp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using transversal.bp.ClienteViewModels;
using transversal.bp.SystemViewModel;
using model.bp.dbo;
using dataAccess.bp;
using transversal.bp.Resources;

namespace business.bp
{
    public class PersonaBusiness : ABusiness
    {
        PersonaDAO _personaDao;
        public PersonaBusiness(ContextBancoPichincha _contextBancoPichincha) : base(_contextBancoPichincha)
        {
            _personaDao = new PersonaDAO(_contextBancoPichincha);
        }


        public async Task<ReplyViewModel> Get()
        {
            try
            {            
                FillSuccessReplyViewModel("Consulta Exitosa", _personaDao.SelectEntity<Persona>());
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Save(ClienteModel _cliente)
        {
            try
            {

                Persona persona = new Persona();
                fillPersona(_cliente, persona);
                persona.bp_per_identificacion = _cliente.identificacion;
                _personaDao.InsertUpdateOrDelete(persona, "I");
                FillSuccessReplyViewModel("El cliente se inserto correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> UpdateById(ClienteModel _cliente, string identificacion)
        {
            try
            {
                Persona persona = _personaDao.GetPersonabyID(identificacion);
                fillPersona(_cliente, persona);
                _personaDao.InsertUpdateOrDelete(persona, "U");
                FillSuccessReplyViewModel("El cliente se elimino correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> Delete( string identificacion)
        {
            try
            {


                Persona persona = _personaDao.GetPersonabyID(identificacion);
                _personaDao.InsertUpdateOrDelete(persona, "D");
                FillSuccessReplyViewModel("El cliente se elimino correctamente", null);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        public async Task<ReplyViewModel> GetdataCreation()
        {
            try
            {
                var _clientes = _personaDao.GetPersonas();
                List<ClienteReplyModel> clienteReplyModels = new List<ClienteReplyModel>();
                clienteReplyModels= _clientes.Select(x => new ClienteReplyModel
                {
                    Nombres = x.bp_per_nombre,  
                    Teléfono = x.bp_per_telefono,
                    Dirección = x.bp_per_direccion,
                    Contraseña = x.Clientes.First().bp_cli_password,
                    estado = x.Clientes.First().bp_cli_estado
                }).ToList();

                FillSuccessReplyViewModel("Consulta Exitosa", clienteReplyModels);
                return reply;

            }
            catch (Exception ex)
            {
                FillErrorReplyViewModel(ex.Message);
                return reply;

            }

        }

        #region Method Private
        private Persona fillPersona(ClienteModel _cliente, Persona persona) {
     
            persona.bp_per_nombre = _cliente.Nombres;
            persona.bp_per_identificacion = _cliente.identificacion;
            persona.bp_per_direccion = _cliente.Dirección;
            if (_cliente.genero.Equals("masculino") || _cliente.genero.Equals("femenino"))
                persona.bp_per_genero = _cliente.genero.Equals("masculino") ? ConsGenero.Male : ConsGenero.Feminine;
            else
                throw new Exception("El genero no es correcto debe ser masculino o femenino ", null);
            persona.bp_per_telefono = _cliente.Teléfono;
            Cliente cliente = new Cliente();
            cliente.bp_cli_estado = ConsEstado.Active;
            cliente.bp_cli_password = _cliente.Contraseña;
            persona.Clientes.Add(cliente);

            return persona;
        }

        #endregion

    }
}
