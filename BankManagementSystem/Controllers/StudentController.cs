//using BankManagementSystem.Entity.Models;
//using BankManagementSystem.Services.Repository;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace BankManagementSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private readonly IStudentRespository _studentRespository;

//        public StudentController(IStudentRespository studentRespository)
//        {
//            _studentRespository = studentRespository;
//        }
//        // GET: api/<StudentController>
//        [HttpGet("GetAllStudents")]
//        public async Task<IActionResult> GetAllStudents()
//        {
//            var students = await _studentRespository.GetAllStudentsAsync();
//            if (students == null)
//            {
//                return NotFound();
//            }
//            return Ok(students);
//        }

//        //GET api/<StudentController>/5
//        [HttpGet("GetStudentById")]
//        public async Task<IActionResult> GetStudentById(int id)
//        {
//            var student = await _studentRespository.GetStudentByIdAsync(id);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            return Ok(student);
//        }

//        //// POST api/<StudentController>
//        [HttpPost("AddStudent")]
//        public async Task<IActionResult> AddStudent([FromBody] Student student)
//        {
//            await _studentRespository.AddStudentAsync(student);
//            return Ok();
//        }

//        // PUT api/<StudentController>/5
//        [HttpPut("UpdateStudent")]
//        public async Task<IActionResult> UpdateStudent([FromBody] Student student)
//        {
//            await _studentRespository.UpdateStudentAsync(student);
//            return Ok();
//        }

//        //// DELETE api/<StudentController>/5
//        [HttpDelete("DeleteStudent")]
//        public async Task<IActionResult> DeleteStudent(int id)
//        {
//            await _studentRespository.DeleteStudentAsync(id);
//            return Ok();
//        }
//    }
//}
