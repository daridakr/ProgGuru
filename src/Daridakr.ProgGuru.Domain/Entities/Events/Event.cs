using Daridakr.ProgGuru.Entities.Base;
using Daridakr.ProgGuru.Helpers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;

namespace Daridakr.ProgGuru.Entities.Events
{
    public class Event : PostEntity
    {
        /* Creator, Title, Subtitle, TextInformation, Views, Likes, Comments, SavesAmount, CoverImagePath, Tags */

        public DateTime StartDate { get; private set; }

        public DateTime? EndDate { get; private set; }

        public double Cost { get; set; }

        /******* Initialization ********************************************************************************/

        /// <summary>
        /// This constructor is used by the ORM/database provider.
        /// </summary>
        protected Event() : base() { }

        /// <summary>
        /// This constructor is used for creating new event.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creatorId"></param>
        /// <param name="title"></param>
        /// <param name="subtitle"></param>
        /// <param name="textInformation"></param>
        /// <param name="coverImagePath"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="cost"></param>
        internal Event(
            [NotNull] Guid id,
            [NotNull] Guid creatorId,
            [NotNull] string title,
            [NotNull] string subtitle,
            [NotNull] string textInformation,
            [NotNull] string coverImagePath,
            [NotNull] DateTime startDate,
            [CanBeNull] DateTime? endDate = null,
            int cost = 0)
             : base(id, creatorId, title, subtitle, textInformation, coverImagePath)
        {
            SetStartDate(startDate);
            EndDate = endDate;
            if (cost != 0) SetCost(cost);
        }

        /******* Business-Logic ********************************************************************************/

        /// <summary>
        /// Method for correct event start date initialize only for constructor.
        /// </summary>
        /// <param name="startDate"></param>
        protected void SetStartDate([NotNull] DateTime startDate)
        {
            StartDate = Check.NotNull(startDate, nameof(startDate));
        }

        /// <summary>
        /// Method for correct event cost initialize only for constructor.
        /// </summary>
        /// <param name="cost"></param>
        protected void SetCost([NotNull] int cost)
        {
            Cost = ProgGuruCheck.IsCorrectNum(cost, nameof(cost));
        }

        /***********************************************************************************************************************/

        /// <summary>
        /// Method for correct event start date changing from domain layer.
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        internal Event ChangeStartDate([NotNull] DateTime startDate)
        {
            SetStartDate(startDate);
            return this;
        }

        /// <summary>
        /// Method for correct event start date changing from domain layer.
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal Event ChangeEndDate([CanBeNull] DateTime? endDate)
        {
            EndDate = endDate;
            return this;
        }

        /// <summary>
        /// Method for correct event cost date changing from domain layer.
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        internal Event ChangeCost([NotNull] int cost)
        {
            SetCost(cost);
            return this;
        }
    }
}
