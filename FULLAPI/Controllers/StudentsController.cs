using Microsoft.AspNetCore.Mvc;
using FULLAPI.DTOs;
using FULLAPI.Mappings;
using FULLAPI.Models;

namespace FULLAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _students = new()
    {
        new Student
        {
            Id = 1,
            StudentNumber = "2025-0001",
            LastName = "Styles",
            FirstName = "Kulas",
            Gender = "Male",
            Address = "Bataan",
            Birthday = new DateTime(2006, 8, 9),
            Birthplace = "Valnzuela"
        }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Student>> GetAll()
    {
        return Ok(_students);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Student> GetById([FromRoute] int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound(new { Message = $"Student with ID {id} not found." });

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create([FromBody] CreateStudentDto dto)
    {
        if (dto == null) return BadRequest("Invalid payload.");

        int nextId = _students.Any() ? _students.Max(s => s.Id) + 1 : 1;
        var newStudent = dto.ToEntity(nextId);

        _students.Add(newStudent);
        return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStudentDto dto)
    {
        if (dto == null) return BadRequest("Invalid payload.");

        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound(new { Message = $"Student with ID {id} not found." });

        student.UpdateEntity(dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null) return NotFound(new { Message = $"Student with ID {id} not found." });

        _students.Remove(student);
        return NoContent();
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<Student>> Search([FromQuery] string? lastName, [FromQuery] string? firstName)
    {
        var query = _students.AsQueryable();

        if (!string.IsNullOrWhiteSpace(lastName))
        {
            query = query.Where(s => s.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(firstName))
        {
            query = query.Where(s => s.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(query.ToList());
    }
}