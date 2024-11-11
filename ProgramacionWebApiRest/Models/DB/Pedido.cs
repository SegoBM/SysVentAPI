using System;

namespace ProgramacionWebApiRest.Models
{
    public class Pedido
    {
        public Guid PedidoId { get; set; }  // uniqueidentifier
        public Guid ClienteId { get; set; }  // foreign key to Cliente
        public DateTime Fecha { get; set; }  // datetime
        public Guid ProductoId { get; set; }  // uniqueidentifier
        public int Cantidad { get; set; }  // int
        public decimal Precio { get; set; }  // decimal(18, 2)
    }
}