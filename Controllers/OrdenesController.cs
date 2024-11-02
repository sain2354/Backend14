using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class OrdenesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdenesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenes()
    {
        // Incluye la colección de detalles de la orden
        return await _context.Ordenes.Include(o => o.OrdenDetalles).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Orden>> PostOrden(Orden orden)
    {
        orden.Fecha = DateTime.Now;
        _context.Ordenes.Add(orden);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetOrden", new { id = orden.Id }, orden);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Orden>> GetOrden(int id)
    {
        // Incluye la colección de detalles de la orden
        var orden = await _context.Ordenes
                                  .Include(o => o.OrdenDetalles)
                                  .FirstOrDefaultAsync(o => o.Id == id);

        if (orden == null)
        {
            return NotFound();
        }

        return orden;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrden(int id, Orden orden)
    {
        if (id != orden.Id)
        {
            return BadRequest();
        }

        _context.Entry(orden).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!OrdenExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrden(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);
        if (orden == null)
        {
            return NotFound();
        }

        _context.Ordenes.Remove(orden);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool OrdenExists(int id)
    {
        return _context.Ordenes.Any(e => e.Id == id);
    }
}
