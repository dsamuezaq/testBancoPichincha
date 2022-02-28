using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.bp.dbo
{
    [Table("bp_persona", Schema = "dbo")]
    public class Persona: IEntity
    {

        public Persona()
        {
            this.Clientes = new HashSet<Cliente>();
            this.Cuentas = new HashSet<Cuenta>();
        }
        [Key]
        public int Id { get; set; }
        public string bp_per_nombre { get; set; }
        public string bp_per_genero { get; set; }
        public string bp_per_identificacion { get; set; }
        public string bp_per_direccion
        { get; set; }
        public string bp_per_telefono
        { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Cuenta> Cuentas { get; set; }
    }
}
