using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Daridakr.ProgGuru.Entities.Base
{
    /// <summary>
    /// Abstract base class for entities containing title, description and some text information.
    /// </summary>
    public abstract class InformingEntity : AuditedAggregateRoot<Guid>
    {
        public string Title { get; protected set; }

        public string Subtitle { get; protected set; }

        public string TextInformation { get; protected set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected InformingEntity() { }

        /// <summary>
        /// This constructor is used for creating a new informing entity.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        internal InformingEntity(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation)
            : base(id)
        {
            SetCreator(creatorId);
            SetTitle(title);
            SetSubtitle(subtitle);
            SetTextInformation(textInformation);
        }

        /******* Initialization Methods ********************************************************************************/

        /// <summary>
        /// Correct creator initialize. Override this method to configure your rules for creator initialize.
        /// </summary>
        /// <param name="creatorId"></param>
        protected virtual void SetCreator([NotNull] Guid creatorId)
        {
            CreatorId = Check.NotNull(creatorId, nameof(creatorId));
        }

        /// <summary>
        /// Override this method for configure correct entity title initialize.
        /// </summary>
        /// <param name="name"></param>
        protected abstract void SetTitle([NotNull] string title);

        /// <summary>
        /// Override this method for configure correct entity subtitle initialize.
        /// </summary>
        /// <param name="name"></param>
        protected abstract void SetSubtitle([NotNull] string subtitle);

        /// <summary>
        /// Override this method for configure correct entity text information initialize.
        /// </summary>
        /// <param name="name"></param>
        protected abstract void SetTextInformation([NotNull] string textInformation);

        /******* Updating ********************************************************************************/

        /// <summary>
        /// Method for correct project title changing from domain layer.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        internal InformingEntity ChangeTitle([NotNull] string title)
        {
            SetTitle(title);
            return this;
        }

        /// <summary>
        /// Method for correct project subtitle changing from domain layer.
        /// </summary>
        /// <param name="subtitle"></param>
        /// <returns></returns>
        internal InformingEntity ChangeSubtitle([NotNull] string subtitle)
        {
            SetSubtitle(subtitle);
            return this;
        }

        /// <summary>
        /// Method for correct project text information changing from domain layer.
        /// </summary>
        /// <param name="textInformation"></param>
        internal InformingEntity ChangeTextInformation([NotNull] string textInformation)
        {
            SetTextInformation(textInformation);
            return this;
        }
    }
}
