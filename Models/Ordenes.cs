public class Orden
{
    public int Id { get; set; } // Clave primaria
    public DateTime Fecha { get; set; } = DateTime.Now;

    // Relación con detalles de la orden
    public List<OrdenDetalle> OrdenDetalles { get; set; } = new(); // Inicializado para evitar nulos

    // Campo opcional para el total de la orden
    public decimal Total { get; set; }
}
