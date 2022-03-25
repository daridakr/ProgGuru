using JetBrains.Annotations;
using System.Globalization;

namespace Daridakr.ProgGuru.Helpers
{
    public class ProgGuruConverter
    {
        public static string ConvertToPostTitle([NotNull] string title)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(title);
        }
    }
}
