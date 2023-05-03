using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSuccessPrediction.Data;
using StudentSuccessPrediction.Models;

namespace StudentSuccessPrediction.Controllers
{
    public class AttendanceMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttendanceMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AttendanceMarks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AttendanceMarks.Include(a => a.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AttendanceMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AttendanceMarks == null)
            {
                return NotFound();
            }

            var attendanceMark = await _context.AttendanceMarks
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceMark == null)
            {
                return NotFound();
            }

            return View(attendanceMark);
        }

        // GET: AttendanceMarks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name");
            return View();
        }

        // POST: AttendanceMarks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AttendanceMarkobtained,StudentId,Subject")] AttendanceMark attendanceMark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendanceMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", attendanceMark.StudentId);
            return View(attendanceMark);
        }

        // GET: AttendanceMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AttendanceMarks == null)
            {
                return NotFound();
            }

            var attendanceMark = await _context.AttendanceMarks.FindAsync(id);
            if (attendanceMark == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", attendanceMark.StudentId);
            return View(attendanceMark);
        }

        // POST: AttendanceMarks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AttendanceMarkobtained,StudentId,Subject")] AttendanceMark attendanceMark)
        {
            if (id != attendanceMark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendanceMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceMarkExists(attendanceMark.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Name", attendanceMark.StudentId);
            return View(attendanceMark);
        }

        // GET: AttendanceMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AttendanceMarks == null)
            {
                return NotFound();
            }

            var attendanceMark = await _context.AttendanceMarks
                .Include(a => a.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendanceMark == null)
            {
                return NotFound();
            }

            return View(attendanceMark);
        }

        // POST: AttendanceMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AttendanceMarks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AttendanceMarks'  is null.");
            }
            var attendanceMark = await _context.AttendanceMarks.FindAsync(id);
            if (attendanceMark != null)
            {
                _context.AttendanceMarks.Remove(attendanceMark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceMarkExists(int id)
        {
          return (_context.AttendanceMarks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
