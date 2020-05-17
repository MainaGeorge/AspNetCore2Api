using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApi.DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        //SERIALIZING DATA USING JSON SERIALIZER
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            var departments = await _db.Departments
                .Include(d => d.Employees)
                .ToListAsync();

            if (!departments.Any()) return NotFound();


            var result = JsonConvert.SerializeObject(
                value: departments,
                formatting: Formatting.None,
                settings: new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            return Ok(result);

        }


        // SERIALIZING DATA USING LAMBDA EXPRESSIONS    
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        // {
        //     var departments = await _db.Departments.Include(d => d.Employees)
        //         .Select(d => new Department
        //         {
        //             Id = d.Id,
        //             Name = d.Name,
        //             Description = d.Description,
        //             Employees = d.Employees.Select(e => new Employee
        //             {
        //                 Name = e.Name,
        //                 Gender = e.Gender,
        //                 Id = e.Id,
        //             })
        //         })
        //         .ToListAsync();
        //
        //     if (departments.Any())
        //     {
        //         return Ok(departments);
        //     }
        //
        //     return NotFound();
        // }

        [HttpGet("[action]/{id:int}")]
        public async Task<ActionResult<Department>> GetById(int id)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if (department != null)
            {
                return Ok(department);
            }

            return NotFound();
        }

        [HttpGet("[action]/{name}")]
        public async Task<ActionResult<Department>> GetByName(string name)
        {
            var department = await _db.Departments.FirstOrDefaultAsync(d => d.Name == name);
            if (department != null)
            {
                return Ok(department);
            }

            return NotFound();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = await _db.Departments.FirstOrDefaultAsync(d => d.Id == id);

            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                _db.Remove(dept);

                await _db.SaveChangesAsync();
                return Ok(dept);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostDepartment([FromBody] Department dept)
        {
            if (ModelState.IsValid)
            {
                await _db.Departments.AddAsync(dept);

                await _db.SaveChangesAsync();

                return CreatedAtAction("GetById", new { id = dept.Id }, dept);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment([FromBody] Department department)
        {
            if (ModelState.IsValid)
            {
                var dept = await _db.Departments.AsNoTracking().FirstOrDefaultAsync(p => p.Id == department.Id);
                if (dept == null)
                {
                    return NotFound();
                }
                else
                {
                    _db.Departments.Update(department);

                    await _db.SaveChangesAsync();
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