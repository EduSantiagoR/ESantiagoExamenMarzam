using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class MedicamentoPedido
    {
        public ML.Pedido Pedido { get; set; }
        public ML.Medicamento Medicamento { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public List<MedicamentoPedido> Pedidos { get; set; }
    }
}
