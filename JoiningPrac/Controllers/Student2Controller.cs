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
    public class SelfClass2 
    {
        public int Id { get; set; }
    }

    public class SelfClass3
    { 
        public decimal? Id { get; set; }
        public string Name { get; set; }
        public string DeptName { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class Student2Controller : ControllerBase
    {
        private readonly ModelContext _context;

        public Student2Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: api/Student2
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetStudent2s()
        {
            List<Student2> student2s = await _context.Student2s.ToListAsync();
            if (student2s.Count <= 0) 
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto 
                {
                    Message = "Student is not found",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Student is found",
                Success = true,
                Payload = student2s
            });
        }

        // GET: api/Student2/5
        [HttpPost("GetByStudentId")]
        public async Task<ActionResult<ResponseDto>> GetStudent2([FromBody]SelfClass2 input)
        {
            if (input.Id == 0) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up Id field",
                    Success = false,
                    Payload = null
                });
            }
            var student2 = await _context.Student2s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();


            if (student2 == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto 
                {
                    Message = "Student is not found in the database",
                    Success = false,
                    Payload = null
                });
            }

            return StatusCode(StatusCodes.Status200OK, new ResponseDto 
            {
                Message = "Student Data found",
                Success = true,
                Payload = student2
            });
        }

        // PUT: api/Student2/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("UpadteStudentData")]
        public async Task<ActionResult<ResponseDto>> PutStudent2([FromBody] Student2 input)
        {

            if (input.Id == 0 || input.Id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up Id field",
                    Success = false,
                    Payload = null
                });
            }

            if (input.Name == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up Name field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Deptid == null || input.Deptid == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up DeptID field",
                    Success = false,
                    Payload = null
                });
            }
            var student2s = await _context.Student2s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();

            if (student2s == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Students is not found",
                    Success = false,
                    Payload = null
                });
            }

            student2s.Name = input.Name;
            student2s.Deptid = input.Deptid;

             _context.Student2s.Update(student2s);

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto 
                {
                    Message = "Can't Update Student data cause Internal Server Error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Student Data found",
                Success = true,
                Payload = null
            });
        }

        // POST: api/Student2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("InsertNewStudent")]
        public async Task<ActionResult<ResponseDto>> PostStudent2([FromBody]Student2 input)
        {

            if (input.Id == 0 || input.Id == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up Id field",
                    Success = false,
                    Payload = null
                });
            }

            if (input.Name == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up Name field",
                    Success = false,
                    Payload = null
                });
            }
            if (input.Deptid == 0 || input.Deptid == null) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Fill up DeptId field",
                    Success = false,
                    Payload = null
                });
            }

            var student2s = await _context.Student2s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            _context.Student2s.Add(input);

            if (student2s != null) 
            {
                return StatusCode(StatusCodes.Status409Conflict, new ResponseDto
                {
                    Message = "Already in the database",
                    Success = false,
                    Payload = null
                });
            }

            bool isSaved = await _context.SaveChangesAsync() > 0;
            if (isSaved == false) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto
                {
                    Message = "Can't Insert Student data cause Internal Server Error",
                    Success = false,
                    Payload = null
                });
            }
            return StatusCode(StatusCodes.Status200OK, new ResponseDto
            {
                Message = "Student Data Added",
                Success = true,
                Payload = null
            });
        }

        // DELETE: api/Student2/5
        [HttpPost("DeleteStudent")]
        public async Task<ActionResult<ResponseDto>> DeleteStudent2([FromBody]SelfClass2 input)
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


            var student2s = await _context.Student2s.Where(i => i.Id == input.Id).FirstOrDefaultAsync();
            if (student2s == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseDto
                {
                    Message = "Data is not found",
                    Success = false,
                    Payload = null
                });
            }

            _context.Student2s.Remove(student2s);
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
        [HttpPost("CustomJoining")]
        public async Task<ActionResult<ResponseDto>> Joining([FromBody] SelfClass2 Input)
        {
            if (Input.Id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDto
                {
                    Message = "Please Input the ID field",
                    Success = false,
                    Payload = null
                });
            }

            List<SelfClass3> selfClass3s = await (from stu2 in _context.Student2s
                                                            .Where(i => i.Id == Input.Id)
                                                  from dept in _context.Depts
                                                            .Where(i => stu2.Deptid == i.Id)
                                                  select new SelfClass3 
                                                  {
                                                      Id = stu2.Id,
                                                      Name = stu2.Name,
                                                      DeptName = dept.Name
                                                  })
                                                            .ToListAsync();

            if (selfClass3s.Count <= 0) 
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
                Message = "Joining",
                Success = true,
                Payload = selfClass3s
            });


        }

        private bool Student2Exists(decimal? id)
        {
            return _context.Student2s.Any(e => e.Id == id);
        }
    }
}
