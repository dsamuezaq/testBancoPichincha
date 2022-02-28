using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.bp.dbo
{
    [Table("bp_cuenta", Schema = "dbo")]
    public class Cuenta
    {

        public Cuenta()
        {
            this.movimientos = new HashSet<Movimiento>();
   
        }
        [Key]
        public int Id { get; set; }
        public int bg_per_id { get; set; }
        public int bp_cue_numero { get; set; }
        public string bp_cue_tipo{ get; set; }
        public string bp_cue_estado { get; set; }
        public decimal bp_cue_sal_inicial { get; set; }

        [ForeignKey("bg_per_id")]
        public virtual Persona _persona { get; set; }

        public virtual ICollection<Movimiento> movimientos { get; set; }
    }
}
