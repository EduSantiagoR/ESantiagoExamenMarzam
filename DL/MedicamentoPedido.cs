using System;
using System.Collections.Generic;

namespace DL
{
    public partial class MedicamentoPedido
    {
        public int IdPedido { get; set; }
        public int IdMedicamento { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        //
        public int MedicamentoId { get; set; }
        public string MedicamentoNombre { get; set; }
        public decimal MedicamentoPrecio { get; set; }
        public int PedidoId { get; set; }
        public string PedidoComprador { get; set; }
        public decimal PedidoTotal { get; set; }
        //
        public virtual Medicamento IdMedicamentoNavigation { get; set; } = null!;
        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
    }
}
