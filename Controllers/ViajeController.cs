using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using travelApp.Models;

namespace travelApp.Controllers
{
    public class ViajeController : Controller
    {
        private readonly TravelingContext _context;

        public ViajeController(TravelingContext context)
        {
            _context = context;
        }

        // GET: Viaje
        public async Task<IActionResult> Index()
        {
            var travelingContext = _context.Viajes.Include(v => v.IdCiudadNavigation).Include(v => v.IdVehiculoNavigation).ThenInclude(v => v.IdTipoNavigation);
            
            return View(await travelingContext.ToListAsync());
        }

        // GET: Viaje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.IdCiudadNavigation)
                .Include(v => v.IdVehiculoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // GET: Viaje/Create
        public IActionResult Create()
        {
            ViewData["IdCiudad"] = new SelectList(_context.Ciudades, "Id", "Nombre");
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculos.Include(v => v.IdTipoNavigation).ToArray().DistinctBy(v=>v.IdTipoNavigation.Id), "Id", "IdTipoNavigation.Nombre");
            
            return View();
        }

        // POST: Viaje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCiudad,IdVehiculo,Fecha")] Viaje viaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudades, "Id", "Nombre", viaje.IdCiudad);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculos.Include(v => v.IdTipoNavigation), "Id", "IdTipoNavigation.Nombre", viaje.IdVehiculo);
            return View(viaje);
        }

        // GET: Viaje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudades, "Id", "Nombre", viaje.IdCiudad);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculos.Include(v => v.IdTipoNavigation), "Id", "IdTipoNavigation.Nombre", viaje.IdVehiculo);
            return View(viaje);
        }

        // POST: Viaje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCiudad,IdVehiculo,Fecha")] Viaje viaje)
        {
            if (id != viaje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViajeExists(viaje.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCiudad"] = new SelectList(_context.Ciudades, "Nombre", "Id", viaje.IdCiudad);
            ViewData["IdVehiculo"] = new SelectList(_context.Vehiculos.Include(v => v.IdTipoNavigation), "Id", "Id", viaje.IdVehiculo);
            return View(viaje);
        }

        // GET: Viaje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.IdCiudadNavigation)
                .Include(v => v.IdVehiculoNavigation).ThenInclude(v => v.IdTipoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // POST: Viaje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Viajes == null)
            {
                return Problem("Entity set 'TravelingContext.Viajes'  is null.");
            }
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje != null)
            {
                _context.Viajes.Remove(viaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViajeExists(int id)
        {
          return (_context.Viajes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
