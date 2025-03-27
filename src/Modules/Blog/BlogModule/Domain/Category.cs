using Common.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogModule.Domain;

[Table("Category", Schema = "blog")]
class Category : BaseEntity
{
    public string Title { get; set; }
    public string Slug { get; set; }
}
