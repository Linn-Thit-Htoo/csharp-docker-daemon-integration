
namespace MigrationCore.AppDbContextModels;

public partial class TblBlog
{
    public string BlogId { get; set; } = null!;

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
