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
            var attendanceMarks = await _context.AttendanceMarks
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .Select(a => new AttendanceViewModel
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    StudentName = a.Student.Name, // populate the student name property
                    SubjectId = a.SubjectId,
                    SubjectName = a.Subject.SubjectName, // populate the subject name property
                    AttendanceMarkobtained = a.AttendanceMarkobtained
                })
                .ToListAsync();

            return View(attendanceMarks);
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
            var model = new AttendanceViewModel();

            // Get a list of all students from the database
            var students = _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToList();
            model.Students = students;

            // Get a list of all subjects from the database
            var subjects = _context.Subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                })
                .ToList();
            model.Subjects = subjects;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttendanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if an attendance mark already exists for this student and subject
                var existingAttendanceMark = await _context.AttendanceMarks
                    .FirstOrDefaultAsync(a => a.StudentId == model.StudentId && a.SubjectId == model.SubjectId);

                if (existingAttendanceMark == null)
                {
                    // Create a new attendance mark
                    var attendanceMark = new AttendanceMark
                    {
                        StudentId = model.StudentId,
                        SubjectId = model.SubjectId,
                        AttendanceMarkobtained = model.AttendanceMarkobtained
                    };

                    _context.AttendanceMarks.Add(attendanceMark);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An attendance mark already exists for this student and subject.");
                }
            }

            // Repopulate the dropdown lists
            var students = await _context.Students
                .OrderBy(s => s.Name)
                
                .ToListAsync();

            var subjects = await _context.Subjects
                .OrderBy(s => s.SubjectName)
                .ToListAsync();

            model.Students = students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            model.Subjects = subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            }).ToList();

            return View(model);
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
