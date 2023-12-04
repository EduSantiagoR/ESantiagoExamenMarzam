using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Pedido
    {
        public int IdPedido { get; set; }
        public string Comprador { get; set; } = null!;
        public decimal? Total { get; set; }
    }
}
