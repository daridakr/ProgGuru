using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Daridakr.ProgGuru
{
    public static class UserExtensions
    {
        public const string CurrentLocation = "CurrentLocation";

        public static void SetCurrentLocation(this IdentityUser user, string location)
        {
            user.SetProperty(CurrentLocation, location);
        }

        public static string GetCurrentLocation(this IdentityUser user)
        {
            return user.GetProperty<string>(CurrentLocation);
        }
    }
}
