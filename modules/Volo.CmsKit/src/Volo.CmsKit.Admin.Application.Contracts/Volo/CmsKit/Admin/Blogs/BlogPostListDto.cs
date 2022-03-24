using System;
using Volo.Abp.Application.Dtos;

namespace Volo.CmsKit.Admin.Blogs;

[Serializable]
public class BlogPostListDto : AuditedEntityDto<Guid>
{
    public Guid GroupId { get; set; }

    public string GroupTitle { get; set; }

    public string CreatorUserName { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string CoverImagePath { get; set; }
}
