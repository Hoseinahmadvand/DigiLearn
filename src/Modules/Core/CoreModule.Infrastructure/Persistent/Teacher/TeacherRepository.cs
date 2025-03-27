using Common.Infrastructure.Repository;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Infrastructure.Persistent.Teacher;

class TeacherRepository : BaseRepository<Domain.Teachers.Models.Teacher,CoreModuleEfContext>,ITeacherRepository
{
    public TeacherRepository(CoreModuleEfContext context) : base(context)
    {
    }

    public void Delete(Domain.Teachers.Models.Teacher teacher)
    {
        Context.Remove(teacher);
    }
}