using System.Collections.Generic;
using System.Linq;
using AspNetCore2Api.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentContext _db;

        public DepartmentController(DepartmentContext db)
        {
            _db = db;
        }

        public IEnumerable<Department> GetDepartments()
        {
            return _db.Departments.ToList();
        }
    }
}