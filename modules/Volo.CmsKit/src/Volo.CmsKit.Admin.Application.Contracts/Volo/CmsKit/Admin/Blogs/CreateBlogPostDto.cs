﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;
using Volo.CmsKit.Blogs;

namespace Volo.CmsKit.Admin.Blogs;

[Serializable]
public class CreateBlogPostDto
{
    [Required]
    public Guid CreatorId { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    [Required]
    [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxTitleLength))]
    public string Title { get; set; }

    [Required]
    [DynamicStringLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxSlugLength), nameof(BlogPostConsts.MinSlugLength))]
    public string Slug { get; set; }

    [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxShortDescriptionLength))]
    public string ShortDescription { get; set; }

    [DynamicMaxLength(typeof(BlogPostConsts), nameof(BlogPostConsts.MaxContentLength))]
    public string Content { get; set; }

    [DataType(DataType.ImageUrl)]
    public string CoverImagePath { get; set; }
}
