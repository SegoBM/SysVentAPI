using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgramacionWebApiRest.Models.DB;
using ProgramacionWebApiRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramacionWebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PuntoDeVentaWebContext _context;

        public PedidosController(PuntoDeVentaWebContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Join(_context.Clientes,
                      pedido => pedido.ClienteId,
                      cliente => cliente.ClienteId,
                      (pedido, cliente) => new { pedido, cliente })
                .Join(_context.Productos,
                      pc => pc.pedido.ProductoId,
                      producto => producto.ProductoId,
                      (pc, producto) => new PedidoDTO
                      {
                          PedidoId = pc.pedido.PedidoId,
                          ClienteNombre = pc.cliente.Nombre,
                          ProductoNombre = producto.Nombre,
                          Cantidad = pc.pedido.Cantidad,
                          Precio = pc.pedido.Precio,
                          Fecha = pc.pedido.Fecha
                      })
                .ToListAsync();

            return pedidos;
        }

        // GET: api/Pedidos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            // Verificar si el producto existe
            var producto = await _context.Productos.FindAsync(pedido.ProductoId);

            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado" });
            }

            // Verificar si hay suficiente stock
            if (producto.Cantidad < pedido.Cantidad)
            {
                return BadRequest(new { message = "No hay suficiente stock para este producto" });
            }

            // Restar la cantidad solicitada del producto
            producto.Cantidad -= pedido.Cantidad;

            // Agregar el pedido
            _context.Pedidos.Add(pedido);

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPedido), new { id = pedido.PedidoId }, pedido);
        }

        // PUT: api/Pedidos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(Guid id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // DELETE: api/Pedidos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(Guid id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }
    }
}
