using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pedido
    {
        public static List<ML.MedicamentoPedido> GetAll()
        {
            List<ML.MedicamentoPedido> pedidos = new List<ML.MedicamentoPedido>();
            try
            {
                using (DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    var query = context.MedicamentoPedidos.FromSqlRaw("PedidoGetAll");
                    if (query != null)
                    {
                        foreach (var item in query)
                        {
                            ML.MedicamentoPedido medicamentoPedido = new ML.MedicamentoPedido();
                            medicamentoPedido.Medicamento = new ML.Medicamento();
                            medicamentoPedido.Pedido = new ML.Pedido();

                            medicamentoPedido.Medicamento.IdMedicamento = item.MedicamentoId;
                            medicamentoPedido.Medicamento.Nombre = item.MedicamentoNombre;
                            medicamentoPedido.Medicamento.Precio = item.MedicamentoPrecio;

                            medicamentoPedido.Pedido.IdPedido = item.PedidoId;
                            medicamentoPedido.Pedido.Comprador = item.PedidoComprador;
                            medicamentoPedido.Pedido.Total = item.PedidoTotal;

                            medicamentoPedido.Cantidad = item.Cantidad;
                            medicamentoPedido.Total = item.Total;

                            pedidos.Add(medicamentoPedido);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return pedidos;
        }
        public static bool Add(string nombre)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"PedidoAdd '{nombre}'");
                    if (rowsAffected > 0)
                    {
                        correct = true;
                    }
                }
            }
            catch
            {

            }
            return correct;
        }
        public static int GetIdPedido(string nombre)
        {
            int numeroPedido = 0;
            try
            {
                using (DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    var query = (from a in context.Pedidos where a.Comprador == nombre select a).AsEnumerable().LastOrDefault();
                    numeroPedido = query.IdPedido;
                }

            }
            catch(Exception ex)
            {

            }
            return numeroPedido;
        }
        public static bool UpdateTotal(int idPedido)
        {
            bool correct = true;
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"PedidoUpdateTotal {idPedido}");
                    if (rowsAffected > 0)
                    {
                        correct = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return correct;
        }
        public static List<ML.Pedido> Get()
        {
            List<ML.Pedido> pedidos = new List<ML.Pedido>();
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    var query = context.Pedidos.FromSqlRaw("GetPedidos");
                    if(query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.Pedido pedido = new ML.Pedido();
                            pedido.IdPedido = item.IdPedido;
                            pedido.Comprador = item.Comprador;
                            pedido.Total = item.Total.Value;
                            pedidos.Add(pedido);
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return pedidos;
        }
        public static List<ML.MedicamentoPedido> GetDetalles(int idPedido)
        {
            List<ML.MedicamentoPedido> detalles = new List<ML.MedicamentoPedido>();
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    var query = (from medicamento in context.MedicamentoPedidos where medicamento.IdPedido == idPedido
                                 join medi in context.Medicamentos on medicamento.IdMedicamento equals medi.IdMedicamento
                                 select new
                                 {
                                     IdMedicamento = medicamento.IdMedicamento,
                                     NombreMedicamento = medi.Nombre,
                                     Precio = medi.Precio,
                                     Cantidad = medicamento.Cantidad,
                                     Total = medicamento.Total
                                 });
                    if (query != null)
                    {
                        foreach(var item in query)
                        {
                            ML.MedicamentoPedido medicamento = new ML.MedicamentoPedido();
                            medicamento.Medicamento = new ML.Medicamento();
                            medicamento.Medicamento.IdMedicamento = item.IdMedicamento;
                            medicamento.Medicamento.Nombre = item.NombreMedicamento;
                            medicamento.Medicamento.Precio = item.Precio;
                            medicamento.Cantidad = item.Cantidad;
                            medicamento.Total = item.Total;
                            detalles.Add(medicamento);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return detalles;
        }
        public static bool Delete(int idPedido)
        {
            bool correct = false;
            try
            {
                using(DL.ESantiagoExamenMarzamContext context = new DL.ESantiagoExamenMarzamContext())
                {
                    int rowsAffected = context.Database.ExecuteSqlRaw($"PedidoDelete {idPedido}");
                    if( rowsAffected > 0 )
                    {
                        correct = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return correct;
        }
    }
}
