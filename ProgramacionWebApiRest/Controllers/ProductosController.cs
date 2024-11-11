using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramacionWebApiRest.Models.DB;
using WebApiProgramWeb;

namespace WebApiProgramWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly PuntoDeVentaWebContext _context;

        public ProductosController(PuntoDeVentaWebContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoCreateDto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Join(_context.Proveedores,
                      producto => producto.ProveedorId,
                      proveedor => proveedor.ProveedorId,
                      (producto, proveedor) => new ProductoCreateDto
                      {
                          ProductoId = producto.ProductoId,
                          Nombre = producto.Nombre,
                          Descripcion = producto.Descripcion,
                          Precio = producto.Precio,
                          Cantidad = producto.Cantidad,
                          ProveedorId = producto.ProveedorId,
                          ProveedorNombre = proveedor.Nombre
                      })
                .ToListAsync();

            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(Guid id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
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

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto productoDto)
        {
            var producto = new Producto
            {
                ProductoId = Guid.NewGuid(),
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Cantidad = productoDto.Cantidad,
                ProveedorId = productoDto.ProveedorId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducto", new { id = producto.ProductoId }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(Guid id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
