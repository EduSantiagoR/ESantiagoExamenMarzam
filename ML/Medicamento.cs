namespace ML
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public decimal Precio { get; set; }
        public string? Nombre { get; set; }
        public List<Medicamento> Medicamentos { get; set; }

    }
}