using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentSuccessPrediction.Data;
using StudentSuccessPrediction.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSuccessPrediction.Controllers
{
    public class PreboardMarksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreboardMarksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PreboardMarks
        public async Task<IActionResult> Index()
        {
            var model = new PreboardMarkViewModel
            {
                PreboardMarks = await _context.PreboardMarks.Include(p => p.Student).ToListAsync()
            };

            return View(model);
        }

        // GET: PreboardMarks/Create
        public IActionResult Create()
        {
            var model = new PreboardMarkViewModel
            {
                Subjects = _context.Subjects.Select(s => new SubjectMarkViewModel
                {
                    SubjectId = s.Id,
                    SubjectName = s.SubjectName,
                    MarkObtained = 0
                }).ToList(),
                Students = _context.Students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Subjects","PreboardsMarks")] PreboardMarkViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Save the PreboardMark instance to the database so that it gets an Id

                var preboardMark = new PreboardMark
                {
                    TotalMark = viewModel.Subjects.Sum(s => s.MarkObtained),
                    StudentId = viewModel.StudentId
                };

                _context.Add(preboardMark);
                
                // Loop through the Subjects and create PreboardSubjectMark instances

                foreach (var subject in viewModel.Subjects)
                {
                    var preboardMarkSubject = new PreboardSubjectMark
                    {
                        PreboardMarkId = preboardMark.Id,
                        SubjectId = subject.SubjectId,
                        MarkObtained = subject.MarkObtained
                    };

                    _context.Add(preboardMarkSubject);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /*foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }*/


            viewModel.Subjects = _context.Subjects.Select(s => new SubjectMarkViewModel
            {
                SubjectId = s.Id,
                SubjectName = s.SubjectName,
                MarkObtained = 0
            }).ToList();
            viewModel.Students = _context.Students.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preboardMark = await _context.PreboardMarks.FindAsync(id);

            if (preboardMark == null)
            {
                return NotFound();
            }

            var viewModel = new PreboardMarkViewModel
            {
                StudentId = preboardMark.StudentId,
                Subjects = await _context.Subjects.Select(s => new SubjectMarkViewModel
                {
                    SubjectId = s.Id,
                    SubjectName = s.SubjectName,
                    MarkObtained = _context.PreboardSubjectMarks.Where(psm => psm.SubjectId == s.Id && psm.PreboardMarkId == id)
                                                                .Select(psm => psm.MarkObtained)
                                                                .FirstOrDefault()
                }).ToListAsync(),
                Students = _context.Students.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                }).ToList()
            };

            return View(viewModel);
        }

    }

}