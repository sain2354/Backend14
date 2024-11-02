public class Venta
{
    public int Id { get; set; }
    public string Cliente { get; set; }
    public int ProductoId { get; set; }
    public decimal Precio { get; set; }
    public DateTime Fecha { get; set; }
}