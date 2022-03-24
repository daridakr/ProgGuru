using Daridakr.ProgGuru.Base;
using System;

namespace Daridakr.ProgGuru.Events
{
    public class EventDto : PostEntityDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Cost { get; set; }

        public string UserName { get; set; }

        //public ICollection<EventUser> Users { get; set; }
    }
}
