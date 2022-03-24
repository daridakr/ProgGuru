using Daridakr.ProgGuru.Entities.Groups;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Identity;
using Volo.CmsKit.Blogs;
using Volo.CmsKit.Comments;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Public.Comments;

namespace Volo.CmsKit.Public.Blogs;

[RequiresGlobalFeature(typeof(BlogsFeature))]
public class BlogPostPublicAppService : CmsKitPublicAppServiceBase, IBlogPostPublicAppService
{
    protected IBlogRepository BlogRepository { get; }

    protected IBlogPostRepository BlogPostRepository { get; }

    protected IGroupRepository GroupRepository { get; }

    protected IIdentityUserRepository UserRepository { get; }

    protected ICommentPublicAppService CommentPublicAppService { get; }

    protected ICommentRepository CommentRepository { get; }

    public BlogPostPublicAppService(
        IBlogRepository blogRepository,
        IBlogPostRepository blogPostRepository,
        IGroupRepository groupRepository,
        IIdentityUserRepository userRepository,
        ICommentPublicAppService commentPublicAppService,
        ICommentRepository commentRepository)
    {
        BlogRepository = blogRepository;
        BlogPostRepository = blogPostRepository;
        GroupRepository = groupRepository;
        UserRepository = userRepository;
        CommentRepository = commentRepository;
    }

    public virtual async Task<BlogPostPublicDto> GetAsync([NotNull] string blogPostSlug)
    {
        var blogPost = await BlogPostRepository.GetBySlugAsync(blogPostSlug);
        var group = await GroupRepository.GetAsync(blogPost.GroupId);
        var user = await UserRepository.GetAsync((Guid)blogPost.CreatorId);

        var blogPostDto = ObjectMapper.Map<BlogPost, BlogPostPublicDto>(blogPost);

        blogPostDto.GroupTitle = group.Title;
        blogPostDto.CreatorName = $" {user.Name} {user.Surname}";
        blogPostDto.CreatorUserName = user.UserName;
        blogPostDto.CreatorProfilePicture = (string)user.ExtraProperties["ProfilePictureUrl"];
        var blogsComments = (await CommentRepository.GetListWithAuthorsAsync("BlogPost", blogPostDto.Id.ToString()));
        blogPostDto.CommentCount = blogsComments.Count();

        return blogPostDto;
    }

    public virtual async Task<PagedResultDto<BlogPostPublicDto>> GetListAsync(PagedAndSortedResultRequestDto input, string filter = null)
    {
        var groups = (await GroupRepository.GetListAsync()).ToDictionary(x => x.Id);

        var users = (await UserRepository.GetListAsync()).ToDictionary(x => x.Id);

        var comments = (await CommentRepository.GetListAsync(null, "BlogPost"));

        var blogPosts = await BlogPostRepository.GetListAsync(filter, null, input.MaxResultCount, input.SkipCount, input.Sorting);

        var dtoList = blogPosts.Select(x =>
        {
            var dto = ObjectMapper.Map<BlogPost, BlogPostPublicDto>(x);

            dto.GroupTitle = groups[x.GroupId].Title;
            dto.CreatorName = $" {users[(Guid)x.CreatorId].Name} {users[(Guid)x.CreatorId].Surname}";
            dto.CreatorUserName = users[(Guid)x.CreatorId].UserName;
            dto.CreatorProfilePicture = (string)users[(Guid)x.CreatorId].ExtraProperties["ProfilePictureUrl"];
            var dtoComments = comments.Where(c => c.Comment.EntityId == x.Id.ToString());
            dto.CommentCount = dtoComments.Count();

            return dto;
        }).ToList();

        return new PagedResultDto<BlogPostPublicDto>(
            await BlogPostRepository.GetCountAsync(),
            dtoList);
    }
}
