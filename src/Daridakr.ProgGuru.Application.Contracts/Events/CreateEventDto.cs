using Daridakr.ProgGuru.Base;
using System;

namespace Daridakr.ProgGuru.Events
{
    public class CreateEventDto : CreatePostEntityDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Cost { get; set; }
    }
}
