using System;
using System.Runtime.Serialization;
using Volo.Abp;

namespace Volo.CmsKit.Blogs;

public class BlogPostSlugAlreadyExistException : BusinessException
{
    public BlogPostSlugAlreadyExistException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

    public BlogPostSlugAlreadyExistException(string slug)
    {
        Slug = slug;
        Code = CmsKitErrorCodes.BlogPosts.SlugAlreadyExist;

        WithData(nameof(Slug), Slug);
    }

    public virtual string Slug { get; }
}
