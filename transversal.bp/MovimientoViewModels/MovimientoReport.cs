using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transversal.bp.MovimientoViewModels
{
    public class MovimientoReport
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public int Numero_Cuenta { get; set; }


        public string Tipo { get; set; }
        public decimal Saldo_inicial { get; set; }
        public string Estado { get; set; }
        public decimal Movimiento { get; set; }

        public decimal Saldo_disponible { get; set; }


    }


}
