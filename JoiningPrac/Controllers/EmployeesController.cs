using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JoiningPrac.Models;
using JoiningPrac.DTOs;

namespace JoiningPrac
{
    public class EmployeeJoiningInput
    {
        public int Eid { get; set; }
    }
    public class EmployeeJoiningOutput 
    {
        public int Eid { get; set; }
        public string Ename { get; set; }
        public string Dname { get; set; }
        public string Dgname { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ModelContext _context;

        public EmployeesController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployeess()
        {
            return await _context.Employeess.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> GetEmployees(int id)
        {
            var employees = await _context.Employeess.FindAsync(id);

            if (employees == null)
            {
                return NotFound();
            }

            return employees;
        }

        [HttpPost("JoiningData")]
        public async Task<ActionResult<ResponseDto>> JoinEmployee([FromBody] EmployeeJoiningInput input)
        {
            if (input.Eid == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto 
                {
                    Message = "Please Fill the Eid Field",
                    Success = false,
                    Payload = null
                });
            }
            List<EmployeeJoiningOutput> employeeJoiningOutputs =
                await (from em in _context.Employeess
                                        .Where(i => i.Eid == input.Eid)
                       from dept in _context.Departments
                                        .Where(i => em.Did == i.Did)
                       from des in _context.Designations
                                        .Where(i => em.Dgid == i.Dgid)
                       select new EmployeeJoiningOutput
                       {
                           Eid = em.Eid,
                           Ename = em.Ename,
                           Dname = dept.Dname,
                           Dgname = des.Dgname
                       }).OrderBy(i => i.Eid).ToListAsync();
            if (employeeJoiningOutputs.Count <= 0) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto 
                {
                    Message = "Data is not found for this ID",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto 
            {
                Message = "Joining Done",
                Success = true,
                Payload = employeeJoiningOutputs
            });
        }
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployees(int id, Employees employees)
        {
            if (id != employees.Eid)
            {
                return BadRequest();
            }

            _context.Entry(employees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employees>> PostEmployees(Employees employees)
        {
            _context.Employeess.Add(employees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployees", new { id = employees.Eid }, employees);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployees(int id)
        {
            var employees = await _context.Employeess.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            _context.Employeess.Remove(employees);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeesExists(int id)
        {
            return _context.Employeess.Any(e => e.Eid == id);
        }
    }
}
