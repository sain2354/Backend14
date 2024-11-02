public class OrdenDetalle
{
    public int Id { get; set; } // Clave primaria

    // Clave foránea a Productos
    public int ProductoId { get; set; }
    public required Producto Producto { get; set; }  // Relación con Producto

    // Clave foránea a Ordenes
    public int OrdenId { get; set; }
    public required Orden Orden { get; set; }  // Relación con Orden

    public int Cantidad { get; set; }
    public decimal Precio { get; set; }

    // Campo calculado opcional para el subtotal del detalle
    public decimal Subtotal => Cantidad * Precio;
}
