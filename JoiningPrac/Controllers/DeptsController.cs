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
    public class SelfClass1 
    {
        public int Id { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class DeptsController : ControllerBase
    {
        private readonly ModelContext _context;

        public DeptsController(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Depts
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetDepts()
        {
            List<Dept>depts = await _context.Depts.ToListAsync();
            if (depts.Count <= 0) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Dept is not found!",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Dept Found",
                Success = true,
                Payload = depts
            });
        }

        // GET: api/Depts/5
        [HttpPost("GetByDeptId")]
        public async Task<ActionResult<ResponseDto>> GetDept([FromBody] SelfClass1 input)
        {
            if (input.Id == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp ID field!",
                    Success = false,
                    Payload = null
                });
            }
            var depts = await _context.Depts.FirstOrDefaultAsync();

            if (depts == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Dept is not found!",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Dept Found",
                Success = true,
                Payload = depts
            });
        }

        // PUT: api/Depts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UpdateDept")]
        public async Task<ActionResult<ResponseDto>> PutDept([FromBody] Dept input)
        {
            if (input.Id == 0 || input.Id == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp ID field!",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Name == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp Name field!",
                    Success = false,
                    Payload = null
                });
            }

            var depts = await _context.Depts.Where(i => i.Id == input.Id).FirstOrDefaultAsync();

            if (depts == null) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Dept is not found",
                    Success = false,
                    Payload = null
                });
            }

            depts.Id = input.Id;
            depts.Name = input.Name;

            _context.Depts.Update(depts);
            bool isSaved =await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Data is not updated for the server error",
                    Success = false,
                    Payload = null
                });
            }
            

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data is Found",
                Success = true,
                Payload = null
            });
        }

        // POST: api/Depts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InsertNewDept")]
        public async Task<ActionResult<ResponseDto>> PostDept([FromBody]Dept input)
        {
            if (input.Id == 0 || input.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp ID field!",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp Name field!",
                    Success = false,
                    Payload = null
                });
            }
            var depts = await _context.Depts.Where(i => i.Id == input.Id).FirstOrDefaultAsync();

            if (depts != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "Dept is already in the database",
                    Success = false,
                    Payload = null
                });
            }
            _context.Depts.Add(input);

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Dept is not inserted cause Internal server error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data Inserted",
                Success = true,
                Payload = null
            });
        }

        // DELETE: api/Depts/5
        [HttpDelete("DeleteDept")]
        public async Task<ActionResult<ResponseDto>> DeleteDept([FromBody] SelfClass1 input)
        {
            if (input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please FillUp ID field!",
                    Success = false,
                    Payload = null
                });
            }


            var dept = await _context.Depts.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (dept == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Data is not found",
                    Success = false,
                    Payload = null
                });
            }

            _context.Depts.Remove(dept);
            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Dept is not deleted cause Internal server error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Data Deleted",
                Success = true,
                Payload = null
            });
        }

        private bool DeptExists(decimal? id)
        {
            return _context.Depts.Any(e => e.Id == id);
        }
    }
}
