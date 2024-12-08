using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public EmployeeController(DatabaseContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeModel>> GetEmployees()
        {
            if(_context.Employees == null)
            {
                return NotFound();
            }
            else
            {
                return Ok( _context.Employees.ToList());
            }
        }

        [HttpGet("{id:int}", Name = "GetbyId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<EmployeeModel>> GetEmployees(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var result = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok( result );
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EmployeeModel>> AddEmployee(EmployeeModel employee)
        {
            if(_context.Employees.FirstOrDefault(u=>u.Name.ToLower() == employee.Name.ToLower())!=null)
                {
                ModelState.AddModelError("CustomError", "Employee already Exists");
                return BadRequest(ModelState);
            }
            if (employee == null) 
            {
                return BadRequest();
            }
            if (employee.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                _context.Employees.Add(employee);
                _context.SaveChanges(); 
            }

            return CreatedAtRoute("GetbyId", new { id = employee.Id }, employee);
        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEmployee(int id) 
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var data = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (data == null)
            {
                return NotFound();
            }
            else
            {
                _context.Employees.Remove(data);
                _context.SaveChanges();
            }
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult updateEmployee(EmployeeModel model, int id) 
        {
            if(model == null || id != model.Id)
            {
                return BadRequest();
            }
            _context.Employees.Update(model);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "PartialUpdate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult partiallyUpdateEmployee([FromBody] JsonPatchDocument<EmployeeModel> patchmodel, int id)
        {
            if (patchmodel == null || id <= 0)
            {
                return BadRequest();
            }
            var res = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (res == null)
            {
                return NotFound();
            }
            patchmodel.ApplyTo(res, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.SaveChanges();
            return NoContent();
        }
    }
}
