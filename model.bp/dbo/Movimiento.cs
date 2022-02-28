using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.bp.dbo
{
    [Table("bp_movimiento", Schema = "dbo")]
    public class Movimiento
    {
        [Key]
        public int Id { get; set; }
        public int bg_cue_id { get; set; }
        public DateTime Fecha { get; set; }
        public string bp_mov_tipo { get; set; }
        public decimal bp_mov_valor { get; set; }
        public decimal bp_mov_saldo { get; set; }


        [ForeignKey("bg_cue_id")]
        public virtual Cuenta _cuenta { get; set; }
    }
}
