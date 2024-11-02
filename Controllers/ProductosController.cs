using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TuNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Producto>> GetAll()
        {
            return _context.Productos.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetById(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return producto;
        }

        [HttpPost]
        public ActionResult<Producto> Create([FromBody] Producto producto)
        {
            if (producto == null) return BadRequest(); // Asegúrate de manejar el caso de producto nulo
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id) return BadRequest();
            _context.Entry(producto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            _context.Productos.Remove(producto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
