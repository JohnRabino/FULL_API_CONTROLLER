using FULLAPI.DTOs;
using FULLAPI.Models;

namespace FULLAPI.Mappings;

public static class StudentMapping
{
    public static Student ToEntity(this CreateStudentDto dto, int newId)
    {
        return new Student
        {
            Id = newId,
            StudentNumber = dto.StudentNumber,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Gender = dto.Gender,
            Address = dto.Address,
            Birthday = dto.Birthday,
            Birthplace = dto.Birthplace
        };
    }

    public static void UpdateEntity(this Student student, UpdateStudentDto dto)
    {
        student.LastName = dto.LastName;
        student.FirstName = dto.FirstName;
        student.Gender = dto.Gender;
        student.Address = dto.Address;
        student.Birthday = dto.Birthday;
        student.Birthplace = dto.Birthplace;
    }
}