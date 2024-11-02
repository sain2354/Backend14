using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TuNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // Obtener todas las ventas
        [HttpGet]
        public ActionResult<List<Venta>> GetAll()
        {
            return _context.Ventas.ToList();
        }

        // Crear una nueva venta
        [HttpPost]
        public ActionResult<Venta> Create([FromBody] Venta venta)
        {
            _context.Ventas.Add(venta);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = venta.Id }, venta);
        }

        // Actualizar una venta existente
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Venta venta)
        {
            if (id != venta.Id) return BadRequest(); // Verifica que el ID coincida

            _context.Entry(venta).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // Marca la entidad como modificada
            _context.SaveChanges(); // Guarda los cambios

            return NoContent(); // Devuelve una respuesta 204 sin contenido
        }

        // Eliminar una venta existente
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var venta = _context.Ventas.Find(id); // Busca la venta por ID
            if (venta == null) return NotFound(); // Devuelve 404 si no se encuentra

            _context.Ventas.Remove(venta); // Elimina la venta
            _context.SaveChanges(); // Guarda los cambios

            return NoContent(); // Devuelve 204 sin contenido
        }
    }
}
