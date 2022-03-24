using JetBrains.Annotations;
using System;

namespace Daridakr.ProgGuru
{
    [Serializable]
    public class TagEto
    {
        public Guid Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public int UsageCount { get; set; }
    }
}
