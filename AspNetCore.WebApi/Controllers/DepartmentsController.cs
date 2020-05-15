using System.Collections.Generic;
using System.Linq;
using AspNetCore2Api.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentContext _db;

        public DepartmentsController(DepartmentContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Department> GetDepartments()
        {
            return _db.Departments.ToList();
        }

        [HttpGet("{id:int}")]
        public Department GetDepartment(int id)
        {
            return _db.Departments.FirstOrDefault(d => d.Id == id);
        }

        [HttpDelete("{id:int}")]
        public string DeleteDepartment(int id)
        {
            var dept = _db.Departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return "No item with that id";
            }
            else
            {
                _db.Remove<Department>(dept);

                _db.SaveChanges();
                return "Delete successful";
            }
        }
    }
}