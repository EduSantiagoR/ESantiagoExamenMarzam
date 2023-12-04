using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Medicamento
    {
        public static List<ML.Medicamento> GetAll()
        {
            List<ML.Medicamento> medicamentos = new List<ML.Medicamento>();
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    var query = context.Medicamentos.FromSqlRaw("MedicamentoGetAll");
                    if(query != null)
                    {
                        foreach(var item in  query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();
                            medicamento.IdMedicamento = item.IdMedicamento;
                            medicamento.Nombre = item.Nombre;
                            medicamento.Precio = item.Precio;
                            medicamentos.Add(medicamento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return medicamentos;
        }
    }
}