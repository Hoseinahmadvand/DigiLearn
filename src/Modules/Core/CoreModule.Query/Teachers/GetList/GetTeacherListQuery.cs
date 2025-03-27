using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query._DTOs;
using CoreModule.Query.Teachers._DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Teachers.GetList;

public class GetTeacherListQuery : IQuery<List<TeacherDto>>
{

}

class GetTeacherListQueryHandler : IQueryHandler<GetTeacherListQuery, List<TeacherDto>>
{
    private readonly QueryContext _context;

    public GetTeacherListQueryHandler(QueryContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherDto>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
    {
        return await _context.Teachers.Include(c => c.User)
            .Select(model => new TeacherDto()
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
            }).ToListAsync(cancellationToken);
    }
}