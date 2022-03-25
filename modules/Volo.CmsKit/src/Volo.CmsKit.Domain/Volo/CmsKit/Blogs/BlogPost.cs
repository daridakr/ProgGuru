﻿using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.CmsKit.Users;

namespace Volo.CmsKit.Blogs;

public class BlogPost : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    /* CreatorId */

    //public virtual Guid BlogId { get; protected set; }

    public virtual Guid GroupId { get; protected set; }

    [NotNull]
    public virtual string Title { get; protected set; }

    [NotNull]
    public virtual string Slug { get; protected set; }

    [NotNull]
    public virtual string ShortDescription { get; protected set; }

    public virtual string Content { get; protected set; }

    public string CoverImagePath { get; set; }

    public virtual Guid? TenantId { get; protected set; }

    //public Guid AuthorId { get; set; }

    //public virtual CmsUser Author { get; set; }

    protected BlogPost()
    {
    }

    internal BlogPost(
        Guid id,
        Guid groupId,
        Guid creatorId,
        [NotNull] string title,
        [NotNull] string slug,
        [CanBeNull] string shortDescription = null,
        [CanBeNull] string content = null,
        [CanBeNull] string coverImagePath = null,
        [CanBeNull] Guid? tenantId = null) : base(id)
    {
        TenantId = tenantId;
        GroupId = groupId;
        CreatorId = creatorId;
        SetTitle(title);
        SetSlug(slug);
        SetShortDescription(shortDescription);
        SetContent(content);
        CoverImagePath = coverImagePath;
    }

    public virtual void SetTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), BlogPostConsts.MaxTitleLength);
    }

    internal void SetSlug(string slug)
    {
        Check.NotNullOrWhiteSpace(slug, nameof(slug), BlogPostConsts.MaxSlugLength, BlogPostConsts.MinSlugLength);

        Slug = SlugNormalizer.Normalize(slug);
    }

    public virtual void SetShortDescription(string shortDescription)
    {
        ShortDescription = Check.Length(shortDescription, nameof(shortDescription), BlogPostConsts.MaxShortDescriptionLength);
    }

    public virtual void SetContent(string content)
    {
        Content = Check.Length(content, nameof(content), BlogPostConsts.MaxContentLength);
    }
}