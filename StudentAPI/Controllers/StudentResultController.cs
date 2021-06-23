using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Data;
using StudentAPI.Entities;
using StudentAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentResultController: ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentResultController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<StudentResultVM> GetStudents()
        {
            var studentResults = _context.StudentResults.Include(s => s.Students).Select(s => new StudentResultVM {
                Id = s.Id,
                Percentage = s.Percentage,
                StudentId = s.StudentId,
                StudetName = s.Students.Name
            }).ToList(); ;
            return studentResults;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentResult(int id)
        {
            try
            {
                var studentResult = _context.StudentResults.Include(s => s.Students).Select(s => new StudentResultVM
                {
                    Id = s.Id,
                    Percentage = s.Percentage,
                    StudentId = s.StudentId,
                    StudetName = s.Students.Name
                }).FirstOrDefault(); ;
                if (studentResult != null)
                {
                    return Ok(studentResult);
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentResultVM studentResultVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StudentResult studentResult = new StudentResult();
                    studentResult.StudentId = studentResultVM.StudentId;
                    studentResult.Percentage = studentResultVM.Percentage;

                    _context.StudentResults.Add(studentResult);
                    _context.SaveChanges();
                    return Ok(GetStudentResult(studentResult.Id));
                }
                catch (Exception e)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromBody] StudentResultVM studentResultVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StudentResult studentResult = _context.StudentResults.Where(a => a.Id == studentResultVM.Id).FirstOrDefault();

                    if (studentResult!=null)
                    {
                        studentResult.StudentId = studentResultVM.StudentId;
                        studentResult.Percentage = studentResultVM.Percentage;
                        _context.StudentResults.Update(studentResult);
                        _context.SaveChanges();
                        return Ok(GetStudentResult(studentResult.Id));
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.NotFound);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode((int)HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                StudentResult studentResult = _context.StudentResults.Where(a => a.Id == id).FirstOrDefault();

                if (studentResult != null)
                {
                    _context.Remove(studentResult);
                    _context.SaveChanges();

                    return Ok("Student Result Deleted Successfully!");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
