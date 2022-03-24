using System;
using Volo.Abp.Application.Dtos;

namespace Volo.CmsKit.Public.Blogs;

[Serializable]
public class BlogPostPublicDto : AuditedEntityDto<Guid>
{
    public Guid GroupId { get; set; }

    public string GroupTitle { get; set; }

    public string CreatorName { get; set; }

    public string CreatorUserName { get; set; }

    public string CreatorProfilePicture { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string CoverImagePath { get; set; }

    public int CommentCount { get; set; }

    //public CmsUserDto Author { get; set; }
}
