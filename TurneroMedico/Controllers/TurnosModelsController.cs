using Microsoft.AspNetCore.Mvc;
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

        //--------------------------------------------------
        public async Task<IActionResult> BuscarPorDni(int? dni)
        {
            if (dni == null)
            {
                return View(); // Esto cargará la vista en blanco para el formulario de búsqueda
            }

            var turnos = await _context.Turnos
                .Where(t => t.Dni == dni)
                .ToListAsync();

            return View(turnos); // Esto pasa los resultados de la búsqueda a la vista
        }
        //----------------------------------------------------------

        // GET: TurnosModels/Create
        public IActionResult Create()
        {
            return View();
        }

        private TurnosModel? validarDuplicadoSuperpuesto(TurnosModel turnosModel)
        {
            // Query Syntax
            //var turnturnoDuplicadoo = (from turno in _context.Turnos
            //                           where turno.FechaTurno == turnosModel.FechaTurno
            //                           select turno).FirstOrDefault();

            var validacionDuracion = turnosModel.FechaTurno.AddMinutes(30);
            var turnoDuplicado = _context.Turnos.Where(turno =>
                                                 turnosModel.FechaTurno > turno.FechaTurno && turnosModel.FechaTurno < turno.FechaFinTurno
                                                 || validacionDuracion > turno.FechaTurno && validacionDuracion < turno.FechaFinTurno
                                                 ).FirstOrDefault();

            return turnoDuplicado;
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
                var esDuplicado = validarDuplicadoSuperpuesto(turnosModel);

                if (esDuplicado != null)
                {
                    ModelState.AddModelError("FechaTurno", 
                        $"Ya existe un turno para la fecha seleccionada." +
                        $"Siguiente turno disponible {esDuplicado.FechaFinTurno}");
                    return View(turnosModel);
                }
                else
                {
                    turnosModel.FechaFinTurno = turnosModel.FechaTurno.AddMinutes(turnosModel.DuracionTurno);
                    _context.Add(turnosModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreApellido,Dni,Email,Telefono,FechaTurno,FechaFinTurno,EspecialidadElegida,DoctorElegido")] TurnosModel turnosModel)
        {
            if (id != turnosModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var esDuplicado = validarDuplicadoSuperpuesto(turnosModel);

                try
                {
                    if (esDuplicado != null && esDuplicado.Id != turnosModel.Id)
                    {
                        ModelState.AddModelError("FechaTurno",
                            $"Ya existe un turno para la fecha seleccionada." +
                            $"Siguiente turno disponible {esDuplicado.FechaFinTurno}");
                        return View(turnosModel);
                    } else
                    {
                        _context.Update(turnosModel);
                        await _context.SaveChangesAsync();
                    }
                    
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
