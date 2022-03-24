using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Volo.CmsKit.Admin.Blogs;

[Serializable]
public class BlogPostDto : AuditedEntityDto<Guid>, IHasCreationTime, IHasModificationTime, IHasConcurrencyStamp
{
    public Guid GroupId { get; set; }

    public string Title { get; set; }

    public string Slug { get; set; }

    public string ShortDescription { get; set; }

    public string Content { get; set; }

    public string CoverImagePath { get; set; }

    public string ConcurrencyStamp { get; set; }
}
