using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using Common.Application;

namespace BlogModule.Services;

public interface IBlogService
{
    Task<OperationResult> CreateCategory(CreateBlogCategoryCommand command);
    Task<OperationResult> EditCategory(EditBlogCategoryCommand command);
    Task<OperationResult> DeleteCategory(Guid categoryId);
    Task<List<BlogCategoryDto>> GetAllCategories();
    Task<BlogCategoryDto> GetCategoryById(Guid id);

    Task AddPostVisit(Guid id);
    Task<OperationResult> CreatePost(CreatePostCommand command);
    Task<OperationResult> EditPost(EditPostCommand command);
    Task<OperationResult> DeletePost(Guid postId);
    Task<BlogPostDto?> GetPostById(Guid postId);
    Task<BlogPostFilterItemDto?> GetPostBySlug(string slug);
    Task<BlogPostFilterResult> GetPostsByFilter(BlogPostFilterParams filterParams);
}
