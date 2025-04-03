using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreModule.Query._Data.Entities;

[Table("CourseStudents", Schema = "dbo")]
public class CourseStudentModel : BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid UserId { get; set; }
}

