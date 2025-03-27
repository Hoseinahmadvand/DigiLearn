using Common.Domain.Repository;

namespace CoreModule.Domain.Teachers.Repository;

public interface ITeacherRepository : IBaseRepository<Models.Teacher>
{
    void Delete(Models.Teacher teacher);
}