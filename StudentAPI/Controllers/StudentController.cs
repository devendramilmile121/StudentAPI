using Microsoft.AspNetCore.Mvc;
using StudentAPI.Data;
using StudentAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController: ControllerBase
    {
        private readonly StudentDbContext _context;

        public StudentController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            IEnumerable<Student> list = _context.Students.ToList();
            return list;
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            try
            {
                Student student = _context.Students.Where(x => x.Id == id).FirstOrDefault();
                if (student!=null)
                {
                    return Ok(student);
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
        public IActionResult AddStudent([FromBody]Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Add(student);
                    _context.SaveChanges();
                    return Ok(GetStudent(student.Id));
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
        public IActionResult UpdateStudent([FromBody] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Students.Update(student);
                    _context.SaveChanges();
                    return Ok(GetStudent(student.Id));
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
                Student student = _context.Students.Where(a => a.Id == id).FirstOrDefault();

                if (student!=null)
                {
                    _context.Remove(student);
                    _context.SaveChanges();

                    return Ok("Student Deleted Successfully!");
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
