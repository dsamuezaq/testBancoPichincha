using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transversal.bp.MovimientoViewModels
{
    public class MovimientoReplyModel
    {
        public int Numero_Cuenta { get; set; }
        public string Tipo { get; set; }
        public decimal Saldo_Inicial { get; set; }
        public string Estado { get; set; }
        public string Movimiento { get; set; }
    }
}
