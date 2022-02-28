using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace transversal.bp.CuentaViewModels
{
    public class CuentaModel
    {
        public int numero_cuenta { get; set; }
        public string tipo { get; set; }
        public decimal saldo_inicial { get; set; }
        public string identificacion { get; set; }
    }
}
