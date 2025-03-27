using Common.Infrastructure.Repository;
using CoreModule.Domain.Courses.Repository;

namespace CoreModule.Infrastructure.Persistent.Course;

public class CourseRepository:BaseRepository<Domain.Courses.Models.Course,CoreModuleEfContext> , ICourseRepository
{
    public CourseRepository(CoreModuleEfContext context) : base(context)
    {
    }
}