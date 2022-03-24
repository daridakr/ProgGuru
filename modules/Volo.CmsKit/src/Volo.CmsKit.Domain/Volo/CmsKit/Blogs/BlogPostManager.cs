using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Volo.CmsKit.Blogs;

public class BlogPostManager : DomainService
{
    protected IBlogPostRepository BlogPostRepository { get; }
    protected IBlogRepository BlogRepository { get; }
    protected IDefaultBlogFeatureProvider BlogFeatureProvider { get; }


    public BlogPostManager(
        IBlogPostRepository blogPostRepository,
        IBlogRepository blogRepository,
        IDefaultBlogFeatureProvider blogFeatureProvider)
    {
        BlogPostRepository = blogPostRepository;
        BlogRepository = blogRepository;
        BlogFeatureProvider = blogFeatureProvider;
    }

    public virtual async Task<BlogPost> CreateAsync(
        [NotNull] Guid creatorId,
        [NotNull] Guid groupId,
        [NotNull] string title,
        [NotNull] string slug,
        [CanBeNull] string shortDescription = null,
        [CanBeNull] string content = null,
        [CanBeNull] string coverImagePath = null)
    {
        Check.NotNull(groupId, nameof(groupId));
        Check.NotNull(creatorId, nameof(creatorId));
        Check.NotNullOrEmpty(title, nameof(title));
        Check.NotNullOrEmpty(slug, nameof(slug));

        var blogPost = new BlogPost(
                    GuidGenerator.Create(),
                    groupId,
                    creatorId,
                    title,
                    slug,
                    shortDescription,
                    content,
                    coverImagePath,
                    CurrentTenant.Id
                    );

        await CheckSlugExistenceAsync(blogPost.Slug);

        return blogPost;
    }

    public virtual async Task SetSlugUrlAsync(BlogPost blogPost, [NotNull] string newSlug)
    {
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug));

        await CheckSlugExistenceAsync(newSlug);

        blogPost.SetSlug(newSlug);
    }

    protected virtual async Task CheckSlugExistenceAsync(string slug)
    {
        if (await BlogPostRepository.SlugExistsAsync(slug))
        {
            throw new BlogPostSlugAlreadyExistException(slug);
        }
    }
}
