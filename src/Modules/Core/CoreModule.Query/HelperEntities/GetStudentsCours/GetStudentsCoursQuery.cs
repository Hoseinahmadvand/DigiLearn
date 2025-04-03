using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.HelperEntities._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.HelperEntities.GetStudentsCours;

public record GetStudentsCoursQuery(Guid CourseId) : IQuery<List<StudentDto>>;

internal class GetStudentsCoursQueryHandler : IQueryHandler<GetStudentsCoursQuery, List<StudentDto>>
{
    private readonly QueryContext _context;

    public GetStudentsCoursQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<StudentDto>> Handle(GetStudentsCoursQuery request, CancellationToken cancellationToken)
    {
        var studentsId = await _context.CourseStudents
            .Where(cs => cs.CourseId == request.CourseId)
            .Select(u => u.UserId)
            .ToListAsync(cancellationToken);

        var students = await _context.Users.Where(u => studentsId.Contains(u.Id))
 .Select(cs => new StudentDto
 {
     Id = cs.Id,
     Name = cs.Name,
     Family = cs.Family,
     Email = cs.Email,
     PhoneNumber = cs.PhoneNumber,
     Avatar = cs.Avatar,
     RegistrationDate = cs.CreationDate
 })
 .ToListAsync(cancellationToken);


        return students;

    }
}
