using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model.bp.dbo
{
    [Table("bp_cliente", Schema = "dbo")]
    public class Cliente : IEntity
    {
        
        [Key]
        public int Id { get; set; }
        public string bp_cli_password { get; set; }
        public string bp_cli_estado { get; set; }
        public int bg_per_id { get; set; }
        [ForeignKey("bg_per_id")]
        public virtual Persona _persona { get; set; }

    }
}
