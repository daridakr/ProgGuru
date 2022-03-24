using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Daridakr.ProgGuru;

public static class ProgGuruDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
    .AddOrUpdateProperty<IdentityUserDto, string>(
        "GithubUsername"
    );

            ObjectExtensionManager.Instance
    .AddOrUpdateProperty<IdentityUserDto, string>(
        "TelegramUsername"
    );

            ObjectExtensionManager.Instance
    .AddOrUpdateProperty<IdentityUserDto, string>(
        "DiscordCode"
    );


            //ObjectExtensionManager.Instance.AddOrUpdateProperty<string>(
            //    new[]
            //    {
            //        typeof(IdentityUserDto),
            //        typeof(IdentityUserCreateDto),
            //        typeof(IdentityUserUpdateDto)
            //    },
            //    "BeginningYear"
            //    );

            /* You can add extension properties to DTOs
             * defined in the depended modules.
             *
             * Example:
             *
             * ObjectExtensionManager.Instance
             *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
             *
             * See the documentation for more:
             * https://docs.abp.io/en/abp/latest/Object-Extensions
             */
        });
    }
}
