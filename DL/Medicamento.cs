using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Medicamento
    {
        public int IdMedicamento { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
    }
}
