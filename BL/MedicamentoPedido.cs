using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MedicamentoPedido
    {
        public static bool Add(int idPedido, int idMedicamento, int cantidad)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"MedicamentoPedidoAdd {idMedicamento},{cantidad},{idPedido}");
                    if(rowsAffected > 0)
                    {
                        correct = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return correct;
        }
    }
}
