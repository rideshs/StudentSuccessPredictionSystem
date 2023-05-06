
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSuccessPrediction.Data;
using StudentSuccessPrediction.Models;

namespace StudentSuccessPrediction.Controllers
{
    public class AssignmentMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssignmentMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AssignmentMark
        public async Task<IActionResult> Index()
        {
            var assignmentMarks = await _context.AssignmentMarks
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .Select(a => new AssignmentMarkViewModel
                {
                    Id = a.Id,
                    StudentId = a.StudentId,
                    StudentName = a.Student.Name, // populate the student name property
                    SubjectId = a.SubjectId,
                    SubjectName = a.Subject.SubjectName, // populate the subject name property
                    AssignmentMarkobtained = a.AssignmentMarkobtained
                })
                .ToListAsync();

            return View(assignmentMarks);
        }

        // GET: AssignmentMark/Create
        public IActionResult Create()
        {
            var viewModel = new AssignmentMarkViewModel
            {
                Subjects = _context.Subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                }).ToList(),
                Students = _context.Students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(viewModel);
        }


        // POST: AssignmentMark/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignmentMarkViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var assignmentMark = new AssignmentMark
                {
                    StudentId = viewModel.StudentId,
                    SubjectId = viewModel.SubjectId,
                    AssignmentMarkobtained = viewModel.AssignmentMarkobtained
                };

                _context.Add(assignmentMark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Subjects = _context.Subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            }).ToList();

            viewModel.Students = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return View(viewModel);
        }


        // GET: AssignmentMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AssignmentMarks == null)
            {
                return NotFound();
            }

            var assignmentMark = await _context.AssignmentMarks.FindAsync(id);
            if (assignmentMark == null)
            {
                return NotFound();
            }

            var viewModel = new AssignmentMarkViewModel
            {
                Id = assignmentMark.Id,
                StudentId = assignmentMark.StudentId,
                SubjectId = assignmentMark.SubjectId,
                AssignmentMarkobtained = assignmentMark.AssignmentMarkobtained,
                Students = _context.Students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList(),
                Subjects = _context.Subjects.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignmentMarkViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var studentExists = _context.Students.Any(s => s.Id == viewModel.StudentId);

                if (!studentExists)
                {
                    ModelState.AddModelError("StudentId", "Invalid student selected.");
                    viewModel.Students = _context.Students.Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }).ToList();

                    viewModel.Subjects = _context.Subjects.Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.SubjectName
                    }).ToList();

                    return View(viewModel);
                }

                var assignmentMark = new AssignmentMark
                {
                    Id = viewModel.Id,
                    StudentId = viewModel.StudentId,
                    SubjectId = viewModel.SubjectId,
                    AssignmentMarkobtained = viewModel.AssignmentMarkobtained
                };

                try
                {
                    _context.Update(assignmentMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentMarkExists(assignmentMark.Id))
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

            if (viewModel == null)
            {
                return NotFound();
            }

            viewModel.Students = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            viewModel.Subjects = _context.Subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            }).ToList();

            return View(viewModel);
        }





        // GET: AssignmentMarks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentMark = await _context.AssignmentMarks
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignmentMark == null)
            {
                return NotFound();
            }

            return View(assignmentMark);
        }

        // POST: AssignmentMarks/Details/5
        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsConfirmed(int id)
        {
            var assignmentMark = await _context.AssignmentMarks.FindAsync(id);
            if (assignmentMark == null)
            {
                return NotFound();
            }

            _context.AssignmentMarks.Remove(assignmentMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: AssignmentMarks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignmentMark = await _context.AssignmentMarks
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignmentMark == null)
            {
                return NotFound();
            }

            var viewModel = new AssignmentMarkViewModel
            {
                Id = assignmentMark.Id,
                StudentId = assignmentMark.StudentId,
                SubjectId = assignmentMark.SubjectId,
                AssignmentMarkobtained = assignmentMark.AssignmentMarkobtained
            };

            return View(viewModel);
        }


        // POST: AssignmentMarks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignmentMark = await _context.AssignmentMarks.FindAsync(id);
            if (assignmentMark == null)
            {
                return NotFound();
            }

            _context.AssignmentMarks.Remove(assignmentMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool AssignmentMarkExists(int id)
        {
            return _context.AssignmentMarks.Any(e => e.Id == id);
        }


    }
}