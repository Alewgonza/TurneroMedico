using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TurneroMedico.Context;
using TurneroMedico.Models;

namespace TurneroMedico.Controllers
{
    public class TurnosModelsController : Controller
    {
        private readonly TurnosDatabaseContext _context;

        public TurnosModelsController(TurnosDatabaseContext context)
        {
            _context = context;
        }

        // GET: TurnosModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turnos.ToListAsync());
        }

        // GET: TurnosModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnosModel = await _context.Turnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnosModel == null)
            {
                return NotFound();
            }

            return View(turnosModel);
        }

        // GET: TurnosModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TurnosModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreApellido,Dni,Email,Telefono,FechaTurno,EspecialidadElegida,DoctorElegido")] TurnosModel turnosModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnosModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turnosModel);
        }

        // GET: TurnosModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnosModel = await _context.Turnos.FindAsync(id);
            if (turnosModel == null)
            {
                return NotFound();
            }
            return View(turnosModel);
        }

        // POST: TurnosModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreApellido,Dni,Email,Telefono,FechaTurno,EspecialidadElegida,DoctorElegido")] TurnosModel turnosModel)
        {
            if (id != turnosModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnosModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnosModelExists(turnosModel.Id))
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
            return View(turnosModel);
        }

        // GET: TurnosModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnosModel = await _context.Turnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnosModel == null)
            {
                return NotFound();
            }

            return View(turnosModel);
        }

        // POST: TurnosModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnosModel = await _context.Turnos.FindAsync(id);
            if (turnosModel != null)
            {
                _context.Turnos.Remove(turnosModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnosModelExists(int id)
        {
            return _context.Turnos.Any(e => e.Id == id);
        }
    }
}
