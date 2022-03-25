using JetBrains.Annotations;
using System;
using System.Text.RegularExpressions;
using Volo.Abp;

namespace Daridakr.ProgGuru.Helpers
{
    public class ProgGuruCheck
    {
        public static string IsStartsWithSeparatorChar([NotNull] string value, string parameterName)
        {
            Check.NotNull(value, nameof(value));
            if (char.IsSeparator(value[0])) throw new ArgumentException($"The {parameterName} cannot start with a separator character", parameterName);
            return value;
        }

        public static string IsContainsSpecChar([NotNull] string value, string parameterName)
        {
            Check.NotNull(value, nameof(value));
            if (!Regex.IsMatch(value, @"\W+")) throw new ArgumentException($"A {parameterName} can only contain letters and numbers", parameterName);
            return value;
        }

        public static int IsCorrectNum([NotNull] int value, string parameterName)
        {
            Check.NotNull(value, nameof(value));
            if (value < 0) throw new ArgumentException($"A {parameterName} can't be a negative", parameterName);
            return value;
        }
    }
}
