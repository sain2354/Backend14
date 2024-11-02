using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrdenDetallesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdenDetallesController(AppDbContext context)
    {
        _context = context;
    }

    // Listar todos los detalles de órdenes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrdenDetalle>>> GetOrdenDetalles()
    {
        return await _context.OrdenDetalles.Include(d => d.Producto).ToListAsync();
    }

    // Obtener un detalle de orden por ID
    [HttpGet("{id}")]
    public async Task<ActionResult<OrdenDetalle>> GetOrdenDetalle(int id)
    {
        var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);

        if (ordenDetalle == null)
        {
            return NotFound();
        }

        return ordenDetalle;
    }

    // Crear un nuevo detalle de orden
    [HttpPost]
    public async Task<ActionResult<OrdenDetalle>> PostOrdenDetalle(OrdenDetalle ordenDetalle)
    {
        _context.OrdenDetalles.Add(ordenDetalle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrdenDetalle), new { id = ordenDetalle.Id }, ordenDetalle);
    }

    // Eliminar un detalle de orden
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrdenDetalle(int id)
    {
        var ordenDetalle = await _context.OrdenDetalles.FindAsync(id);
        if (ordenDetalle == null)
        {
            return NotFound();
        }

        _context.OrdenDetalles.Remove(ordenDetalle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
