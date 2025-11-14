using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> Students = new List<Student>
        {
            new Student{roll_no=1,name="Ronit",fees=10000,branch="Ict"},
            new Student{roll_no=2,name="ABC",fees=20000,branch="CE"}
        };
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Students;
        }
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = Students.FirstOrDefault(s => s.roll_no == id);
            if(student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            if(Students.Any(s=>s.roll_no== student.roll_no))
            {
                return BadRequest("Student with the same  roll no alredy exists.");
            }
            Students.Add(student);
            return CreatedAtAction("GetStudent", new { id = student.roll_no }, student);
        }

        [HttpPut("{id")]
        public IActionResult PutStudent (int id, Student student)
        {
            var existingStudent = Students.FirstOrDefault(s => s.roll_no == id);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.name = student.name;
            existingStudent.branch = student.branch;
            existingStudent.fees = student.fees;

            return NoContent();
        }
    }
}
