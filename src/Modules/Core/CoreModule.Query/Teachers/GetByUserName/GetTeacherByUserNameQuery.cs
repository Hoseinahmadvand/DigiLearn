using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Teachers._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teachers.GetByUserName;

public record GetTeacherByUserNameQuery(string Username) : IQuery<TeacherDto?>;

class GetTeacherByUserNameQueryHandler : IQueryHandler<GetTeacherByUserNameQuery, TeacherDto?>
{
    private readonly QueryContext _context;

    public GetTeacherByUserNameQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<TeacherDto?> Handle(GetTeacherByUserNameQuery request, CancellationToken cancellationToken)
    {
        var model = await _context.Teachers
     .Include(c => c.User)
     .FirstOrDefaultAsync(f => f.UserName == request.Username, cancellationToken: cancellationToken);

        if (model == null)
        {
            return null;
        }

        return new TeacherDto
        {
            Id = model.Id,
            CreationDate = model.CreationDate,
            UserName = model.UserName,
            CvFileName = model.CvFileName,
            Status = model.Status,
            Biography = model.Biography,
            Description = model.Description,
            User = new CoreModuleUserDto
            {
                Id = model.User.Id,
                CreationDate = model.User.CreationDate,
                Avatar = model.User.Avatar,
                Name = model.User.Name,
                Family = model.User.Family,
                Email = model.User.Email,
                PhoneNumber = model.User.PhoneNumber
            }
        };
    }
}