using System.Collections.Generic;
using System.Linq;
using AspNetCore2Api.DataAccessLayer;
using AspNetCoreApi.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentContext _db;

        public DepartmentsController(DepartmentContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            var departments = _db.Departments.ToList();

            if (departments.Any())
            {
                return Ok(departments);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Department> GetDepartment(int id)
        {
            var department = _db.Departments.FirstOrDefault(d => d.Id == id);
            if (department != null)
            {
                return Ok(department);
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var dept = _db.Departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                _db.Remove(dept);

                _db.SaveChanges();
                return NoContent();
            }
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] Department dept)
        {
            if (ModelState.IsValid)
            {
                _db.Departments.Add(dept);

                _db.SaveChanges();

                return CreatedAtAction("GetDepartment", new { id = dept.Id }, dept);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public IActionResult PutDepartment([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                var dept = _db.Departments.AsNoTracking().FirstOrDefault(p => p.Id == department.Id);
                if (dept == null)
                {
                    return NotFound();
                }
                else
                {
                    _db.Departments.Update(department);

                    _db.SaveChanges();
                    return NoContent();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}