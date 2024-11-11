public class PedidoDTO
{
    public Guid PedidoId { get; set; }
    public string ClienteNombre { get; set; }
    public string ProductoNombre { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public DateTime Fecha { get; set; }
}