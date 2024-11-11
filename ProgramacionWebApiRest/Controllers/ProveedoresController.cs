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
    public class ProveedoresController : ControllerBase
    {
        private readonly PuntoDeVentaWebContext _context;

        public ProveedoresController(PuntoDeVentaWebContext context)
        {
            _context = context;
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedore>>> GetProveedores()
        {
            return await _context.Proveedores.ToListAsync();
        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedore>> GetProveedore(Guid id)
        {
            var proveedore = await _context.Proveedores.FindAsync(id);

            if (proveedore == null)
            {
                return NotFound();
            }

            return proveedore;
        }

        // PUT: api/Proveedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedore(Guid id, Proveedore proveedore)
        {
            if (id != proveedore.ProveedorId)
            {
                return BadRequest();
            }

            _context.Entry(proveedore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedoreExists(id))
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

        // POST: api/Proveedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedore>> PostProveedore(Proveedore proveedore)
        {
            _context.Proveedores.Add(proveedore);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProveedore", new { id = proveedore.ProveedorId }, proveedore);
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedore(Guid id)
        {
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }

            _context.Proveedores.Remove(proveedore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedoreExists(Guid id)
        {
            return _context.Proveedores.Any(e => e.ProveedorId == id);
        }
    }
}
