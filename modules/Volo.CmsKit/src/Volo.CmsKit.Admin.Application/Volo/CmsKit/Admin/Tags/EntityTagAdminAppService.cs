using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Tags;

namespace Volo.CmsKit.Admin.Tags;

[RequiresGlobalFeature(typeof(TagsFeature))]
public class EntityTagAdminAppService : CmsKitAdminAppServiceBase, IEntityTagAdminAppService
{
    protected ITagDefinitionStore TagDefinitionStore { get; }
    protected EntityTagManager EntityTagManager { get; }
    protected TagManager TagManager { get; }
    protected ITagRepository TagRepository { get; }
    protected IEntityTagRepository EntityTagRepository { get; }

    public EntityTagAdminAppService(
        ITagDefinitionStore tagDefinitionStore,
        EntityTagManager entityTagManager,
        TagManager tagManager,
        ITagRepository tagRepository,
        IEntityTagRepository entityTagRepository)
    {
        TagDefinitionStore = tagDefinitionStore;
        EntityTagManager = entityTagManager;
        TagManager = tagManager;
        TagRepository = tagRepository;
        EntityTagRepository = entityTagRepository;
    }

    public virtual async Task AddTagToEntityAsync(EntityTagCreateDto input)
    {
        var tag = await TagManager.GetOrAddAsync(input.EntityType, input.TagName);

        await EntityTagManager.AddTagToEntityAsync(
            tag.Id,
            input.EntityType,
            input.EntityId,
            CurrentTenant?.Id);
    }

    public virtual async Task RemoveTagFromEntityAsync(EntityTagRemoveDto input)
    {
        await EntityTagManager.RemoveTagFromEntityAsync(
            input.TagId,
            input.EntityType,
            input.EntityId,
            CurrentTenant?.Id);
    }

    public virtual async Task SetEntityTagsAsync(EntityTagSetDto input)
    {
        await EntityTagManager.SetEntityTagsAsync(input.EntityType, input.EntityId, input.Tags);
    }

    public virtual async Task RemoveEntityTagsAsync(EntityTagRemoveDto input)
    {
        await EntityTagManager.RemoveEntityTagsAsync(input.EntityType, input.EntityId);
    }
}
