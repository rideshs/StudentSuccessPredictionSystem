using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentSuccessPrediction.Data;
using StudentSuccessPrediction.Models;
using System.Collections;

namespace StudentSuccessPrediction.Controllers
{
    public class PreboardMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreboardMarksController(ApplicationDbContext context)
        {
            _context = context;
        }
        /*
         
          LoadStudentsAndSubjectsAsync() is a custom method that is called to load the lists of students and subjects
          in the PreboardMarkViewModel object.
        
         */
        private async Task LoadStudentsAndSubjectsAsync(PreboardMarkViewModel preboardMarkViewModel)
        {
            preboardMarkViewModel.Students = await _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            preboardMarkViewModel.Subjects = await _context.Subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                })
                .ToListAsync();
        }


        // GET: PreboardMarks
        public async Task<IActionResult> Index()
        {
            var preboardMarks = await _context.Preboards
                .Include(p => p.Student)
                .Include(p => p.Subject)
                .ToListAsync();

            var viewModelList = preboardMarks.Select(p => new PreboardMarkViewModel
            {
                Id = p.Id,
                StudentId = p.StudentId,
                SubjectId = p.SubjectId,
                MarkObtained = p.MarkObtained,
                StudentName = p.Student.Name,
                SubjectName = p.Subject.SubjectName
            }).ToList();

            return View(viewModelList);
        }

        // GET: PreboardMarks/Create
        public IActionResult Create()
        {
            // Load students and subjects from the database
            var students = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });

            var subjects = _context.Subjects.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SubjectName
            });

            // Create a new view model and populate its select list properties
            var viewModel = new PreboardMarkViewModel
            {
                Students = students.ToList(),
                Subjects = subjects.ToList()
            };

            return View(viewModel);
        }

        // POST: PreboardMarks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreboardMarkViewModel preboardMarkViewModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the user has already entered a preboard mark for the same subject
                bool isDuplicateSubjectMark = await _context.Preboards.AnyAsync(p =>
                    p.StudentId == preboardMarkViewModel.StudentId &&
                    p.SubjectId == preboardMarkViewModel.SubjectId &&
                    p.Id != preboardMarkViewModel.Id); // exclude current preboard mark if editing

                if (isDuplicateSubjectMark)
                {
                    ModelState.AddModelError("", "A preboard mark for this subject has already been entered for this student.");
                    await LoadStudentsAndSubjectsAsync(preboardMarkViewModel);
                    return View(preboardMarkViewModel);
                }


                // Create a new Preboard entity with the selected student, subject, and mark obtained
                var preboardMark = new Preboard
                {
                    StudentId = preboardMarkViewModel.StudentId,
                    SubjectId = preboardMarkViewModel.SubjectId,
                    MarkObtained = preboardMarkViewModel.MarkObtained
                };

                // Add the new Preboard entity to the context and save changes
                _context.Add(preboardMark);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // If the model state is invalid, repopulate the select list properties in the view model
            preboardMarkViewModel.Students = _context.Students
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList();

            preboardMarkViewModel.Subjects = _context.Subjects
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.SubjectName
                }).ToList();

            return View(preboardMarkViewModel);
        }

        // other controller methods

        // GET: PreboardMarks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preboardMark = await _context.Preboards.FindAsync(id);
            if (preboardMark == null)
            {
                return NotFound();
            }

            var preboardMarkViewModel = new PreboardMarkViewModel
            {
                Id = preboardMark.Id,
                StudentId = preboardMark.StudentId,
                SubjectId = preboardMark.SubjectId,
                MarkObtained = preboardMark.MarkObtained,
                Students = await _context.Students
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name //Text = $"{s.FirstName} {s.LastName}"
                    })
                    .ToListAsync(),
                Subjects = await _context.Subjects
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.SubjectName
                    })
                    .ToListAsync()
            };

            return View(preboardMarkViewModel);
        }

        // POST: PreboardMarks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PreboardMarkViewModel preboardMarkViewModel)
        {
            if (id != preboardMarkViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var preboardMark = await _context.Preboards.FindAsync(id);
                    preboardMark.StudentId = preboardMarkViewModel.StudentId;
                    preboardMark.SubjectId = preboardMarkViewModel.SubjectId;
                    preboardMark.MarkObtained = preboardMarkViewModel.MarkObtained;

                    // Check for duplicate subject mark
                    var isDuplicateSubjectMark = await _context.Preboards.AnyAsync(pm => pm.Id != id && pm.StudentId == preboardMarkViewModel.StudentId && pm.SubjectId == preboardMarkViewModel.SubjectId);
                    if (isDuplicateSubjectMark)
                    {
                        ModelState.AddModelError("", "A preboard mark for this subject has already been entered for this student.");
                        await LoadStudentsAndSubjectsAsync(preboardMarkViewModel);
                        return View(preboardMarkViewModel);
                    }
                    _context.Update(preboardMark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PreboardMarkExists(preboardMarkViewModel.Id))
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
            await LoadStudentsAndSubjectsAsync(preboardMarkViewModel);
            return View(preboardMarkViewModel);

        }

            //DELETE

            public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the preboard mark from the database
            var preboardMark = await _context.Preboards
                .Include(p => p.Student)
                .Include(p => p.Subject)
                .FirstOrDefaultAsync(p => p.Id == id);

            // If the preboard mark is not found, return a 404 error
            if (preboardMark == null)
            {
                return NotFound();
            }

            // Create a new PreboardMarkViewModel with the preboard mark data
            var preboardMarkViewModel = new PreboardMarkViewModel
            {
                Id = preboardMark.Id,
                StudentId = preboardMark.StudentId,
                StudentName = preboardMark.Student.Name,
                SubjectId = preboardMark.SubjectId,
                SubjectName = preboardMark.Subject.SubjectName,
                MarkObtained = preboardMark.MarkObtained
            };

            // Load the list of students and subjects for the dropdowns
            await LoadStudentsAndSubjectsAsync(preboardMarkViewModel);

            // Pass the view model to the view
            return View(preboardMarkViewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preboardMark = await _context.Preboards.FindAsync(id);
            _context.Preboards.Remove(preboardMark);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            private bool PreboardMarkExists(int id)
            {
                return _context.Preboards.Any(e => e.Id == id);
            }



        }
    }
