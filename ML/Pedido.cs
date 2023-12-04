using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public string? Comprador { get; set; }
        public decimal? Total { get; set; }
        public List<Pedido>? Pedidos { get; set; }
        public int? IdMedicamento { get; set; }
        public int? Cantidad { get; set; }
    }
}
